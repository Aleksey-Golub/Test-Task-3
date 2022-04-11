using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField] private EnemyAI _AI;

    private IReadOnlyLevelPoint _to;

    public override void Init(IReadOnlyLevelPoint startPoint, IKnowClosestPoint level)
    {
        base.Init(startPoint, level);

        _to = startPoint;
    }

    public override bool CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        Move(deltaTime);

        return IsAlive;
    }

    protected override void CheckInput()
    {
        if ((_to.Position - transform.position).sqrMagnitude > 0.0025) // sqrMagnitude, not distance
            return;

        _to = _AI.GetNextLevelPointOrNull(CurrentLevelPoint);
        if (_to == null)
        {
            _to = CurrentLevelPoint;
            Movement = Vector2.zero;
        }
        else
        {
            Movement = _to.Position - transform.position;
            Movement.Normalize();
        }
    }

    protected override void Move(float deltaTime)
    {
        transform.position = Vector3.MoveTowards(transform.position, _to.Position, deltaTime * Speed);
    }
}
