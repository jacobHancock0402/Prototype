using UnityEngine;

public class BulletManager : MonoBehaviour {
	public float lastPlayTime;
	public bool canPlay;

	void Update()
	{
		if((Time.time - lastPlayTime) > 1f)
		{
			canPlay = true;
		}
		else
		{
			canPlay = false;
		}
	}

}