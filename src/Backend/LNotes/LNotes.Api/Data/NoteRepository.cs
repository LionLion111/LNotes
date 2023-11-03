using LNotes.Api.Data.Models;

namespace LNotes.Api.Data
{
    public class NoteRepository : INoteRepository
    {
        private readonly IDictionary<Guid, Note> _notes = new Dictionary<Guid, Note>();       

        public async Task<IEnumerable<Note>> GetNotesAsync()
        {
           return await Task.FromResult(_notes.Values);
        }

        public async Task<Note?> GetNoteByIdAsync(Guid id)
        {
            _notes.TryGetValue(id, out var note);
            return await Task.FromResult(note);
        }

        public async Task CreateNoteAsync(Note note)
        {
            _notes.Add(note.Id, note);
            await Task.CompletedTask;
        }       
        
        public async Task UpdateNoteAsync(Note note)
        {
            _notes[note.Id] = note;
            await Task.CompletedTask;
        }

        public Task DeleteNoteAsync(Guid id)
        {
            _notes.Remove(id);
            return Task.CompletedTask;
        }
    }
}
