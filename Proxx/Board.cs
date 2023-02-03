namespace Proxx;

/// <summary>
/// Boards indexes start from 1 
/// </summary>
public class Board
{
    private readonly Cell?[,] _cells;

    public int Size { get; private set; }

    public Cell? this[int x, int y]
    {
        get { return _cells[x, y]; }
        //set { _cells[x, y] = value; }
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

        for (int yIndex = Math.Max(1, y - 1); yIndex <= Math.Min(y + 1, Size); yIndex++)
        {
            for (int xIndex = Math.Max(1, x - 1); xIndex <= Math.Min(x + 1, Size); xIndex++)
            {
                var cell = _cells[xIndex, yIndex];
                modification(cell);
            }
        }
    }

    public void ApplyToAll(Action<Cell?, int, int> modification)
    {
        for (int yIndex = 1; yIndex <= Size; yIndex++)
        {
            for (int xIndex = 1; xIndex <= Size; xIndex++)
            {
                var cell = _cells[xIndex, yIndex];
                modification(cell, xIndex, yIndex
                );
            }
        }
    }
}