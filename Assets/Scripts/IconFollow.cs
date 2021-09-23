using UnityEngine;
using UnityEngine.UI;

public class IconFollow : MonoBehaviour {
	public Stickman stick;
	public Stickman ai;
	public SpriteRenderer sprite;
	public Color color;
	public float health;
	public float lastHealth;
	public float startingXScale;
	public float startingXPos;
	public float startPos;
	public IconFollow underBox;
	public GameObject trackerObj;
	void Start()
	{
		sprite = gameObject.GetComponent<SpriteRenderer>();
		startingXScale = transform.localScale.x;
		startingXPos = transform.localPosition.x;
		startPos = sprite.bounds.min.x;
	}
	void Update()
	{
		health = stick.health;
		color = sprite.color;
		//transform.parent.position = new Vector3(stick.Head.transform.position.x, stick.Head.transform.position.y, stick.Head.transform.position.z);
		if(sprite.sprite.name == "progress")
		{
			Debug.LogError("owainhopkins" + sprite.bounds.extents.x + " " + startingXPos);
		}
		transform.localPosition = new Vector3 (startingXPos + (sprite.bounds.extents.x/transform.localScale.x), transform.localPosition.y, transform.localPosition.z);
		Debug.LogError("thatinglyfeeling" + transform.parent.name);
		if(sprite.sprite.name == "progress" && sprite.gameObject.name != "progress")
		{
			if(health > 0)
			{
				sprite.color = new Color(0,0,0,0);
				float multiplier = (stick.startingHealth - health) / stick.startingHealth;
				Debug.LogError("iaminfantuatedwithjoyhall" + sprite.bounds.size.x + " " + multiplier);
				transform.localScale = new Vector3(startingXScale * (1-multiplier), transform.localScale.y, transform.localScale.z);
			}
			// idk was working now now less go gay
			//sprite.gameObject.transform.localPosition = new Vector3(startPos + ((1-multiplier)*sprite.bounds.extents.x),sprite.gameObject.transform.localPosition.y ,sprite.gameObject.transform.localPosition.z );
		}
		if(((stick.firing && sprite.sprite.name == "Exclamation") || (stick.Alerted && sprite.sprite.name == "Questiokn") || (sprite.sprite.name == "progress")) && health > 0)
		{
			color.a = 255;
			sprite.color = color;
		}
		else {
			color.a = 0;
			sprite.color = color;
		}
		lastHealth = health;
	}
}