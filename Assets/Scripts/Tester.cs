using UnityEngine;
using UnityEngine.UI;

public class Tester: MonoBehaviour {
	void Start()
	{
		Debug.LogError("frontbumper" + transform.localScale);
	}
	void Update()
	{
		if(transform.parent == null)
		{
			Debug.LogError("frontbumper" + transform.localScale);
		}
	}
}