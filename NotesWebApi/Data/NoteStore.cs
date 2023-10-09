using NotesWebApi.Models;

namespace NotesWebApi.Data
{
    public class NoteStore
    {
        public static List<Note> notesList = new List<Note>
        {
            new Note{ Id=1, Title="title", Body="body" },
            new Note{ Id=2, Title="netxt", Body="body" },
            new Note{ Id=3, Title="asdf", Body="body" },
            new Note{ Id=4, Title="title", Body="adf" },
        };
    }
}
