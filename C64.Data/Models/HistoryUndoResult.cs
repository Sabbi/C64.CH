namespace C64.Data.Models
{
    public class HistoryUndoResult
    {
        public HistoryUndoResultStatus Status { get; set; }
    }

    public enum HistoryUndoResultStatus
    {
        Success
    }
}