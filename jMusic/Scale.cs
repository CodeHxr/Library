using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jMusic
{
    public class Scale
    {
        #region Private Members
        private readonly Note _root;
        private ScaleTypes _scaleType;

        #endregion

        #region Properties
        public List<Note> Notes { get; } = new List<Note>();
        #endregion

        #region Constructors

        public Scale(Note root, ScaleTypes type)
        {
            _root = root;
            _scaleType = type;

            BuildScale();
        }

        public Scale(NoteValues rootValue, ScaleTypes scaleType)
        {
            var rootNote = new Note(rootValue);

            _root = rootNote;
            _scaleType = scaleType;

            BuildScale();
        }
        #endregion

        #region Private Methods

        private void BuildScale()
        {
            // Start with a major scale, then modify if needed
            // Major scale pattern: r, w, w, h, w, w, w
            // Major scale offsets: 0, 2, 2, 1, 2, 2, 2
            // Root note offsets:   0, 2, 4, 5, 7, 9, 11
            Notes.AddRange(new [] { _root, _root + 2, _root + 4, _root + 5, _root + 7, _root + 9, _root + 11});

            switch (_scaleType)
            {
                case ScaleTypes.Major:
                    // do nothing
                    break;
                case ScaleTypes.Minor:
                    Notes[2] -= 1;
                    Notes[5] -= 1;
                    Notes[6] -= 1;
                    break;
                case ScaleTypes.HarmonicMinor:
                    Notes[2] -= 1;
                    Notes[5] -= 1;
                    break;
                case ScaleTypes.MelodicMinor:
                    Notes[2] -= 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}
