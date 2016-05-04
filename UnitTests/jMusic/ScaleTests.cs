using System;
using System.Collections.Generic;
using System.Linq;
using jMusic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.jMusic
{
    [TestClass]
    public class ScaleTests
    {
        [TestMethod]
        public void NoteConstructor_CMajor_BuildsScaleProperly()
        {
            // Arrange
            var note = new Note(NoteValues.C);
            var expectedNoteValues = new List<NoteValues>
            {
                NoteValues.C,
                NoteValues.D,
                NoteValues.E,
                NoteValues.F,
                NoteValues.G,
                NoteValues.A,
                NoteValues.B
            };

            // Act
            var scale = new Scale(note, ScaleTypes.Major);

            // Assert
            var actualNoteValues = scale.Notes.Select(n => n.Value).ToList();
            CollectionAssert.AreEqual(expectedNoteValues, actualNoteValues);
        }

        [TestMethod]
        public void NoteValueConstructor_CMinor_BuildsScaleProperly()
        {
            // Arrange
            var expectedNoteValues = new List<NoteValues>
            {
                NoteValues.C,
                NoteValues.D,
                NoteValues.Eb,
                NoteValues.F,
                NoteValues.G,
                NoteValues.Ab,
                NoteValues.Bb
            };

            // Act
            var scale = new Scale(NoteValues.C, ScaleTypes.Minor);

            // Assert
            var actualNoteValues = scale.Notes.Select(n => n.Value).ToList();
            CollectionAssert.AreEqual(expectedNoteValues, actualNoteValues);
        }
    }
}
