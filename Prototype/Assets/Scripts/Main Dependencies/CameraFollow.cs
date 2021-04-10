using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject playerBody;
	public Stickman player;
	void Update()
	{
		//Debug.LogError(stickBody.transform.position.x);
		//if(!player.dead)
		//{
			transform.position = new Vector3(playerBody.transform.position.x, playerBody.transform.position.y, transform.position.z);
		//}
			// i think it looks better if still follow on death

	}
}