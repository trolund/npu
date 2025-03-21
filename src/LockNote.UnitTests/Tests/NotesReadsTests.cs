// using LockNote.Bl;
//
// namespace LockNote.UnitTests.Tests;
//
// public class NotesReadsTests
// {
//     [Theory]
//     [InlineData(1)]
//     public void Test1(int numberOfReads)
//     {
//         var service = new NotesService();
//         
//         var notes = new List<Note>();
//         for (var i = 0; i < numberOfReads; i++)
//         {
//             notes.Add(new Note
//             {
//                 Id = Guid.NewGuid().ToString(),
//                 PartitionKey = "partitionKey",
//                 Title = "title",
//                 Content = "content",
//                 CreatedAt = DateTime.UtcNow,
//                 UpdatedAt = DateTime.UtcNow
//             });
//         }
//
//         var reads = new List<NoteRead>();
//         for (var i = 0; i < numberOfReads; i++)
//         {
//             reads.Add(new NoteRead
//             {
//                 Id = Guid.NewGuid().ToString(),
//                 PartitionKey = "partitionKey",
//                 NoteId = notes[i].Id,
//                 ReadAt = DateTime.UtcNow
//             });
//         }
//
//         var notesReads = new NotesReads(notes, reads);
//         var result = notesReads.GetNotesReads();
//         Assert.Equal(numberOfReads, result.Count);
//     }
// }