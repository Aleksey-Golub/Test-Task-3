using System;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour, IUpdatable
{
    private List<Bomb> _bombs = new List<Bomb>();
    private BombFactory _bombFactory;

    public event Action<Bomb> BombExploded;

    public bool CustomUpdate(float deltaTime)
    {
        for (int i = 0; i< _bombs.Count; i++)
        {
            if (_bombs[i].CustomUpdate(deltaTime) == false)
            {
                int lastIndex = _bombs.Count - 1;
                _bombs[i] = _bombs[lastIndex];
                _bombs.RemoveAt(lastIndex);
                i--;
            }
        }

        return true;
    }

    public void Init(BombFactory bombFactory)
    {
        _bombFactory = bombFactory;
    }

    public void SetBomb(IReadOnlyLevelPoint point)
    {
        Bomb bomb = _bombFactory.GetBomb(point.Position, transform);
        bomb.Init(point);
        bomb.Exploded += OnBombExploded;

        _bombs.Add(bomb);
    }

    private void OnBombExploded(Bomb bomb)
    {
        ParticleSystem newVFX = _bombFactory.GetExplosionEffect(bomb.LevelPoint.Position, transform);
        newVFX.gameObject.SetActive(true); // activate here, deactivate if ParticleSystem.Main.StopAction.Disable in inspector

        bomb.Exploded -= OnBombExploded;
        BombExploded?.Invoke(bomb);
        _bombFactory.Recycle(bomb);
    }
}
