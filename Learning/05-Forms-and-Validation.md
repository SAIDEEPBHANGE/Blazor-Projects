# Forms And Validation

## Forms and Input Controls

- `EditForm` binds a model and validates submitted data.
- Input controls include `InputText`, `InputNumber`, `InputSelect`, `InputDate`, and `InputCheckbox`.
- Always associate labels with controls and provide clear feedback.

```razor
<EditForm Model="model" OnValidSubmit="SaveAsync">
    <DataAnnotationsValidator />
    <ValidationSummary />
    <label for="name">Name</label>
    <InputText id="name" class="form-control" @bind-Value="model.Name" />
    <ValidationMessage For="() => model.Name" />
    <button class="btn btn-primary" type="submit">Save</button>
</EditForm>
```

## Form Validation

```csharp
public sealed class Student
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = "";
    [Range(1, 120)]
    public int Age { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
}
```

- `DataAnnotationsValidator` executes attributes on the model.
- `OnValidSubmit` runs only after successful validation.
- `OnSubmit` runs regardless of validation.
- Custom server validation belongs in a validation API or service and must be repeated for security.
- Client validation improves UX but never replaces server authorization or validation.

## Repository Example

- [StudentFormModal.razor](../Student-Management-System/Components/Pages/StudentFormModal.razor) demonstrates a modal form.
