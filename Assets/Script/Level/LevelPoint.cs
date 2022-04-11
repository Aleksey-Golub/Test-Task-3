using UnityEngine;

public enum LevelPointType
{
    None,
    Rock,
    Bomb
}

public class LevelPoint : MonoBehaviour, IReadOnlyLevelPoint
{
    [SerializeField] private LevelPointType _type;
    [SerializeField] private LevelPointView _view;
    [Header("Debug")]
    [SerializeField] private LevelPoint _northNeighbor;
    [SerializeField] private LevelPoint _eastNeighbor;
    [SerializeField] private LevelPoint _southNeighbor;
    [SerializeField] private LevelPoint _westNeighbor;
    [SerializeField] private bool _isPassable;

    public IReadOnlyLevelPoint NorthNeighbor { get => _northNeighbor; private set => _northNeighbor = (LevelPoint)value; }
    public IReadOnlyLevelPoint EastNeighbor { get => _eastNeighbor; private set => _eastNeighbor = (LevelPoint)value; }
    public IReadOnlyLevelPoint SouthNeighbor { get => _southNeighbor; private set => _southNeighbor = (LevelPoint)value; }
    public IReadOnlyLevelPoint WestNeighbor { get => _westNeighbor; private set => _westNeighbor = (LevelPoint)value; }
    public Vector3 Position => transform.position;
    public int OrderInLayer { get; private set; }
    public LevelPointType Type => _type;
    public bool IsPassable { get => _isPassable; set => _isPassable = value; }

    public void Init(LevelPointData data, int orderInLayer)
    {
        OrderInLayer = orderInLayer;
        _view.Init(data, orderInLayer);

        SetType(_type);
    }

    public IReadOnlyLevelPoint[] GetAllNeighbors()
    {
        return new IReadOnlyLevelPoint[] { _northNeighbor, _eastNeighbor, _southNeighbor, _westNeighbor };
    }

    public void SetType(LevelPointType newType)
    {
        switch (newType)
        {
            case LevelPointType.None:
                IsPassable = true;
                break;
            case LevelPointType.Rock:
            case LevelPointType.Bomb:
                IsPassable = false;
                break;
        }

        _type = newType;
        _view.ViewPoint(_type);
    }

    public static void SetPointsNeighbors(LevelPoint[,] points)
    {
        int maxColunmIndex = points.GetUpperBound(1); // 16, if default size
        int maxRowIndex = points.GetUpperBound(0); // 8, if default size

        for (int row = 0; row <= maxRowIndex; row++)
        {
            for (int column = 0; column <= maxColunmIndex; column++)
            {
                int northRow = row - 1;
                int eastColumn = column + 1;
                int southRow = row + 1;
                int westColumn = column - 1;

                points[row, column].NorthNeighbor = northRow >= 0 ? points[northRow, column] : null;
                points[row, column].EastNeighbor = eastColumn <= maxColunmIndex ? points[row, eastColumn] : null;
                points[row, column].SouthNeighbor = southRow <= maxRowIndex ? points[southRow, column] : null;
                points[row, column].WestNeighbor = westColumn >= 0 ? points[row, westColumn] : null;
            }
        }
    }

    private void OnValidate()
    {
        SetType(_type);
    }
}
