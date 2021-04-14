using UnityEngine;
using UnityEngine.Events;

public class BushAction : MonoBehaviour
{
    public UnityEvent action;

    public void Action()
    {
        action?.Invoke();
    }
}