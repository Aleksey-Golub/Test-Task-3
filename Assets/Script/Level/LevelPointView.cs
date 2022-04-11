using UnityEngine;

public class LevelPointView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LevelPointData _data;
    [SerializeField] private CircleCollider2D _collider2D;

    public void Init(LevelPointData data, int orderInLayer)
    {
        _data = data;
        _spriteRenderer.sortingOrder = orderInLayer;
    }

    public void ViewPoint(LevelPointType type)
    {
        switch (type)
        {
            case LevelPointType.None:
                SetSprite(null);
                _collider2D.enabled = false;
                break;
            case LevelPointType.Rock:
                SetSprite(_data.Rock);
                _collider2D.enabled = true;
                break;
            case LevelPointType.Bomb:
                _collider2D.enabled = false;
                break;
        }
    }

    private void SetSprite(Sprite newSprite)
    {
        _spriteRenderer.sprite = newSprite;
    }
}