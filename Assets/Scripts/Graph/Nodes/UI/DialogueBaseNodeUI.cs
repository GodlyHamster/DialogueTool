using Unity.VisualScripting;
using UnityEngine;

public abstract class DialogueBaseNodeUI : MonoBehaviour
{
    public bool isBeingDragged = false;

    public DialogueBaseNodeData nodeData;

    public void SetPosition(Vector2 position)
    {
        nodeData.NodePosition = position;
    }

    public virtual void Setup() {}

    public virtual void OnLoad() {}
}
