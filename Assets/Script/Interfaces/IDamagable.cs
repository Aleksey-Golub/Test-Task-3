using System;

public interface IDamagable
{
    bool IsAlive { get; }
    event Action<BaseUnit> Died;

    void ApplyDamage(int damage);
}
