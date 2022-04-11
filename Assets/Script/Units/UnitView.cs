using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private readonly int _xInputKey = Animator.StringToHash("xInput");
    private readonly int _yInputKey = Animator.StringToHash("yInput");
    private readonly int _speedKey = Animator.StringToHash("speed");
    private readonly int _directionKey = Animator.StringToHash("direction");

    public void SetOrderInLayer(int newValue)
    {
        _spriteRenderer.sortingOrder = newValue;
    }

    public void Move(Vector2 movement, int direction)
    {
        _animator.SetFloat(_xInputKey, movement.x);
        _animator.SetFloat(_yInputKey, movement.y);
        _animator.SetFloat(_speedKey, movement.sqrMagnitude);
        _animator.SetInteger(_directionKey, direction);
    }
}
