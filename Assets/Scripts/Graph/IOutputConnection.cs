using UnityEngine;

public interface IOutputConnection
{
    [SerializeField]
    public NodeConnection nodeOutput { get; set; }
}

public interface IInputConnection
{
    [SerializeField]
    public NodeConnection nodeInput { get; set; }
}
