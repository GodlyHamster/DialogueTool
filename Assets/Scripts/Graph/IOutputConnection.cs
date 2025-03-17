using UnityEngine;

public interface IOutputConnection
{
    [SerializeField]
    public NodeConnection nodeOutput { get; set; }
}
