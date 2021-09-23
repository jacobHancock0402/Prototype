using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]
public class FeetMove: MonoBehaviour
{
	public Stickman stickman;
	public GameObject leg;
	public GameObject body;
	void Start()
	{
		// LayerMask mask = LayerMask.GetMask("World");
		// //Vector3 n = (new Vector3(leg.transform.position.x, body.transform.position.y, leg.transform.position.z)
		// RaycastHit2D hit = Physics2D.Raycast((new Vector3(leg.transform.position.x, body.transform.position.y, leg.transform.position.z)), -leg.transform.up, 10, mask);
		// if(hit.collider != null)
		// {
		// 	transform.position = hit.point;
		// }
	}
	void Update()
	{
		LayerMask mask = LayerMask.GetMask("World");
		Vector3 n = new Vector3(leg.transform.position.x, body.transform.position.y, leg.transform.position.z);
		RaycastHit2D hit = Physics2D.Raycast((new Vector3(leg.transform.position.x, body.transform.position.y, leg.transform.position.z)), -leg.transform.up, 10, mask);
		if(hit.collider != null)
		{
			transform.position = hit.point;
		}
		if(!stickman.HasCollidedWalk)
		{
			//leg.GetComponent<Rigidbody2D>().AddForce(n * 5, ForceMode2D.Impulse);
		}
		// mutliple volumes doesn't work, idk why
		// flash pos left fine, although only works on that side
		// ar still can't shoot
	}
}
