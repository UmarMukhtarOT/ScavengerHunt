using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnableDisable : MonoBehaviour
{
    public UnityEvent OnEnableAction;
    public UnityEvent OnDisableAction;
    private void OnEnable()
    {
        OnEnableAction.Invoke();
    }
    private void OnDisable()
    {
        OnDisableAction.Invoke();
    }
}
