using UnityEngine;

public class Interaction : MonoBehaviour
{
    public virtual void OnClick(Collider collider) { }

    public virtual bool State { get; }

}