using UnityEngine;

public class DialogueNode : MonoBehaviour
{
    public bool isBeingDragged = false;

    public DialogueBaseNode node;

    public void SetPosition(Vector2 position)
    {
        node.NodePosition = position;
    }
}
