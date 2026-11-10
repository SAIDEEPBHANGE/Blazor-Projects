using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Weather_Dashboard.Services;

public sealed class WeatherService(HttpClient httpClient)
{
    private const string GeocodingEndpoint = "https://geocoding-api.open-meteo.com/v1/search";
    private const string ForecastEndpoint = "https://api.open-meteo.com/v1/forecast";

    public async Task<WeatherLookupResult> GetCurrentWeatherAsync(string city, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return WeatherLookupResult.Failure("Please enter a city name.");
        }

        try
        {
            var geocodingUrl = $"{GeocodingEndpoint}?name={Uri.EscapeDataString(city)}&count=1&language=en&format=json";
            var geocodingResponse = await httpClient.GetFromJsonAsync<GeocodingResponse>(geocodingUrl, cancellationToken);
            var location = geocodingResponse?.Results?.FirstOrDefault();

            if (location is null)
            {
                return WeatherLookupResult.Failure($"No weather data found for '{city}'.");
            }

            var weatherUrl = $"{ForecastEndpoint}?latitude={location.Latitude}&longitude={location.Longitude}&current=temperature_2m,relative_humidity_2m,wind_speed_10m,weather_code&timezone=auto";
            var weatherResponse = await httpClient.GetFromJsonAsync<ForecastResponse>(weatherUrl, cancellationToken);

            if (weatherResponse?.Current is null)
            {
                return WeatherLookupResult.Failure("Weather service returned no current conditions.");
            }

            var weather = new CurrentWeather(
                location.Name,
                location.Country,
                weatherResponse.Current.Temperature,
                weatherResponse.Current.WindSpeed,
                weatherResponse.Current.Humidity,
                weatherResponse.Current.WeatherCode,
                weatherResponse.Current.Time);

            return WeatherLookupResult.Success(weather);
        }
        catch (HttpRequestException)
        {
            return WeatherLookupResult.Failure("Unable to reach the weather service right now.");
        }
        catch (TaskCanceledException)
        {
            return WeatherLookupResult.Failure("The weather request timed out.");
        }
        catch (Exception)
        {
            return WeatherLookupResult.Failure("Something went wrong while reading weather data.");
        }
    }
}

public sealed record WeatherLookupResult(CurrentWeather? Weather, string? ErrorMessage)
{
    public bool IsSuccess => Weather is not null && string.IsNullOrWhiteSpace(ErrorMessage);

    public static WeatherLookupResult Success(CurrentWeather weather) => new(weather, null);

    public static WeatherLookupResult Failure(string message) => new(null, message);
}

public sealed record CurrentWeather(
    string City,
    string? Country,
    double Temperature,
    double WindSpeed,
    int Humidity,
    int WeatherCode,
    DateTime Time);

file sealed class GeocodingResponse
{
    [JsonPropertyName("results")]
    public List<LocationResult>? Results { get; init; }
}

file sealed class LocationResult
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("country")]
    public string? Country { get; init; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; init; }
}

file sealed class ForecastResponse
{
    [JsonPropertyName("current")]
    public CurrentData? Current { get; init; }
}

file sealed class CurrentData
{
    [JsonPropertyName("temperature_2m")]
    public double Temperature { get; init; }

    [JsonPropertyName("wind_speed_10m")]
    public double WindSpeed { get; init; }

    [JsonPropertyName("relative_humidity_2m")]
    public int Humidity { get; init; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; init; }

    [JsonPropertyName("time")]
    public DateTime Time { get; init; }
}
