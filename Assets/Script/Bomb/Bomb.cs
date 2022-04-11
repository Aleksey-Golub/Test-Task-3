using System;
using UnityEngine;

public class Bomb : MonoBehaviour, IUpdatable
{
    [SerializeField] private float _explosionDelay = 3f;
    [SerializeField] private float _explosionRadius = 1f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private CircleCollider2D _collider2D;

    private float _timer = 0;

    public IReadOnlyLevelPoint LevelPoint { get; private set; }
    public event Action<Bomb> Exploded;

    public bool CustomUpdate(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= _explosionDelay)
        {
            Explode();
            return false;
        }

        return true;
    }

    public void Init(IReadOnlyLevelPoint levelPoint)
    {
        LevelPoint = levelPoint;
        gameObject.SetActive(true);
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (var h in hits)
            if (h.TryGetComponent(out IDamagable damagable))
                damagable.ApplyDamage(_damage);

        Exploded?.Invoke(this);
        ResetFields();
    }

    private void ResetFields()
    {
        _timer = 0;
        _collider2D.isTrigger = true;
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _collider2D.isTrigger = false;
    }
}
