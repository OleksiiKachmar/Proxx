using Proxx.Domain;

namespace Proxx.Abstractions;

public interface IBlackHoleGenerator
{
    void PlaceBlackHoles(Board board, int blackHolesCount);
}