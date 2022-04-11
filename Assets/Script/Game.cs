using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Level _level;
    [SerializeField] private Player _player;
    [SerializeField] private BombFactory _bombFactory;
    [SerializeField] private BombController _bombController;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private BombButton _bombButton;
    [SerializeField] private Joystick _joystick;
    [Header("Settings")]
    [SerializeField] private LevelPoint _startPlayerPoint;
    [SerializeField] private InputType _inputType;

    private IEnumerator Start()
    {
        _level.Init();

        _player.Init(_startPlayerPoint, _level);
        _player.Input = GetPlayerInput();
        _player.BombSet += OnPlayerBombSet;
        _player.Died += OnPlayerDied;

        _bombFactory.Init();
        _bombController.Init(_bombFactory);
        _bombController.BombExploded += OnBombExploded;

        _enemyController.Init(_level);
        _enemyController.AllEnemiesDied += OnAllEnemiesDied;

        _startPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        _startPanel.SetActive(false);
    }

    private IPlayerInput GetPlayerInput()
    {
        switch (_inputType)
        {
            case InputType.Desktop:
                return new PlayerInputDesktop();
            case InputType.Mobile:
                _joystick.gameObject.SetActive(true);
                _bombButton.gameObject.SetActive(true);
                return new PlayerInputMobile(_bombButton, _joystick);
            default:
                throw new NotImplementedException();
        }
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        _player.CustomUpdate(deltaTime);
        _bombController.CustomUpdate(deltaTime);

        _enemyController.CustomUpdate(deltaTime);

        _bombButton.CustomUpdate(deltaTime);
    }

    private void FixedUpdate()
    {
        float fixedDeltaTime = Time.fixedDeltaTime;

        _player.CustomFixedUpdate(fixedDeltaTime);
    }

    private void OnAllEnemiesDied()
    {
        ReloadLevel();
    }

    private void OnPlayerBombSet(IReadOnlyLevelPoint levelPoint)
    {
        _bombController.SetBomb(levelPoint);
        _level.SetBombTo(levelPoint);
    }

    private void OnPlayerDied(BaseUnit player)
    {
        ReloadLevel();
    }

    private void OnBombExploded(Bomb bomb)
    {
        _level.RemoveBombFrom(bomb.LevelPoint);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
