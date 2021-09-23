using UnityEngine;
using System;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {
	public float lastPlayTime;
	public bool canPlay;
	public List<GameObject> bullets;
	public int maxBullets = 50;
	void Start()
	{
		bullets = new List<GameObject>();
	}
	void Update()
	{
		if((Time.time - lastPlayTime) > 0.5f)
		{
			canPlay = true;
		}
		else
		{
			canPlay = false;
		}
		if(bullets.Count > maxBullets)
		{
			//Debug.LogError(bullets.Count);
			Destroy(bullets[0]);
			bullets.Remove(bullets[0]);
		}
	}

}