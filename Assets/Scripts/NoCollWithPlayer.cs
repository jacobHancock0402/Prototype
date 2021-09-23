using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
 
public class NoCollWithPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Stickman Stick;
    public Stickman Player;
    public Collider2D[] ignoredColliders;
    public Collider2D Collider;
    public int numPChilds;
    public Collider2D[] pChildsColl;
    public bool ignorePlayer = false;
    public bool flag = false;
 
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
        numPChilds = Player.muscles.Length;
        pChildsColl = new Collider2D[numPChilds];
        for(int i=0;i<numPChilds;i++)
        {
            pChildsColl[i] = Player.muscles[i].bone.gameObject.GetComponent<Collider2D>();
        }

    }
    void Update() {
        if(!flag)
        {
            int numPChilds = Player.muscles.Length;
            pChildsColl = new Collider2D[numPChilds];
            for(int i=0;i<numPChilds;i++)
            {
                pChildsColl[i] = Player.muscles[i].bone.gameObject.GetComponent<Collider2D>();
            }
            flag = true;
        }
        else if(!Player.grabbingR && !Player.grabbingL &&!Player.holdingL &&!Player.holdingR)
        {
            if(!ignorePlayer)
            {
                for(int i=0;i<numPChilds;i++)
                {
                    Physics2D.IgnoreCollision(pChildsColl[i], Collider);
                }
            }
            ignorePlayer = true;
        }
        else
        {
            if(ignorePlayer)
            {
                for(int i=0;i<numPChilds;i++)
                {
                    Physics2D.IgnoreCollision(pChildsColl[i], Collider, false);
                }
            }
            ignorePlayer = false;
        }
    }
    // still can't climb a wall
    // arm gets clamped between wall = hard to move back up
    // on 2nd grab when hanging on wall, not enough force to pull body to arm
    // can't get over ledge as again, not enough power, but also prob the angle body comes in at
     //if ((coll.gameObject.tag == "Player" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "lArm" ||coll.gameObject.tag == "Gun" || (coll.gameObject.tag == "lLeg" && gameObject.tag != "lFoot" && gameObject.tag != "rLeg") || (coll.gameObject.tag == "rLeg" && gameObject.tag != "rFoot" && gameObject.tag != "lLeg") || coll.gameObject.tag == "LlLeg" || coll.gameObject.tag == "LrLeg" || coll.gameObject.tag == "Head" || coll.gameObject.tag == "Body" || (coll.gameObject.tag == "rFoot" && gameObject.tag != "rLeg" && gameObject.tag != "lFoot") || (coll.gameObject.tag == "lFoot" && gameObject.tag != "lLeg" && gameObject.tag != "rFoot" )) && (coll.gameObject.tag != gameObject.tag) ) || coll.gameObject.tag == "rArm"))
     //if ((coll.gameObject.tag == "Player" || coll.gameObject.tag == "rArm" || coll.gameObject.tag == "lArm" ||coll.gameObject.tag == "Gun" || (coll.gameObject.tag == "lLeg" && gameObject.tag != "lFoot") || (coll.gameObject.tag == "rLeg" && gameObject.tag != "rFoot" ) || coll.gameObject.tag == "LlLeg" || coll.gameObject.tag == "LrLeg" || coll.gameObject.tag == "Head" || coll.gameObject.tag == "Body" || (coll.gameObject.tag == "rFoot" && gameObject.tag != "rLeg" ) || (coll.gameObject.tag == "lFoot" && gameObject.tag != "lLeg"  )) && (coll.gameObject.tag != gameObject.tag) )// || coll.gameObject.tag == "rArm"))
}