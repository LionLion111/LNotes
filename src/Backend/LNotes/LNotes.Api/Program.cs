using LNotes.Api.Contracts;
using LNotes.Api.Data;
using LNotes.Api.Data.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<INoteRepository, NoteRepository>();

var app = builder.Build();

app.MapGet("/notes",
    async (INoteRepository repository) =>
    {
        var notes = await repository.GetNotesAsync();
        return Results.Ok(notes);
    });

app.MapGet("/notes/{id:guid}",
    async (INoteRepository repository, Guid id) =>
    {
        var note = await repository.GetNoteByIdAsync(id);
        return note is null ? Results.NotFound() : Results.Ok(note);
    });

app.MapPost("/notes",
    async (INoteRepository repository, AddNoteRequest request) =>
    {
        var note = new Note
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Content = request.Content,
            CreatedDateTime = DateTime.UtcNow,
        };
        
        await repository.CreateNoteAsync(note);
        return Results.Created($"/notes/{note.Id}", note);
    });

app.MapPut("/notes/{id:guid}",
    async(INoteRepository repository, Guid id, UpdateNoteRequest request) =>
    {
        var note = await repository.GetNoteByIdAsync(id);

        if (note is null)
        {
            return Results.NotFound();
        }

        note.Title = request.Title ?? note.Title;
        note.Content = request.Content ?? note.Content;
        note.IsCompleted = request.IsCompleted ?? note.IsCompleted;
        
        await repository.UpdateNoteAsync(note);
        
        return Results.NoContent();
    });

app.MapDelete("/notes/{id:guid}",
    async (INoteRepository repository, Guid id) =>
    {
        var note = await repository.GetNoteByIdAsync(id);

        if (note is null)
        {
            return Results.NotFound();
        }

        await repository.DeleteNoteAsync(id);

        return Results.NoContent();
    });

app.Run();
