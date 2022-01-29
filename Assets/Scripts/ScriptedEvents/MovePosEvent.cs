using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosEvent : ScriptedEvent
{
    public List<Vector3> moves = new List<Vector3>();
    public float speed;

    public bool destroyAtEnd = false;
    
    private int curIndex = 0;
    private bool active = false;
    private bool wasActivated = false;

    private Vector3 destination;
    private bool moving = false;
    private void Update()
    {
        if (active)
        {
            if (moves.Count - 1 >= curIndex)
            {
                if (!moving)
                {
                    destination = new Vector3(transform.position.x + moves[curIndex].x, transform.position.y + moves[curIndex].y,
                        transform.position.z + moves[curIndex].z);
                    moving = true;
                }
                
                transform.position = Vector3.Lerp(transform.position,
                    destination, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, destination) < .2f)
                {
                    curIndex++;
                    moving = false;
                }
            }
            else
            {
                if (destroyAtEnd)
                {
                    active = false;
                    Destroy(gameObject);
                }
            }
        }
    }

    protected override void Activate()
    {
        active = true;        
        wasActivated = true;

    }
    
}
