using Proxx.Domain;
using Proxx.Implementations;
using System.Collections.Generic;
using FluentAssertions;

namespace Proxx.Tests.Implementations
{
    [TestClass]
    public class RandomBlackHoleGeneratorTest
    {
        [TestMethod]
        [DataRow(1, 5)]
        [DataRow(5, 10)]
        [DataRow(8, 100)]
        public void PlaceBlackHoles_ExactlyXNumberHoles(int numberOfHoles, int boardSize)
        {
            //Arrange
            RandomBlackHoleGenerator sut = new RandomBlackHoleGenerator();
            Board board = new Board(boardSize);

            //Act
            sut.PlaceBlackHoles(board, numberOfHoles);

            //Assert
            int actualBlackHolesCount = 0;
            board.ApplyToAll((cell, x, y) =>
            {
                if (cell is BlackHoleCell) actualBlackHolesCount++;
            });
            Assert.AreEqual(numberOfHoles, actualBlackHolesCount);
        }

        // In this implementation I am using flatCellIndex as one reads a book and the numbering starts from 1
        [TestMethod]
        [DataRow(1, 5, 9)]
        [DataRow(1, 5, 8)]
        [DataRow(6, 8, 9)]
        [DataRow(1, 2, 3)]
        public void PlaceBlackHoles_DistributionIsEvenWith5PercentOfAccuracy(params int[] flattenedIndexes)
        {
            RandomBlackHoleGenerator sut = new RandomBlackHoleGenerator();

            int matchCount = 0;

            var expectedBoardPattern = GetExpectedPattern(flattenedIndexes);

            for (int i = 0; i < 10000; i++)
            {
                Board board = new Board(3);
                sut.PlaceBlackHoles(board, 3);
                if (DoesBoardMatchesPattern(board, expectedBoardPattern))
                {
                    matchCount++;
                }
            }

            //This is to test that each pattern of black holes distribution has equal chances.
            matchCount.Should().BeInRange(100, 150);
        }

        private bool DoesBoardMatchesPattern(Board board, List<BlackHoleCell> expectedBoardPattern)
        {
            foreach (var blackHoleCell in expectedBoardPattern)
            {
                if (!(board[blackHoleCell.X, blackHoleCell.Y] is BlackHoleCell))
                {
                    return false;
                }
            }

            return true;
        }

        private List<BlackHoleCell> GetExpectedPattern(params int[] flattenedIndexes)
        {
            List<BlackHoleCell> result = new List<BlackHoleCell>(3);
            foreach (var flattenedIndex in flattenedIndexes)
            {
                var blackHole = new BlackHoleCell(default, GetX(flattenedIndex), GetY(flattenedIndex));
                result.Add(blackHole);
            }

            return result;
        }

        private int GetY(int flattenedIndex)
        {
            return (flattenedIndex - 1) / 3;
        }

        private int GetX(int flattenedIndex)
        {
            return (flattenedIndex - 1) % 3;
        }
    }

}