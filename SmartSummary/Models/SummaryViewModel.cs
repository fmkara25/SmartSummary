namespace SmartSummary.Models
{
    public class SummaryViewModel
    {
        public string? InputText { get; set; }
        public int SentenceCount { get; set; } = 3;
        public string? Language { get; set; } // Örn: "Turkish", "English"
        public string? OutputSummary { get; set; }
        public string? Error { get; set; }
    }
}
