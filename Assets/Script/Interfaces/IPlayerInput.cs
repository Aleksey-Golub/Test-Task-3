public interface IPlayerInput
{
    public float XMovement { get; }
    public float YMovement { get; }
    public bool SetBomb { get; }
}

public enum InputType
{
    Desktop,
    Mobile
}
