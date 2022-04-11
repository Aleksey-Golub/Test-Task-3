using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class BombFactory : ScriptableObject
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private ParticleSystem _explosionPrefab;
    
    private List<ParticleSystem> _explosionVFXs;
    private Stack<Bomb> _bombs;

    public void Init()
    {
        _explosionVFXs = new List<ParticleSystem>();
        _bombs = new Stack<Bomb>();
    }

    public Bomb GetBomb(Vector3 position, Transform parent)
    {
        Bomb bomb;
        if (_bombs.Count > 0)
        {
            bomb = _bombs.Pop();
            bomb.transform.parent = parent;
            bomb.transform.position = position;
        }
        else
        {
            bomb = Instantiate(_bombPrefab, position, Quaternion.identity, parent);
        }
        return bomb;
    }

    public ParticleSystem GetExplosionEffect(Vector3 position, Transform parent)
    {
        ParticleSystem particle = _explosionVFXs.FirstOrDefault(p => p.gameObject.activeSelf == false);

        if (particle == null)
        {
            particle = Instantiate(_explosionPrefab, position, Quaternion.identity, parent);
            _explosionVFXs.Add(particle);
        }

        particle.transform.position = position;
        particle.transform.parent = parent;
        return particle;
    }

    public void Recycle(Bomb bomb)
    {
        _bombs.Push(bomb);
    }
}
