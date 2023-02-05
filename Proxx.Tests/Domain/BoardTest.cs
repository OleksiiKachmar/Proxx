using FluentAssertions;
using NSubstitute;
using Proxx.Abstractions;
using Proxx.Domain;

namespace Proxx.Tests.Domain
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void ApplyToAdjacentCells_WhenCellIsInTopLeft()
        {
            //Arrange
            var board = GetBoard();

            //Act
            board.ApplyToAdjacentCells((cell) =>
            {
                cell!.Open();
            }, 0, 0);

            //Assert
            Assert.IsFalse(board[0, 0].IsOpen);
            Assert.IsTrue(board[1, 0].IsOpen);
            Assert.IsFalse(board[2, 0].IsOpen);
            Assert.IsTrue(board[0, 1].IsOpen);
            Assert.IsTrue(board[1, 1].IsOpen);
            Assert.IsFalse(board[2, 1].IsOpen);
            Assert.IsFalse(board[0, 2].IsOpen);
            Assert.IsFalse(board[1, 2].IsOpen);
            Assert.IsFalse(board[2, 2].IsOpen);
        }

        [TestMethod]
        public void ApplyToAdjacentCells_WhenCellIsInCenter()
        {
            //Arrange
            var board = GetBoard();

            //Act
            board.ApplyToAdjacentCells((cell) =>
            {
                cell!.Open();
            }, 1, 1);

            //Assert
            Assert.IsTrue(board[0, 0].IsOpen);
            Assert.IsTrue(board[1, 0].IsOpen);
            Assert.IsTrue(board[2, 0].IsOpen);
            Assert.IsTrue(board[0, 1].IsOpen);
            Assert.IsFalse(board[1, 1].IsOpen);
            Assert.IsTrue(board[2, 1].IsOpen);
            Assert.IsTrue(board[0, 2].IsOpen);
            Assert.IsTrue(board[1, 2].IsOpen);
            Assert.IsTrue(board[2, 2].IsOpen);

        }

        [TestMethod]
        public void ApplyToAdjacentCells_WhenCellIsInBottomRight()
        {
            //Arrange
            var board = GetBoard();

            //Act
            board.ApplyToAdjacentCells((cell) =>
            {
                cell!.Open();
            }, 2, 2);

            //Assert
            Assert.IsFalse(board[0, 0].IsOpen);
            Assert.IsFalse(board[1, 0].IsOpen);
            Assert.IsFalse(board[2, 0].IsOpen);
            Assert.IsFalse(board[0, 1].IsOpen);
            Assert.IsTrue(board[1, 1].IsOpen);
            Assert.IsTrue(board[2, 1].IsOpen);
            Assert.IsFalse(board[0, 2].IsOpen);
            Assert.IsTrue(board[1, 2].IsOpen);
            Assert.IsFalse(board[2, 2].IsOpen);
        }

        [TestMethod]
        public void ApplyToAdjacentCells_OpensAllCells()
        {
            //Arrange
            Board board = GetBoard();

            //Act
            board.ApplyToAll((cell, x, y) =>
            {
                cell!.Open();
            });

            //Assert
            Assert.IsTrue(board[0, 0].IsOpen);
            Assert.IsTrue(board[1, 0].IsOpen);
            Assert.IsTrue(board[2, 0].IsOpen);
            Assert.IsTrue(board[0, 1].IsOpen);
            Assert.IsTrue(board[1, 1].IsOpen);
            Assert.IsTrue(board[2, 1].IsOpen);
            Assert.IsTrue(board[0, 2].IsOpen);
            Assert.IsTrue(board[1, 2].IsOpen);
            Assert.IsTrue(board[2, 2].IsOpen);
        }

        [TestMethod]
        [DataRow(-1, -1)]
        [DataRow(1, -1)]
        [DataRow(-1, 1)]
        [DataRow(5, 1)]
        [DataRow(1, 5)]
        [DataRow(5, 5)]
        public void Indexer_ThrowsException_WhenCellIndexIsOutOfRange(int x, int y)
        {
            //Arrange
            Board board = GetBoard();

            //Act and Assert
            Action act = () =>
            {
                var value = board[x, y];
            };

            act.Should().Throw<InvalidOperationException>()
                .WithMessage($"Cell coordinate X: {x} and Y: {y} is outside valid range!");
        }

        private static Board GetBoard()
        {
            Board board = new Board(3);
            board.AddCell(new MockCell(board, 0, 0));
            board.AddCell(new MockCell(board, 1, 0));
            board.AddCell(new MockCell(board, 2, 0));
            board.AddCell(new MockCell(board, 0, 1));
            board.AddCell(new MockCell(board, 1, 1));
            board.AddCell(new MockCell(board, 2, 1));
            board.AddCell(new MockCell(board, 0, 2));
            board.AddCell(new MockCell(board, 1, 2));
            board.AddCell(new MockCell(board, 2, 2));
            return board;
        }
    }
}