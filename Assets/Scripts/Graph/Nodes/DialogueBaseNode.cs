using Assets.Scripts;
using UnityEngine;
using UnityEngine.Events;

public abstract class DialogueBaseNode : MonoBehaviour, NodeDataInterface
{
    public virtual Vector2 Position { get; set; } = Vector2.zero;
    public virtual Vector2 Size { get; set; } = new Vector2(5, 3);

    public bool isBeingDragged = false;

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }
}
