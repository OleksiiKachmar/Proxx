using Proxx.Domain;
using Proxx.Implementations;
using System.Collections.Generic;
using FluentAssertions;

namespace Proxx.Tests.Implementations
{
    [TestClass]
    public class CounterCellGeneratorTest
    {
        [TestMethod]
        public void PlaceCounterCells_AddCounterCellsToBoard()
        {
            //Arrange
            CounterCellGenerator sut = new();
            Board board = new Board(3);
            board.AddCell(new BlackHoleCell(board, 0, 0));
            board.AddCell(new BlackHoleCell(board, 0, 2));
            
            //Act
            sut.PlaceCounterCells(board);

            //Assert
            board[0, 1].Should().BeOfType<CounterCell>().Subject.Counter.Should().Be(2);
            board[1, 0].Should().BeOfType<CounterCell>().Subject.Counter.Should().Be(1);
            board[1, 1].Should().BeOfType<CounterCell>().Subject.Counter.Should().Be(2);
            board[1, 2].Should().BeOfType<CounterCell>().Subject.Counter.Should().Be(1);
            board[2, 0].Should().BeOfType<CounterCell>().Subject.Counter.Should().Be(0);
            board[2, 1].Should().BeOfType<CounterCell>().Subject.Counter.Should().Be(0);
            board[2, 2].Should().BeOfType<CounterCell>().Subject.Counter.Should().Be(0);
        }
    }
}