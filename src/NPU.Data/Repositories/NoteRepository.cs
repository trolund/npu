using LockNote.Data.Base;
using LockNote.Data.Model;
using Microsoft.Azure.Cosmos;

namespace LockNote.Data.Repositories;

public class NoteRepository(IRepository<Note> notesRepository)
{
    public async Task<Note> UpdateNoteAsync(Note note)
    {
        return await notesRepository.UpdateAsync(note.Id, note);
    }

    public async Task<Note> CreateNoteAsync(Note note)
    {
        return await notesRepository.AddAsync(note);
    }

    public async Task<Note?> GetNoteAsync(string id)
    {
        return await notesRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await notesRepository.GetAllAsync(new QueryDefinition("SELECT * FROM c"));
    }

    // delete one note by id
    public async Task DeleteNoteAsync(string id)
    {
        await notesRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Note>> DeleteAllOverMonthOld()
    {
        // all notes where CreatedAt is more then a month ago
        var query = new QueryDefinition(
            $"SELECT * FROM c WHERE c.CreatedAt < '{DateTime.UtcNow.AddMonths(-1):yyyy-MM-ddTHH:mm:ss.ffffffZ}'");

        var items = (await notesRepository.GetAllAsync(query)).ToList();

        foreach (var item in items)
        {
            await notesRepository.DeleteAsync(item.Id);
        }

        return items;
    }
}