using FluentAssertions;
using NSubstitute;
using Proxx.Abstractions;
using Proxx.Domain;

namespace Proxx.Tests
{
    [TestClass]
    public class GameManagerTest
    {
        [TestMethod]
        public void Constructor_ThrowsExceptions_WhenSettingsValidationFails()
        {
            GameSettings gameSettings = new GameSettings() { BlackHoleCount = 16, BoardSize = 4 };
            Action act = () =>
            {
                new GameManager(gameSettings, null, null);
            };

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Black holes count should be less than number of cells on the board.");
        }

        [TestMethod]
        public void ClickCell_ChangesCellStateToOpen()
        {
            //Arrange
            GameSettings gameSettings = new GameSettings() { BlackHoleCount = 3, BoardSize = 3 };
            var blackHoleGenerator = Substitute.For<IBlackHoleGenerator>();
            var counterCellGenerator = Substitute.For<ICounterCellGenerator>();

            counterCellGenerator.WhenForAnyArgs(x => x.PlaceCounterCells(null!))
                .Do((x) =>
                {
                    Board board = x.Arg<Board>();
                    board.AddCell(new CounterCell(board, 1, 1));
                });

            GameManager gameManager = new GameManager(gameSettings, blackHoleGenerator, counterCellGenerator);

            //Act
            var actualBoard = gameManager.ClickCell(1, 1);

            //Assert
            actualBoard[1, 1]!.IsOpen.Should().Be(true);
        }

    }
}