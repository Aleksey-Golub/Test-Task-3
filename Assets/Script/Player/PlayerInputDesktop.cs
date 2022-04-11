using UnityEngine;

public class PlayerInputDesktop : IPlayerInput
{
    public float XMovement => Input.GetAxisRaw("Horizontal");
    public float YMovement => Input.GetAxisRaw("Vertical");
    public bool SetBomb => Input.GetKeyDown(KeyCode.Space);
}
