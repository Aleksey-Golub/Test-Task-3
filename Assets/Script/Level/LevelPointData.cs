using UnityEngine;

[CreateAssetMenu]
public class LevelPointData : ScriptableObject
{
    [SerializeField] private Sprite _rock;

    public Sprite Rock => _rock;
}
