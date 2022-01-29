using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : ScriptedEvent
{
    public Action onTrigger = delegate {  };
    protected override void Activate()
    {
        onTrigger?.Invoke();
    }
}
