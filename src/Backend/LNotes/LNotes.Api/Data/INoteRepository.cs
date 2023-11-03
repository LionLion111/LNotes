using LNotes.Api.Data.Models;

namespace LNotes.Api.Data;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetNotesAsync();
    Task<Note?> GetNoteByIdAsync(Guid id);
    Task CreateNoteAsync(Note note); 
    Task UpdateNoteAsync(Note note); 
    Task DeleteNoteAsync(Guid id);
}
