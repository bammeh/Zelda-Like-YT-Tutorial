using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;

    public void OnSignalRaised()
    {
        signalEvent.Invoke(); // calls event.
    }

    private void OnEnable()
    {
        signal.RegisterListener(this); // add to Listener
    }

    private void OnDisable()
    {
        signal.DeRegisterListener(this); // Remove listener (saves memory).
    }
}
