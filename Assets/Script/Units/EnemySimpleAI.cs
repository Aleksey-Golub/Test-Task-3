using UnityEngine;

[CreateAssetMenu(menuName = "Enemy AI/Simple Enemy Ai")]
public class EnemySimpleAI : EnemyAI
{
    public override IReadOnlyLevelPoint GetNextLevelPointOrNull(IReadOnlyLevelPoint currentLevelPoint)
    {
        IReadOnlyLevelPoint[] neighbors = currentLevelPoint.GetAllNeighbors();
        int count = neighbors.Length;

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(i, count);
            if (neighbors[index] == null || neighbors[index].IsPassable == false)
            {
                neighbors[index] = neighbors[i];
            }
            else
            {
                return neighbors[index];
            }
        }

        return null;
    }
}
