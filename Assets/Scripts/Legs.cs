using UnityEngine;
using System.Collections;

public class Legs : MonoBehaviour {
    public Stickman Stick;
    public bool jumping;
    public Rigidbody2D rbLLeg;
    public Rigidbody2D rbRLeg;
    public Rigidbody2D Leg;

	void Start() {
        if (gameObject.tag == "rLeg")
        {
            Leg = rbRLeg;
        }
        else
        {
            Leg = rbLLeg;
        }
	}

    void Update()
    {
    }
    public void Jump(Vector3 position)
    {
        // could add vectors as variables to edit in editor

        Vector2 direction = gameObject.transform.position - position;
        Leg.AddForce(direction * 15, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // ai would have to be seperate or reference the parent as it effect all stickmen here
        if (coll.gameObject.tag == "World")
        {
            Stick.jumping = false;
            if (Stick.posturingL && gameObject.tag == "lLeg")
            {
                Stick.posturingL = false;
                Rigidbody2D connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
                if (coll.gameObject.tag == "Rope")
                {
                    DistanceJoint2D Hinge = gameObject.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
                    Hinge.enableCollision = true;
                    Hinge.connectedBody = connectedBody;
                }

                else
                {
                    HingeJoint2D Hinge = gameObject.AddComponent(typeof(HingeJoint2D )) as HingeJoint2D ;
                    Hinge.enableCollision = true;
                    Hinge.connectedBody = connectedBody;
                }
                Stick.NowPosturingL = true;
            }
            else if (Stick.posturingR && gameObject.tag == "rLeg")
            {
                Rigidbody2D connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
                Stick.posturingR = false;
                if (coll.gameObject.tag == "Rope")
                {
                    DistanceJoint2D Hinge = gameObject.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
                    Hinge.enableCollision = true;
                    Hinge.connectedBody = connectedBody;
                }

                else
                {
                    HingeJoint2D Hinge = gameObject.AddComponent(typeof(HingeJoint2D )) as HingeJoint2D ;
                    Hinge.enableCollision = true;
                    Hinge.connectedBody = connectedBody;
                }
                Stick.NowPosturingR = true;
            }

            if (gameObject.tag == "AI")
            {
                AI A = gameObject.transform.parent.gameObject.GetComponent<AI>();
                A.collided = true;
                A.jumping = false;
                A.flying = false;
            }
        }
    }

    void OnCollisionStay2D(Collision2D coll) {
        if(coll.gameObject.tag == "World")
        {
            if (gameObject.tag != "AI")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log(coll.GetContact(0).normal);
                    Debug.Log("hi");
                    Jump(coll.GetContact(0).point);
                    jumping = true;

                    if (Stick.NowPosturingL)
                    {
                        Stick.StopHold(Stick.lLeg, "LeftL");
                    }
                    if (Stick.NowPosturingR)
                    {
                        Stick.StopHold(Stick.rLeg, "RightL");
                    }
                }
                Stick.collided = true;
                Stick.jumping = false;
                Stick.flying = false;
            }
        }
    }

 }