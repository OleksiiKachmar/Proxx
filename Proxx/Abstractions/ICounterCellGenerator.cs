using Proxx.Domain;

namespace Proxx.Abstractions;

public interface ICounterCellGenerator
{
    void PlaceCounterCells(Board board);
}