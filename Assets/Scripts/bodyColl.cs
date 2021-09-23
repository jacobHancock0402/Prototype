
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


// effects work , just can't acccess literally anything from the fucking scripts, absolute cockshit


//using Unity.Core.Runtime;

//using UnityEngine.Experimental.Rendering.HDPipeline;
// yeah idk dude just isn't moving, even though path is assigned, i even moved him and still doesn't 
// will move whilst crouching though
public class BodyColl: MonoBehaviour
{
	// public bool collided = false;
	// public float collidedTime = 0f;
	public Stickman stick;
	// void Update()
	// {
	// 	if(collided)
	// 	{
	// 		collidedTime += Time.deltaTime;
	// 	}
	// 	if(collidedTime > 1f)
	// 	{
	// 		collided = false;
	// 		collidedTime = 0f;
	// 		stick.dead = false;
	// 	}
	// }

	// void OnCollisionEnter2D(Collision2D coll)
	// {
	// 	if(gameObject.tag != "rFoot" && gameObject.tag != "lFoot" && coll.gameObject.transform.root != gameObject.transform.root && coll.relativeVelocity.magnitude > 100 && coll.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 5f)
	// 	{
	// 		collided = true;
	// 		stick.dead = true;
	// 	}
	// }
}
