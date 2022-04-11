using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IUpdatable
{
    [SerializeField] private List<LevelEnemyData> _enemiesOnLevel;

    private List<Enemy> _enemies = new List<Enemy>();

    public event Action AllEnemiesDied;

    public void Init(IKnowClosestPoint level)
    {
        foreach (var e in _enemiesOnLevel)
        {
            var enemy = Instantiate(e.Enemy, e.Point.Position, Quaternion.identity, transform);
            enemy.Init(e.Point, level);
            enemy.Died += OnEnemyDied;
            _enemies.Add(enemy);
        }
    }

    public bool CustomUpdate(float deltaTime)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].CustomUpdate(deltaTime) == false)
            {
                int lastIndex = _enemies.Count - 1;
                _enemies[i] = _enemies[lastIndex];
                _enemies.RemoveAt(lastIndex);
                i--;
            }
        }

        return true;
    }

    private void OnEnemyDied(BaseUnit enemy)
    {
        enemy.Died -= OnEnemyDied;

        _enemies.Remove((Enemy)enemy);
        Destroy(enemy.gameObject);

        if (_enemies.Count == 0)
            AllEnemiesDied?.Invoke();
    }

    [Serializable]
    private class LevelEnemyData
    {
        public Enemy Enemy;
        public LevelPoint Point;
    }
}
