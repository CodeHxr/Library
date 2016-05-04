using System;
using jMusic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.jMusic
{
    [TestClass]
    public class NoteTests
    { 
        [TestMethod]
        public void AddOperator_IncrementHalfStep()
        {
            // Arrange
            var note = new Note(NoteValues.C);

            // Act
            var newNote = note + 1;

            // Assert
            Assert.AreEqual(NoteValues.Db, newNote.Value);
        }

        [TestMethod]
        public void AddOperator_IncrementFullStep()
        {
            // Arrange
            var note = new Note(NoteValues.C);

            // Act
            var newNote = note + 2;

            // Assert
            Assert.AreEqual(NoteValues.D, newNote.Value);
        }

        [TestMethod]
        public void AddOperator_WrapAroundScale()
        {
            // Arrange
            var note = new Note(NoteValues.G);

            // Act
            var newNote = note + 3;

            // Assert
            Assert.AreEqual(NoteValues.Bb, newNote.Value);
        }

        [TestMethod]
        public void AddOperator_FullScaleIncrement()
        {
            // Arrange
            var note = new Note(NoteValues.C);

            // Act
            var newNote = note + 12;

            // Assert
            Assert.AreEqual(note.Value, newNote.Value);
        }

        // This method assumes that if one can decrement one half-step, the rest of it should work fine
        [TestMethod]
        public void SubtractOperator_DecrementHalfStep()
        {
            // Arrange
            var note = new Note(NoteValues.C);

            // Act
            var newNote = note - 1;

            // Assert
            Assert.AreEqual(NoteValues.B, newNote.Value);
        }
    }
}
