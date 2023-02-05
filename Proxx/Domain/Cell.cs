namespace Proxx.Domain;

public abstract class Cell
{
    protected readonly Board Board;
    public int X { get; }
    public int Y { get; }

    protected Cell(Board board, int x, int y)
    {
        Board = board;
        Y = y;
        X = x;
    }

    public bool IsOpen { get; private set; }

    public virtual void Open()
    {
        IsOpen = true;
    }
}