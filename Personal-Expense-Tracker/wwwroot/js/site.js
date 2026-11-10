window.expenseTrackerState = {
    get: function (key) {
        const raw = localStorage.getItem(key);
        if (!raw) {
            return null;
        }

        try {
            return JSON.parse(raw);
        } catch {
            return null;
        }
    },
    set: function (key, value) {
        localStorage.setItem(key, JSON.stringify(value));
    }
};

window.expenseTracker = {
    downloadCsv: function (fileName, content) {
        const blob = new Blob([content], { type: "text/csv;charset=utf-8;" });
        const url = URL.createObjectURL(blob);
        const link = document.createElement("a");
        link.href = url;
        link.download = fileName;
        link.style.display = "none";
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        URL.revokeObjectURL(url);
    }
};
