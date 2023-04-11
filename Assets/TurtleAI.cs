using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAI : MonoBehaviour
{


    //0: idle, 1: attacking
    private int state = 0; 

    //0:scald, 1: missle, 2:slam, 3:bubble
    private int currentAtk = 0; 

    private float currentIdleTimer = 0; 

    private float setTimerIdle = 0; 
    
    private bool canAtk = false; 

    private bool canFire = true; 
    
    private float setFireTimer = 0; 
    private float curFireTimer = 0; 

    private int numFired = 0; 

    public float timeForSlamTravel; 

    private float currentSlamTime; 

    public bool completeingSetup = false; 

    private Vector3 targetSlamPos; 

    private Vector3 initalSlamPos; 

    public GameObject player; 

    public GameObject bubble; 

    public Transform[] bubbleSpawns;

    public GameObject[] bubblesObs; 

    void handleTimers() 
    {
        if(!canAtk)
        {
            currentIdleTimer += Time.deltaTime; 
            if(currentIdleTimer >= setTimerIdle)
            {
                canAtk = true;
                currentIdleTimer = 0; 
            }
        }

        if(!canFire)
        {
            curFireTimer += Time.deltaTime; 
            if(curFireTimer >= setFireTimer)
            {
                canFire = true;
                curFireTimer = 0; 
            }
        }

        if(currentSlamTime <= timeForSlamTravel)
        {
            currentSlamTime += Time.deltaTime; 
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        setTimerIdle = Random.Range(3.0f, 6.0f); 
        bubblesObs = new GameObject[bubbleSpawns.Length];

        currentSlamTime = timeForSlamTravel; 
    }

    void BubbleSetUp()
     {
        for(int i = 0; i < bubblesObs.Length; i++)
        {
            bubblesObs[i] = Instantiate(bubble, bubbleSpawns[i].position,  Quaternion.identity, gameObject.transform);
        }
    }

    void BubbleAtk()
    {
        for(int i = 0; i < bubblesObs.Length; i++)
        {
            bubblesObs[i].GetComponent<bubbleControl>().ShootBubble(player.transform);
        }
        numFired++; 
        if(numFired < 5)
        {

            BubbleSetUp(); 
            canFire = false;
            setFireTimer = 0.7f;
        }
        else
        {
            numFired = 0;
            state = 1;
            //add currentAtk random here later
            setTimerIdle = Random.Range(3.0f, 6.0f); 
            canAtk = false; 

            currentAtk = Random.Range(2,4);
        }
        
    }

    void TurtleTravel()
    {
        transform.position = initalSlamPos + (targetSlamPos-initalSlamPos) * Mathf.Sin(Mathf.PI/2 *(currentSlamTime/timeForSlamTravel )); 

        if(currentSlamTime >= timeForSlamTravel)
        {
            completeingSetup = false; 

        }
    }

    void SlamSetup()
    {
        currentSlamTime = 0; 
        completeingSetup = true; 
        initalSlamPos = transform.position; 
        targetSlamPos = player.transform.position  + new Vector3(0f, 5f, 0) ; 
    }

    // Update is called once per frame
    void Update()
    {
        //idle state
        handleTimers();
        if(state == 0)
        {
            if(canAtk)
            {
                currentAtk = Random.Range(2,4);
                //currentAtk = 3; 
                //done
                state = 1; 
                Debug.Log("TIME TO ATTACK!!");
                Debug.Log($"It has been {currentIdleTimer}ms");
                //face the player 

            }
        }
        //attacking
        else if(state == 1)
        {
            if(canAtk)
            { 
                transform.LookAt(player.transform.position);

                if(currentAtk == 0 )
                {
                    Debug.Log("SCALD ATTACK!");
                    //setTimerIdle = 3.0f; 
                    setFireTimer = 1;
                }
                else if(currentAtk == 1)
                {
                    Debug.Log("MISSILE ATTACK!");
                    //setTimerIdle = 1.0f;
                    setFireTimer = 1;
                }
                else if(currentAtk == 2)
                {
                    Debug.Log("SLAM ATTACK!");
                    SlamSetup(); 
                    setFireTimer = 2;
                }
                else if(currentAtk == 3)
                {   
                    Debug.Log("BUBLE ATTACK!");
                    setFireTimer = 2;
                    BubbleSetUp(); 
                }   
                state = 2;
                //currentAtk = Random.Range(0,4);
                //currentAtk = 3;
                canAtk = false;

                canFire = false; 
            
            }
        }
        else if(state == 2)
        {

            transform.LookAt(player.transform.position);
            if(completeingSetup)
            {
                if(currentAtk == 2)
                {
                    TurtleTravel();
                }
            }
            if(canFire)
            {
                if(currentAtk == 3 )
                {
                    BubbleAtk(); 
                }

            }
        }
    }
}
