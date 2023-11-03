namespace LNotes.Api.Contracts;

public class UpdateNoteRequest
{
    public string? Title { get; set; } 
    public string? Content { get; set; } 
    public bool? IsCompleted { get; set; }
}
