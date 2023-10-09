using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NotesWebApi.Data;
using NotesWebApi.Models;
using System.Reflection.Metadata.Ecma335;
using System.Linq;

namespace NotesWebApi.Controllers
{
    [ApiController]
    [Route("notes")]
    public class NoteApiController : ControllerBase
    {
        //private readonly ILogger<NoteApiController> _logger;
        //public NoteApiController(ILogger<NoteApiController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Note>> GetNotes()
        {
            //_logger.LogInformation("Getting all notes");
            return Ok(NoteStore.notesList);
        }


        [HttpGet("{id:int}", Name = "GetNote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Note> GetNote(int id)
        {
            var note = NoteStore.notesList.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                //_logger.LogError("Get note error with id" + id.ToString());
                return NotFound();
            }
            return Ok(note);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Note> CreateNote([FromBody]Note note)
        {
            if(NoteStore.notesList.FirstOrDefault(n => n.Title.ToLower() == note.Title.ToLower()) != null)
            {
                ModelState.AddModelError("CreateError", "This title already exists!");
                return BadRequest(ModelState);

            }

            if (note == null) return BadRequest(note);
            note.Id = NoteStore.notesList.OrderByDescending(n => n.Id).FirstOrDefault().Id + 1;
            NoteStore.notesList.Add(note);

            //return Ok(note);
            return CreatedAtRoute("GetNote", new { id = note.Id }, note);
        }


        [HttpDelete("{id:int}", Name = "DeleteNote")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteNote(int id)
        {
            var note = NoteStore.notesList.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            NoteStore.notesList.Remove(note);
            return NoContent();
        }


        [HttpPut(Name ="UpdateNote")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateNote([FromBody]Note note)
        {
            if (note == null) return BadRequest();

            var old_note = NoteStore.notesList.FirstOrDefault(n => n.Id == note.Id);

            if (old_note == null) return BadRequest();
            old_note.Title = note.Title;
            old_note.Body = note.Body;

            return NoContent();
        }
        
    }
}
