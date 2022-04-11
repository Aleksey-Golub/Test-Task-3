using System;
using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West
}

public abstract class BaseUnit : MonoBehaviour, IUpdatable, IDamagable
{
    [SerializeField] private UnitView _view;
    [SerializeField] private int _health = 1;

    [SerializeField] protected float Speed = 1f;
    protected Vector2 Movement = Vector2.zero;
    protected IReadOnlyLevelPoint CurrentLevelPoint;

    private IKnowClosestPoint _level;
    private Direction _direction = Direction.East;

    public bool IsAlive => _health > 0;
    public event Action<BaseUnit> Died;

    public virtual void Init(IReadOnlyLevelPoint startPoint, IKnowClosestPoint level)
    {
        _level = level;
        CurrentLevelPoint = startPoint;
        transform.position = CurrentLevelPoint.Position;
        SetOrderInLayer();
    }

    public virtual bool CustomUpdate(float deltaTime)
    {
        UpdateCurrentLevelPoint();
        CheckInput();

        SetMovementDirection();

        _view.Move(Movement, (int)_direction);

        return true;
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (IsAlive == false)
        {
            Die();
        }
    }

    protected abstract void Move(float deltaTime);

    private void UpdateCurrentLevelPoint()
    {
        if (Time.frameCount % 2 == 0)
        {
            CurrentLevelPoint = _level.GetClosetsTo(transform.position);
            SetOrderInLayer();
        }
    }

    protected abstract void CheckInput();
    
    protected void Die()
    {
        Died?.Invoke(this);
    }

    private void SetMovementDirection()
    {
        float angle = Vector2.Angle(Movement, Vector2.up);
        if (Movement.sqrMagnitude != 0)
        {
            if (angle <= 45)
                _direction = Direction.North;
            else if (angle >= 135)
                _direction = Direction.South;
            else if (Movement.x > 0)
                _direction = Direction.East;
            else
                _direction = Direction.West;
        }
    }

    private void SetOrderInLayer()
    {
        _view.SetOrderInLayer(CurrentLevelPoint.OrderInLayer);
    }
}
