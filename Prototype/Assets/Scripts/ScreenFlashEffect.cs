using UnityEngine;
using UnityEngine.UI;

public class ScreenFlashEffect : MonoBehaviour {

	public Image img;
	public Color color;
	public float startTime = 0f;
	public bool recurse;
	public bool backwards;
	public bool canActivate;
	public float timeSinceActivate;
	public int repeats = 0;
	void Start()
	{
		img = gameObject.GetComponent<Image>();
		color = img.color;
	}
	void Update()
	{
		timeSinceActivate = Time.time - startTime;
		if(timeSinceActivate > 3f)
		{
			canActivate = true;
		}
		else
		{
			canActivate = false;
		}
	}
	public void Activate()
	{
		if(canActivate || recurse)
		{
			//startTime = Time.time;
			recurse = true;
			float increment = 0.05f;
			if(backwards)
			{
				increment = -increment;
			}
			//elapsedTime = Time.time - startTime;
			color.a += increment;
			img.color = color;
			if((color.a < 1f && !backwards) || (color.a > 0f && backwards))
			{
				Invoke("Activate", 0.0001f);
			}
			else if(!backwards)
			{
				backwards = true;
				Invoke("Activate", 0.0001f);
				//elapsedTime += Time.deltaTime;
			}
			// else if(repeats < 3)
			// {
			// 	repeats++;
			// 	backwards = false;
			// 	Invoke("Activate", 0.0001f);
			// }
			else
			{
				backwards = false;
				repeats = 0;
				recurse = false;
			}
		}

		//img.color = Color.Lerp(col2,col1,0.01f);
		//float addition = 1/255;
		// while(color.a < 1)
		// {
		// 	if(Time.time - timer > 2f)
		// 	{
		// 		break;
		// 	}
		// 	if(Random.Range(1, 10) == 1)
		// 	{
		// 		color.a -= addition;
		// 	}
		// 	else
		// 	{
		// 		color.a+=addition;
		// 	}
		// 	img.color = color;
		// }
		//timer = Time.time;
		// while(color.a > 0)
		// {
		// 	if(Time.time - timer > 2f)
		// 	{
		// 		break;
		// 	}
		// 	if(Random.Range(1,10) == 1)
		// 	{
		// 		color.a -= 1;
		// 	}
		// 	else
		// 	{
		// 		color.a-=1;
		// 	}
		// 	img.color = color;
		// }
	}
}