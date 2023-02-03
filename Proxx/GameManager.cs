namespace Proxx;

public class GameManager
{
    private readonly IBlackHoleGenerator _blackHoleGenerator;
    private readonly Board _board;

    public GameManager(GameSettings gameSettings, IBlackHoleGenerator blackHoleGenerator, ICounterCellGenerator counterCellGenerator)
    {
        _blackHoleGenerator = blackHoleGenerator;
        _board = new Board(gameSettings.BoardSize);

        blackHoleGenerator.PlaceBlackHoles(_board, gameSettings.BlackHoleCount);
        counterCellGenerator.PlaceCounterCells(_board);
    }

    public void ClickCell(int x, int y)
    {
        var cell = _board[x, y];
        if (cell == null)
        {
            throw new InvalidOperationException(
                $"Board was incorrectly initialized, and as a result cell with coordinates x: {x}, y: {y} is null");
        }
        
        cell.Open();
    }
}