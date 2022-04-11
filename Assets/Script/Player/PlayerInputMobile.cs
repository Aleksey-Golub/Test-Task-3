public class PlayerInputMobile : IPlayerInput
{
    private BombButton _setBombButton;
    private Joystick _joystick;

    public float XMovement => _joystick.Horizontal;
    public float YMovement => _joystick.Vertical;
    public bool SetBomb => _setBombButton.IsClicked;

    public PlayerInputMobile(BombButton setBombButton, Joystick joystick)
    {
        _setBombButton = setBombButton;
        _joystick = joystick;
    }
}
