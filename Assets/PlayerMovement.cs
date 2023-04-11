using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerMovement : MonoBehaviour
{

    public Text HpStatus; 

    public float setHP;

    private float curHP;


    public float speed; 

    public float sprintSpeed; 

    public float dashSpeed; 

    public float jumpHeight;

    public float gravity;

    public float setDashCoolDown; 

    private float currentDashCoolDown = 0.0f; 

    public float setDashSpeed; 

    private float currentDashSpeed =0.0f;

    private Vector3 dashDirection; 

    public float dashSlowDown;

    public CharacterController controller; 

    private bool ifRunning = false;

    private Vector3 gravityVelocity = new Vector3(0.0f,0.0f,0.0f);

    public Transform collisionCheck;
    
    public LayerMask playerLayer;

    private bool canDash = true; 

    void Start()
    {
        curHP = setHP; 
        HpStatus.text = $"HP: {curHP}";
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Debug.Log("hit projectile");
            curHP -= 20;

            HpStatus.text = $"HP: {curHP}";
            Debug.Log("Current HP: ");
            Debug.Log(curHP);
            if(curHP <= 0) 
            {
                Debug.Log("You died!");
            }
        }
    }


    // void OnCollisionEnter(Collision collider)
    // {
    //     Debug.Log("helo");
    //     Debug.Log(collider.gameObject.layer);
    //     if(collider.gameObject.layer == 8)
    //     {
    //         Debug.Log("Collission!!");
    //     }
    // }

    // Update is called once per frame
    void Update()
    {

        if(!canDash)
        {
            if(currentDashCoolDown >= setDashCoolDown)
            {
                canDash = true;
                currentDashCoolDown = 0.0f;
            }
            else
            {
                currentDashCoolDown+= Time.deltaTime; 
            }
        }
        
        if(currentDashSpeed >= speed)
        {
            controller.Move(dashDirection*currentDashSpeed*Time.deltaTime);
            currentDashSpeed -= dashSlowDown*Time.deltaTime;
            return;
        }

        Vector3 movementVector = new Vector3(0.0f,0.0f,0.0f);

        if(Input.GetKey("w"))
        {
            movementVector += transform.forward;
        }
        if(Input.GetKey("s"))
        {
            movementVector += -transform.forward;
        }
        if(Input.GetKey("a"))
        {
            movementVector += -transform.right;
        }
        if(Input.GetKey("d"))
        {
            movementVector += transform.right;
        }
        
       


        Vector3 velocityVec;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            velocityVec  = Vector3.Normalize(movementVector)*sprintSpeed; 
        }
        else 
        {
            velocityVec  = Vector3.Normalize(movementVector)*speed; 
        }

        if(Input.GetKey("c") && canDash)
        {
            //velocityVec = Vector3.Normalize(movementVector)*dashSpeed; 
            if(velocityVec == new Vector3(0.0f,0.0f, 0.0f))
            {
                dashDirection = transform.forward;
            }
            else 
            {
                dashDirection = Vector3.Normalize(velocityVec);
            }
            canDash = false;
            currentDashSpeed = setDashSpeed; 
        }

        if(Physics.CheckSphere(collisionCheck.position,0.5f,playerLayer))
        {
            if(Input.GetKey("space"))
            {
                gravityVelocity = new Vector3(0.0f, jumpHeight,0.0f);
                velocityVec += gravityVelocity;
            }
            else
            {
                gravityVelocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
        else
        {
            gravityVelocity += new Vector3(0.0f, -gravity, 0.0f)*Time.deltaTime;
            
            velocityVec += gravityVelocity; 
        }

        controller.Move(velocityVec*Time.deltaTime);
    }
}
