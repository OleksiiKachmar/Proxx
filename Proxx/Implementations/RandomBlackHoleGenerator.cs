using Proxx.Abstractions;
using Proxx.Domain;

namespace Proxx.Implementations;

public class RandomBlackHoleGenerator : IBlackHoleGenerator
{
    // In this implementation I am using flatCellIndex as one reads a book and the numbering starts from 1
    public void PlaceBlackHoles(Board board, int blackHolesCount)
    {
        var flattenedIndexesForBlackHoles = GetFlattenedCellIndexesOfBlackHoles(board.Size, blackHolesCount);
        foreach (var flattenedIndexForBlackHole in flattenedIndexesForBlackHoles)
        {
            int xIndex = (flattenedIndexForBlackHole - 1) % board.Size;
            int yIndex = (flattenedIndexForBlackHole - 1) / board.Size;
            board.AddCell(new BlackHoleCell(board, xIndex, yIndex));
        }
    }

    private IEnumerable<int> GetFlattenedCellIndexesOfBlackHoles(int size, int blackHolesCount)
    {
        if (blackHolesCount == 0)
        {
            return Array.Empty<int>();
        }

        int totalNumberOfCells = size * size;
        var result = new List<int>(blackHolesCount);
        int unassignedBlackHolesCount = blackHolesCount;
        Random random = new Random();//(Guid.NewGuid().GetHashCode());
        for (int flatCellIndex = totalNumberOfCells; flatCellIndex > 0; flatCellIndex--)
        {
            int passingScore = flatCellIndex - unassignedBlackHolesCount;
            int randomNumber = random.Next(1, flatCellIndex + 1);
            if (randomNumber > passingScore)
            {
                result.Add(flatCellIndex);
                unassignedBlackHolesCount--;
            }
        }

        return result;
    }
}