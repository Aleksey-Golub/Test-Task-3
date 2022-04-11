using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BombButton : Button, IUpdatable
{
    public bool IsClicked { get; set; }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        IsClicked = true;
        Debug.Log("BUTTON");
    }
    public bool CustomUpdate(float deltaTime)
    {
        IsClicked = false;

        return true;
    }
}
