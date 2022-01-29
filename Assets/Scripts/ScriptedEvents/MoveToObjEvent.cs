using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObjEvent : ScriptedEvent
{
    public Transform obj;
    public float speed;
    
    public bool destroyAtEnd = false;
    
    private bool active = false;    
    private bool wasActivated = false;

    private void Update()
    {
        if (active)
        {
            transform.position = Vector3.Lerp(transform.position,obj.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, obj.position) < .2f && destroyAtEnd)
            {
                active = false;
                Destroy(gameObject);
            }
        }
    }

    protected override void Activate()
    {
        active = true;
        wasActivated = true;
    }
}
