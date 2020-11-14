


using UnityEngine;
using System.Collections;
public class AI: MonoBehaviour
{
 
    public _Muscl[] muscles;
    // this might cause problems if die midair, like you can't jump after spawn. = make reset
    public bool jumping = false;
    public string State = "Patrol";
 
    public bool Right;
    public bool Left;
    public bool stretch = false;
    public bool flying = false;

    public GameObject cone;
    public GameObject rArm;
    public Rigidbody2D rbRIGHT;
    public bool collided = false;
    public Rigidbody2D rbLEFT;
    public Rigidbody2D rbLLeg;
    public Rigidbody2D rbRLeg;
    public Rigidbody2D rbHead;
    public string Direction = "Right";
    public float time = 0;
    public GameObject Player;
    public bool shooting;
    public Vector3 LastKnownPos;
    public Vector3 position;
 
    public Vector2 WalkRightVector;
    public Vector2 WalkLeftVector;
 
    private float MoveDelayPointer;
    public float MoveDelay;
    public float tim;


 
    // Update is called once per frame

    // the movement from gun is caused by collider but , without it, arm goes weird
    // and start going inwards to body
    // this is caused by the rigidbody trying to push back
    // minimising mass seems to get rid of unnauathorised movement but still jank
    // gravity scale = 0 means jank is minimal so it's clear rigid body is at fault
    private void Update()
    {
        foreach (_Muscl muscle in muscles)
        {   
            if (muscle.bone)
            {

                // change this to shooting arm is using 2hands
                if (muscle.bone.gameObject.tag != "rArm");
                {
                    muscle.ActivateMuscle();
                }
            }
        }
        Vector3 direction = gameObject.transform.GetChild(1).position - Player.transform.position;
        if (direction.magnitude < 10f) 
        {
            State = "Chasing";
        }

        else if (direction.magnitude >= 10f && State == "Chasing") {
            State = "Searching";
            LastKnownPos = Player.transform.position;
            tim = Time.time;
        }

        else if (State != "Searching" || Time.time - tim > 2)
        {
            State = "Patrol";
        }
        if (State == "Chasing" || State == "Searching"){
            if (State == "Chasing")
            {
                position = Player.transform.position;
            }

            else
            {
                position = LastKnownPos;
                if (Right || Left)
                {
                    tim = Time.time;
                }
            }
            Transform body = gameObject.transform.GetChild(0);
            if (position.x < body.position.x - 2)
            {
                Right = false;
                Left = true;
            }
            // haven't consider wether they are equa
            else if (position.x > body.position.x + 2)
            {
                Right = true;
                Left = false;
            }
            // use already given position vector and try to access from gun and then creating a new variable to calculate angle with
            else
            {
                Right = false;
                Left = false;
            }

            if(Time.time - time > 1)
            {
                rArm.GetComponent<Gun>().shooting = true;
                time = Time.time;
            }


        }

        else if (State == "Patrol")
        {
            Right = false;
            Left = false;
            time = Time.time;
        }
        else
        {
            time = Time.time;
        }

        while (Right == true && Left == false && Time.time > MoveDelayPointer)
        {
            Invoke("Step1Right", 0f);
            Invoke("Step2Right", 0.085f);
            MoveDelayPointer = Time.time + MoveDelay;
        }
 
        while (Left == true && Right == false && Time.time > MoveDelayPointer)
        {
            Invoke("Step1Left", 0f);
            Invoke("Step2Left", 0.085f);
            MoveDelayPointer = Time.time + MoveDelay;
        }
        collided = false;
    }

    public void Step1Right()
    {
        if(rbRIGHT)
        {
            rbRIGHT.AddForce(WalkRightVector, ForceMode2D.Impulse);
        }

        if(rbLEFT)
        {
            rbLEFT.AddForce(WalkRightVector * -0.5f, ForceMode2D.Impulse);
        }
        
    }
 
    public void Step2Right()
    {
        if(rbLEFT)
        {
            rbLEFT.AddForce(WalkRightVector, ForceMode2D.Impulse);
        }

        if (rbRIGHT)
        {
            rbRIGHT.AddForce(WalkRightVector * -0.5f, ForceMode2D.Impulse);
        }
    }
 
    public void Step1Left()
    {
        if (rbRIGHT)
        {
            rbRIGHT.AddForce(WalkLeftVector, ForceMode2D.Impulse);
        }
        if (rbLEFT)
        {
            rbLEFT.AddForce(WalkRightVector * -0.5f, ForceMode2D.Impulse);
        }
    }
 
    public void Step2Left()
    {
        if (rbLEFT)
        {
            rbLEFT.AddForce(WalkLeftVector, ForceMode2D.Impulse);
        }

        if (rbRIGHT)
        {
            rbRIGHT.AddForce(WalkLeftVector * -0.5f, ForceMode2D.Impulse);
        }
    }
    }
// implement a check if collision is a world object

// if so, check if climable and then pass that for the ai to jump

[System.Serializable]
public class _Muscl
{
    public Rigidbody2D bone;
    public float restRotation;
    public float force;
 
    public void ActivateMuscle()
    {   

        GameObject gameobj = bone.gameObject;
        AI a = gameobj.transform.root.gameObject.GetComponent<AI>();
        if (a.flying == false || restRotation == 90 || restRotation == -90)
        bone.MoveRotation(Mathf.LerpAngle(bone.rotation, restRotation, force * Time.deltaTime));
    }
    //if (Direction == "Right")
        //{
        //rbLLeg.AddForce(new Vector2(-10f,5f), ForceMode2D.Impulse);
        //rbRLeg.AddForce(new Vector2(-10f,5f), ForceMode2D.Impulse);
        //rbHead.AddForce(new Vector2(20f,0f), ForceMode2D.Impulse);
        //}

        //if(Direction == "Left")
        //{
        //rbLLeg.AddForce(new Vector2(10f,5f), ForceMode2D.Impulse);
        //rbRLeg.AddForce(new Vector2(10f,5f), ForceMode2D.Impulse);
        //rbHead.AddForce(new Vector2(-20f,0f), ForceMode2D.Impulse);
        //}
}