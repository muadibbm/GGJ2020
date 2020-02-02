using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
    public UnityAction<Collider> onEnter;
    public UnityAction<Collider> onExit;

    private void OnTriggerEnter(Collider other) {
        if(onEnter != null) onEnter.Invoke(other);
    }

    private void OnTriggerExit(Collider other) {
        if (onExit != null) onExit.Invoke(other);
    }
}
