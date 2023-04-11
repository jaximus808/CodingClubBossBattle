using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleControl : MonoBehaviour
{
    public Transform player;

    public float setBubbleTimer; 

    private float curBubbleTimer; 

    public GameObject bubble;     

    // Update is called once per frame
    void Update()
    {
        if(curBubbleTimer <= 0)
        {
            curBubbleTimer = setBubbleTimer; 
            Instantiate(bubble, transform.position, Quaternion.identity ).GetComponent<bubbleControl>().ShootBubble(player);  
        }
        else
        {
            curBubbleTimer -= Time.deltaTime; 
        }
    }
}
