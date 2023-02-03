namespace Proxx;

public class CounterCell : Cell
{
    public int Counter { get; private set; }

    public CounterCell(Board board, int x, int y) : base(board, x, y)
    {
        Counter = 0;
    }

    public void IncreaseCounter()
    {
        Counter++;
    }

    public override void Open()
    {
        base.Open();
        OpenAdjacentEmptyCells();
    }

    private void OpenAdjacentEmptyCells()
    {
        Board.ApplyToAdjacentCells((cell) =>
          {
              if (cell == null || cell.IsOpen)
              {
                  return;
              }
              if (cell is CounterCell { Counter: 0 } counterCell)
              {
                  counterCell.Open();
              }
          }, X, Y);
    }
}