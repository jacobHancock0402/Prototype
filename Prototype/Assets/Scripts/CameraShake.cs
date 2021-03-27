using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraShake : MonoBehaviour{
	public IEnumerator Shake()
	{
		float timeElapsed = 0f;
		float startTime = Time.time;
		int counter = 0;
		Vector3 originalPos = transform.position;
		while(counter < 1000f)
		{
			counter++;
			timeElapsed = Time.time - startTime;
			float x = UnityEngine.Random.Range(-5f,5f);
			float y = UnityEngine.Random.Range(-5f,5f);
			transform.position = new Vector3(transform.position.x + x, transform.position.y+y, transform.position.z);
			yield return null;
		}
		transform.position = originalPos;
	}
}