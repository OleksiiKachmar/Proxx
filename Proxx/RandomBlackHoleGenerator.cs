namespace Proxx;

public class RandomBlackHoleGenerator : IBlackHoleGenerator
{
    public void PlaceBlackHoles(Board board, int blackHolesCount)
    {
        var flattenedIndexesForBlackHoles = GetFlattenedCellIndexesOfBlackHoles(board.Size, blackHolesCount);
        foreach (var flattenedIndexesForBlackHole in flattenedIndexesForBlackHoles)
        {
            int xIndex = board.Size % flattenedIndexesForBlackHole + 1;
            int yIndex = board.Size / flattenedIndexesForBlackHole;
            board.AddCell(new BlackHoleCell(board, xIndex, yIndex));
        }
    }

    private IEnumerable<int> GetFlattenedCellIndexesOfBlackHoles(int blackHolesCount, int totalNumberOfCells)
    {
        if (blackHolesCount == 0)
        {
            return Array.Empty<int>();
        }

        var result = new List<int>(blackHolesCount);
        int unassignedBlackHolesCount = blackHolesCount;
        Random random = new Random();
        for (int flatCellIndex = totalNumberOfCells; flatCellIndex > 0; flatCellIndex--)
        {
            int passingScore = flatCellIndex - unassignedBlackHolesCount;
            int randomNumber = random.Next(1, flatCellIndex + 1);
            if (randomNumber < passingScore)
            {
                result.Add(flatCellIndex);
                unassignedBlackHolesCount--;
            }
        }

        return result;
    }
}