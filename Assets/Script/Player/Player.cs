using System;
using UnityEngine;

public class Player : BaseUnit, IFixedUpdatable
{
    [SerializeField] private Rigidbody2D _rb2d;
    [Tooltip("Rotation angle of movement vector when moving up and down. Used for easy operation on desktop.")]
    [SerializeField] private float _zRorationAngle = -45f;

    public IPlayerInput Input { get; set; }

    public event Action<IReadOnlyLevelPoint> BombSet;

    public override bool CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);

        if (Input.SetBomb && CurrentLevelPoint.Type != LevelPointType.Bomb)
            BombSet?.Invoke(CurrentLevelPoint);

        return true;
    }

    public void CustomFixedUpdate(float fixedDeltaTime)
    {
        Move(fixedDeltaTime);
    }

    protected override void Move(float fixedDeltaTime)
    {
        Vector3 vector = Movement;

        if (Input is PlayerInputDesktop)
            if (Vector3.Dot(Movement, Vector3.right) == 0)
                vector = Quaternion.Euler(0, 0, _zRorationAngle) * vector;

        Vector2 offset = fixedDeltaTime * Speed * vector;

        _rb2d.MovePosition(_rb2d.position + offset);
    }

    protected override void CheckInput()
    {
        Movement.x = Input.XMovement;
        Movement.y = Input.YMovement;
        Movement.Normalize();

        Debug.Log(Movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
            Die();
    }
}
