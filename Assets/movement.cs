using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float jumpHeight; 

    public Rigidbody rb; 

    private Vector3 jumpForce; 


    // Start is called before the first frame update
    void Start()
    {
        jumpForce = new Vector3(0,1*jumpHeight,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(jumpForce);
        }
    }

}
