using UnityEngine;

public interface IReadOnlyLevelPoint
{
    public IReadOnlyLevelPoint NorthNeighbor { get; }
    public IReadOnlyLevelPoint EastNeighbor { get; }
    public IReadOnlyLevelPoint SouthNeighbor { get; }
    public IReadOnlyLevelPoint WestNeighbor { get; }
    public Vector3 Position { get; }
    public int OrderInLayer { get; }
    public LevelPointType Type { get; }
    public bool IsPassable { get; }

    IReadOnlyLevelPoint[] GetAllNeighbors();
}
