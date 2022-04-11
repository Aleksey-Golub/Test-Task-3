using UnityEngine;

public interface IKnowClosestPoint
{
    LevelPoint GetClosetsTo(Vector3 point);
}
