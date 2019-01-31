using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{

    //Serialization is loading/unloading opbjects from memory.

    public float initialValue;

    [HideInInspector] //Don't change this value, so we hide it.
    public float RuntimeValue;

    public void OnAfterDeserialize() // After everything is unloaded.
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize()
    {
        
    }
}
