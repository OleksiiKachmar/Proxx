using Proxx.Abstractions;
using Proxx.Domain;

namespace Proxx.Implementations;

public class CounterCellGenerator : ICounterCellGenerator
{
    public void PlaceCounterCells(Board board)
    {
        PlaceDefaultCounterCells(board);
        UpdateCounterCellValues(board);
    }

    private void UpdateCounterCellValues(Board board)
    {
        board.ApplyToAll((cell, x, y) =>
        {
            if (cell is BlackHoleCell)
            {
                UpdateCountersOfAdjacentCells(board, x, y);
            }
        });
    }

    private void UpdateCountersOfAdjacentCells(Board board, int x, int y)
    {
        board.ApplyToAdjacentCells((cell) =>
        {
            if (cell is CounterCell counterCell)
            {
                counterCell.IncreaseCounter();
            }
        }, x, y);
    }

    private void PlaceDefaultCounterCells(Board board)
    {
        board.ApplyToAll((cell, x, y) =>
        {
            if (cell == null)
            {
                board.AddCell(new CounterCell(board, x, y));
            }
        });
    }
}