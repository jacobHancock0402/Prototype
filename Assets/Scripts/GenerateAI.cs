using UnityEngine;
using Random = UnityEngine.Random;
using System;
using System.Collections;
using System.Collections.Generic;
public class GenerateAI: MonoBehaviour
{
	public GameObject StickPrefab;
	public List<GameObject> icons;
	public Stickman GenStick;
	public Stickman Player;
	public Vector3 startPos;
	public GameObject posObj;
	public bool started = false;
	public Color upper_colour;
	public Color lower_colour;
	public Rope rArmRope;
	public Rope lArmRope;
	public Rope rLegRope;
	public Rope lLegRope;
	public Sprite eyebrow;
	public Sprite nose;
	public Sprite eye;
	public bool check = false;
	void Start()
	{
		createAI();
		//started = true;
		
		// so this does all colour for the clothes, maybe the leg and upper body colour should be same
		// should maybe have a flag for long sleeve/trousers, so then it also sets the leg/arm colours to lower/upper colours
		// will need to create like a dictionary of colours matching to the different skin tones
		// also then have to do some face gen
		// prob should move this code somewhere else, so it's like a function with a position;
	}
	void Update()
	{
		if(!started)
		{
			createAI();
			started = true;
		}
		else {
			if(!check)
			{
				// pretty sure this works now and efficient
				foreach(_Muscle leg in GenStick.rLegMuscleList)
				{
					leg.bone.gameObject.GetComponent<SpriteRenderer>().color = lower_colour;
				}
				foreach(_Muscle leg in GenStick.lLegMuscleList)
				{
					leg.bone.gameObject.GetComponent<SpriteRenderer>().color = lower_colour;
				}
				check = true;
			}
			rArmRope.limb_colour = upper_colour;
			lArmRope.limb_colour = upper_colour;
			rLegRope.limb_colour = lower_colour;
			lLegRope.limb_colour = lower_colour;
			GenStick.stickGen = this;
		}
		GenStick.playerStick = Player;
		GenStick.playerBody = Player.Body;
		Debug.LogError("iliftweights" + GenStick.NewFaces[0].eye.GetComponent<SpriteRenderer>().sprite.name + this);
	}
	public void createAI()
	{
		if(Player != null)
		{
			foreach(GameObject icon in icons)
			{
				//SpriteRenderer sprite = obj.AddComponent<SpriteRenderer>();
				//sprite.sprite = icon;
				IconFollow fol = icon.GetComponent<IconFollow>();
				fol.stick = GenStick;
				//icon.transform.parent = GenStick.transform.root;
			}
			started = true;
			gameObject.transform.position = posObj.transform.position;
			startPos = posObj.transform.position;
			GameObject Stickman = Instantiate(StickPrefab);
			Stickman.tag = "AI";
			Debug.LogError("xi jingping" + Stickman.transform.position);
			Stickman.transform.position = startPos;
			Debug.LogError("xi" + Stickman.transform.position);
			//Stickman.GetComponent<Stickman>().Player = false;
			GenStick = Stickman.GetComponent<Stickman>();
			//deadhere
			GenStick.dead = true;
			GenStick.Player = false;
			//GenStick.Player = false;
			bool shorts = false;
			bool longSleeve = true;
			if(Random.Range(0, 3) == 0)
			{
				shorts = true;
			}
			if(Random.Range(0, 3) == 0)
			{
				longSleeve = false;
			}
			Color colour1 = new Color(Random.Range(0, 255)/255f,Random.Range(0, 255)/255f,Random.Range(0, 255)/255f);
			Color colour2 = new Color(Random.Range(0, 255)/255f, Random.Range(0, 255)/255f, Random.Range(0, 255)/255f);
			Debug.LogError("hoolightweightbaby" + colour1);
			GenStick.Body.GetComponent<SpriteRenderer>().color = colour1;
			Debug.LogError("highground" + GenStick.rArm.GetComponent<SpriteRenderer>().color);
			GenStick.rArm.GetComponent<SpriteRenderer>().color = colour1;
			GenStick.lArm.GetComponent<SpriteRenderer>().color = colour1;
			GenStick.rLeg.GetComponent<SpriteRenderer>().color = colour2;
			GenStick.lLeg.GetComponent<SpriteRenderer>().color = colour2;
			lArmRope = GenStick.lArm.GetComponent<Rope>();
			rArmRope = GenStick.rArm.GetComponent<Rope>();
			lLegRope = GenStick.lLeg.GetComponent<Rope>();
			rLegRope = GenStick.rLeg.GetComponent<Rope>();

			Debug.LogError("sleeve" + longSleeve);
			Debug.LogError("shorts" + shorts);
			eyebrow = Player.eyebrows[Random.Range(0, Player.eyebrows.Count - 1)];
			eye = Player.eyes[Random.Range(0, Player.eyes.Count - 1)];
			int skin_colourPicker = Random.Range(0, Player.noses.Count - 1);
			nose = Player.noses[skin_colourPicker];
			Debug.LogError("disdecolour" + skin_colourPicker);
			skin_colourPicker = skin_colourPicker/3;
			Sprite face_colour = Player.face_colours[skin_colourPicker];
			Color skin_colour = Player.skin_colours[skin_colourPicker];
			Debug.LogError("colour4" + skin_colourPicker);
			if(longSleeve)
			{
				// this should work but i'm changing the variable of rope script, which has a ref to the prefab, which then affect's all other's
				// i can't really create a copy of prefab, as they all run at same time in start, and the method below these also doesn't work, prob because list hasn't been assigned yet, becuasue rope isn't finsihed
				// 
				rArmRope.limb_colour = colour1;
				lArmRope.limb_colour = colour1;
				upper_colour = colour1;
				// Debug.LogError("fatherletgoofmyhand");
				// foreach(GameObject arm in GenStick.rArmList)
				// {
				// 	arm.GetComponent<SpriteRenderer>().color = colour1;
				// 	Debug.LogError(arm.GetComponent<SpriteRenderer>().color + "Sayless");
				// }
				// foreach(GameObject arm in GenStick.lArmList)
				// {
				// 	arm.GetComponent<SpriteRenderer>().color = colour1;
				// }

			}
			else {
				upper_colour = skin_colour;
				rArmRope.limb_colour = skin_colour;
				lArmRope.limb_colour = skin_colour;
			}
			//upper_colour = skin_colour;
			if(!shorts)
			{
				GenStick.rLeg.GetComponent<Rope>().limb_colour = colour2;
				GenStick.lLeg.GetComponent<Rope>().limb_colour = colour2;
				lower_colour = colour2;
				foreach(GameObject leg in GenStick.rLegList)
				{
					leg.GetComponent<SpriteRenderer>().color = colour2;
				}
				foreach(GameObject leg in GenStick.lLegList)
				{
					leg.GetComponent<SpriteRenderer>().color = colour2;
				}
			}
			else {
				rLegRope.limb_colour = skin_colour;
				lLegRope.limb_colour = skin_colour;
				lower_colour = skin_colour;
			}
			foreach(Face face in GenStick.NewFaces)
			{
				//GenStick.EditorFaces.Add(face);
				Debug.LogError("thename" + nose + skin_colour);
				face.eyebrow.GetComponent<SpriteRenderer>().sprite = eyebrow;
				face.eye.GetComponent<SpriteRenderer>().sprite = eye;
				face.nose.GetComponent<SpriteRenderer>().sprite = nose;
				Debug.LogError("yeahwereallydiditwithnofeatures" + face.eye.GetComponent<SpriteRenderer>().sprite.name + GenStick.gameObject.name);
				Destroy(face.nose);
				//face.eyebrow.transform.parent.GetComponent<SpriteRenderer>().sprite = face_colour;
			}
			// yeah don't know what's happening here, it sets the sprite, but then for some reason sets to default one, no idea why wtff
			GenStick.Head.GetComponent<SpriteRenderer>().sprite = face_colour;
			started = true;
		}
	}
}