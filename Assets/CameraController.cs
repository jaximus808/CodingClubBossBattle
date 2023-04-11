using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject camera; 


    public float sensitivity = 100.0f;
    
    private float verticalRotation = 0.0f;
    private float horizontalRotation = 0.0f;
    
    private float clampAngle = 90.0f;
    
    public Transform testTarget; 

    void Start()
    {
        //Locks the mouse and hides it so the player can control it
        Cursor.lockState = CursorLockMode.None; 
        Cursor.lockState = CursorLockMode.Confined; 
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void Look()
    {
        //gets the axis that the mouse has moved
        float mouseVertical = -Input.GetAxis("Mouse Y");
        float mouseHorizontal = Input.GetAxis("Mouse X");

        //rotate our current vertical and horizontal rotiations by the mouse 
        verticalRotation += mouseVertical * sensitivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle); 

        horizontalRotation += mouseHorizontal *sensitivity *Time.deltaTime;

        //rotates the camera up and down on the x axis
        camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0.0f, 0.0f);

        //rotates the player by the y axis to move left or right
        transform.rotation= Quaternion.Euler(0.0f, horizontalRotation, 0.0f);
        
    }
}
