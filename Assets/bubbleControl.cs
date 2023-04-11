using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleControl : MonoBehaviour
{
    public float forceVal;

    private bool fired; 

    public Rigidbody rb; 
    // Start is called before the first frame update
    void Start()
    {
        // transform.LookAt(target);
        // rb.AddForce(transform.forward * forceVal);
        fired = false; 
    }


    void OnCollisionEnter(Collision collision)
    {
        if(!fired) return; 
        Destroy(gameObject);    
    }

    public void ShootBubble(Transform target)
    {
        transform.LookAt(target);
        rb.AddForce(transform.forward * forceVal);
        fired = true; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
