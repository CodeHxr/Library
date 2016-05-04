using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jMusic
{
    public class Note
    {
        #region Private Members

        #endregion

        #region Properties
        public NoteValues Value { get; }

        public string Name => Value.ToString();
        #endregion

        #region Constructors
        public Note(NoteValues note)
        {
            Value = note;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region Operators

        public static Note operator +(Note a, int halfSteps)
        {
            var noteNumber = (int)a.Value;

            noteNumber += halfSteps;
            noteNumber %= 12;

            var newNote = new Note((NoteValues)noteNumber);

            return newNote;
        }

        public static Note operator -(Note n, int halfSteps)
        {
            var newNote = n + (-1 * halfSteps);
            return newNote;
        }
        #endregion
    }
}
