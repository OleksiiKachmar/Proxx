using System.ComponentModel.DataAnnotations;

namespace Proxx;

public class GameSettings
{
    [MinLength(1)]
    public int BlackHoleCount { get; set; }
    [MinLength(3)]
    public int BoardSize { get; set; }
}