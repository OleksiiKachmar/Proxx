namespace Proxx.Domain;

public class Board
{
    private readonly Cell?[,] _cells;

    public int Size { get; }

    public Cell? this[int x, int y]
    {
        get
        {
            ValidateCoordinates(x, y);
            return _cells[x, y];
        }
    }

    public Board(int size)
    {
        _cells = new Cell[size, size];
        Size = size;
    }

    public void AddCell(Cell cell)
    {
        _cells[cell.X, cell.Y] = cell;
    }

    public void ApplyToAdjacentCells(Action<Cell?> modification, int x, int y)
    {
        for (int yIndex = Math.Max(0, y - 1); yIndex <= Math.Min(y + 1, Size - 1); yIndex++)
        {
            for (int xIndex = Math.Max(0, x - 1); xIndex <= Math.Min(x + 1, Size - 1); xIndex++)
            {
                if (xIndex == x && yIndex == y)
                {
                    continue;
                }
                var cell = _cells[xIndex, yIndex];
                modification(cell);
            }
        }
    }

    public void ApplyToAll(Action<Cell?, int, int> modification)
    {
        for (int yIndex = 0; yIndex < Size; yIndex++)
        {
            for (int xIndex = 0; xIndex < Size; xIndex++)
            {
                var cell = _cells[xIndex, yIndex];
                modification(cell, xIndex, yIndex);
            }
        }
    }
    private void ValidateCoordinates(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Size || y >= Size)
        {
            throw new InvalidOperationException($"Cell coordinate X: {x} and Y: {y} is outside valid range!");
        }
    }
}