using Proxx.Abstractions;
using Proxx.Domain;

namespace Proxx;

public class GameManager
{
    private readonly Board _board;

    public GameManager(GameSettings gameSettings, IBlackHoleGenerator blackHoleGenerator, ICounterCellGenerator counterCellGenerator)
    {
        ValidateSettings(gameSettings);
        _board = new Board(gameSettings.BoardSize);

        blackHoleGenerator.PlaceBlackHoles(_board, gameSettings.BlackHoleCount);
        counterCellGenerator.PlaceCounterCells(_board);
    }

    private void ValidateSettings(GameSettings gameSettings)
    {
        int totalNumberOfCells = gameSettings.BoardSize * gameSettings.BoardSize;
        if (gameSettings.BlackHoleCount >= totalNumberOfCells)
        {
            throw new InvalidOperationException("Black holes count should be less than number of cells on the board.");
        }
    }

    public Board ClickCell(int x, int y)
    {
        var cell = _board[x, y];
        if (cell == null)
        {
            throw new InvalidOperationException(
                $"Board was incorrectly initialized, and as a result cell with coordinates x: {x}, y: {y} is null");
        }

        cell.Open();
        return _board;
    }
}