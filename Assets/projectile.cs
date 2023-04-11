using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collider)
    {
        Debug.Log("helo");
        Debug.Log(collider.gameObject.layer);
        if(collider.gameObject.layer == 8)
        {
            Debug.Log("Collission!!");
        }
    }
}
