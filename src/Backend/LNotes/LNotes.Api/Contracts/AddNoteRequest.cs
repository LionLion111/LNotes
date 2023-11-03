namespace LNotes.Api.Contracts;

public class AddNoteRequest
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
