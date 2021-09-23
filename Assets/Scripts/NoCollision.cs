using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
 
public class NoCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public Stickman Stick;
    public Collider2D[] ignoredColliders;
    public Collider2D Collider;
    public int stickInstanceID;
 
    // the different feet can't collide as it feels like doesnt trip as legs stay ahead of eachother
    // but shouldn't they move back and forth?
    // but looks too weak and weird with overlap otherwise
    // two options are at bottom
    // if collision sort out directions, as sometimes will collide whe
    // i.e left goes right when changing direction to right, when right shoudl go first
    void Start() {
        Stick = gameObject.transform.root.gameObject.GetComponent<Stickman>();
        Collider = gameObject.GetComponent<Collider2D>();
        ignoredColliders = new Collider2D[0];
        stickInstanceID = Stick.gameObject.GetInstanceID();
    }
    void Update() {
        if(Collider == null)
        {
            Collider = gameObject.GetComponent<Collider2D>();
        }
        if(Stick)
        {
            if(!Stick.PreHoldingR && gameObject.tag == "rArm" && (Stick.NowHoldingR || Stick.holdingR))
            {
                foreach(Collider2D collider2 in ignoredColliders)
                {
                    Physics2D.IgnoreCollision(Collider, collider2, false);
                }
            }
            if(!Stick.PreHoldingL && gameObject.tag == "lArm" && (Stick.NowHoldingL || Stick.holdingL))
            {
                foreach(Collider2D collider2 in ignoredColliders)
                {
                    Physics2D.IgnoreCollision(Collider, collider2, false);
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(Collider != null)
        {
            if(Stick)
            {
                if(((Stick.PreHoldingR) && gameObject.tag == "rArm") || (Stick.PreHoldingL && gameObject.tag == "lArm") || (Stick.NowHoldingR && gameObject.tag == "rArm" && coll.gameObject.tag != "Body" && coll.gameObject.tag != "rArm") || (Stick.NowHoldingL && gameObject.tag == "lArm" && coll.gameObject.tag != "Body" && coll.gameObject.tag != "lArm"))
                {
                    //Physics2D.IgnoreCollision(Collider, coll.gameObject.GetComponent<Collider2D>());
                    //Debug.Log("Niggigotthishere");
                    //Array.Resize(ref ignoredColliders, ignoredColliders.Length + 1);
                    //ignoredColliders[ignoredColliders.Length - 1] = coll.collider;
                }
            }
            
            if ((coll.gameObject.transform.root.gameObject.GetInstanceID() == stickInstanceID) && ((coll.gameObject.tag == "Head" || coll.gameObject.tag == "Player" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "lArm" || (coll.gameObject.tag == "lLeg" && gameObject.tag != "lFoot") || (coll.gameObject.tag == "rLeg" && gameObject.tag != "rFoot" ) || coll.gameObject.tag == "LlLeg" || coll.gameObject.tag == "LrLeg" || coll.gameObject.tag == "Head" || (coll.gameObject.tag == "Body" &&  gameObject.tag != "rLeg" && gameObject.tag != "lLeg") || (coll.gameObject.tag == "rFoot" && gameObject.tag != "rLeg" ) || (coll.gameObject.tag == "lFoot" && gameObject.tag != "lLeg" )) && (coll.gameObject.tag != gameObject.tag || gameObject.tag == "rArm" || gameObject.tag == "lArm")))
            {    
                Physics2D.IgnoreCollision(Collider, coll.gameObject.GetComponent<Collider2D>());
                if(gameObject.tag == "lArm")
                {
                    Debug.Log("imsleepcuh");
                }
            }
        }
            
    }
    // still can't climb a wall
    // arm gets clamped between wall = hard to move back up
    // on 2nd grab when hanging on wall, not enough force to pull body to arm
    // can't get over ledge as again, not enough power, but also prob the angle body comes in at
     //if ((coll.gameObject.tag == "Player" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "lArm" ||coll.gameObject.tag == "Gun" || (coll.gameObject.tag == "lLeg" && gameObject.tag != "lFoot" && gameObject.tag != "rLeg") || (coll.gameObject.tag == "rLeg" && gameObject.tag != "rFoot" && gameObject.tag != "lLeg") || coll.gameObject.tag == "LlLeg" || coll.gameObject.tag == "LrLeg" || coll.gameObject.tag == "Head" || coll.gameObject.tag == "Body" || (coll.gameObject.tag == "rFoot" && gameObject.tag != "rLeg" && gameObject.tag != "lFoot") || (coll.gameObject.tag == "lFoot" && gameObject.tag != "lLeg" && gameObject.tag != "rFoot" )) && (coll.gameObject.tag != gameObject.tag) ) || coll.gameObject.tag == "rArm"))
     //if ((coll.gameObject.tag == "Player" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "lArm" ||coll.gameObject.tag == "Gun" || (coll.gameObject.tag == "lLeg" && gameObject.tag != "lFoot") || (coll.gameObject.tag == "rLeg" && gameObject.tag != "rFoot" ) || coll.gameObject.tag == "LlLeg" || coll.gameObject.tag == "LrLeg" || coll.gameObject.tag == "Head" || coll.gameObject.tag == "Body" || (coll.gameObject.tag == "rFoot" && gameObject.tag != "rLeg" ) || (coll.gameObject.tag == "lFoot" && gameObject.tag != "lLeg"  )) && (coll.gameObject.tag != gameObject.tag) )// || coll.gameObject.tag == "rArm"))
}