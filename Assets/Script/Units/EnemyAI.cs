using UnityEngine;

public abstract class EnemyAI : ScriptableObject
{
    public abstract IReadOnlyLevelPoint GetNextLevelPointOrNull(IReadOnlyLevelPoint currentLevelPoint);
}
