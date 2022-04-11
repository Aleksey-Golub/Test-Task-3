using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, IKnowClosestPoint
{
    [SerializeField] private LevelPointData _data;
    [SerializeField] private Vector2Int _levelSize = new Vector2Int(17, 9);
    [SerializeField] private List<LevelPoint> _points;

    public void Init()
    {
        ConfigurateLevel();
    }

    public void SetBombTo(IReadOnlyLevelPoint point)
    {
        SetPointType(point, LevelPointType.Bomb);
    }

    public void RemoveBombFrom(IReadOnlyLevelPoint point)
    {
        SetPointType(point, LevelPointType.None);
    }

    public LevelPoint GetClosetsTo(Vector3 point)
    {
        float sqrDistanceToClosest = float.MaxValue;

        LevelPoint closest = null;

        foreach (var p in _points)
        {
            float sqrDistance = (point - p.Position).sqrMagnitude;
            if (sqrDistance < sqrDistanceToClosest)
            {
                sqrDistanceToClosest = sqrDistance;
                closest = p;
            }
        }

        return closest;
    }

    private void SetPointType(IReadOnlyLevelPoint point, LevelPointType type)
    {
        (point as LevelPoint).SetType(type);
    }

    [ContextMenu("Rename Points")]
    private void RenamePoints()
    {
        for (int i = 0; i < _points.Count; i++)
            _points[i].gameObject.name = $"Point {i + 1}";

        Debug.Log("All Points Renamed");
    }

    [ContextMenu("Init Points and Set Points Neighbors")]
    private void ConfigurateLevel()
    {
        LevelPoint[,] points = new LevelPoint[_levelSize.y, _levelSize.x];

        if (points.Length != _points.Count)
        {
            Debug.LogError($"List of points must contains exactly {_levelSize.x * _levelSize.y} points at the specified size");
            return;
        }

        int index = 0;
        for (int row = 0; row < _levelSize.y; row++)
        {
            for (int column = 0; column < _levelSize.x; column++)
            {
                points[row, column] = _points[index];
                _points[index].Init(_data, row);
                index++;
            }
        }

        LevelPoint.SetPointsNeighbors(points);
    }
}
