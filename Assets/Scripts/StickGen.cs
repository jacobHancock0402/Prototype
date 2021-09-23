// using UnityEngine;
// using Random = UnityEngine.Random;
// using System;
// using System.Collections;
// using System.Collections.Generic;
// public class GenerateAI: MonoBehaviour
// {
// 	public GameObject StickPrefab;
// 	public Stickman GenStick;
// 	public Stickman Player;
// 	void Start()
// 	{
// 		Instantiate(StickPrefab);
// 		GenStick = StickPrefab.GetComponent<Stickman>();
// 		Color colour1 = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
// 		Color colour2 = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
// 		GenStick.Body.GetComponent<SpriteRenderer>().color = colour1;
// 		GenStick.rArm.GetComponent<SpriteRenderer>().color = colour1;
// 		GenStick.lArm.GetComponent<SpriteRenderer>().color = colour1;
// 		GenStick.rLeg.GetComponent<SpriteRenderer>().color = colour2;
// 		GenStick.lLeg.GetComponent<SpriteRenderer>().color = colour2;
// 		Sprite eyebrow = Player.eyebrows[Random.Range(0, Player.eyebrows.Count - 1)];
// 		Sprite eye = Player.eyes[Random.Range(0, Player.eyes.Count - 1)];
// 		Sprite nose = Player.noses[Random.Range(0, Player.noses.Count - 1)];
// 		foreach(Face face in Player.EditorFaces)
// 		{
// 			GenStick.EditorFaces.Add(face);
// 			GenStick.EditorFaces[GenStick.EditorFaces.Count - 1].eyebrow.GetComponent<SpriteRenderer>().sprite = eyebrow;
// 			GenStick.EditorFaces[GenStick.EditorFaces.Count - 1].eye.GetComponent<SpriteRenderer>().sprite = eye;
// 			GenStick.EditorFaces[GenStick.EditorFaces.Count - 1].nose.GetComponent<SpriteRenderer>().sprite = nose;

// 		}
// 		// so this does all colour for the clothes, maybe the leg and upper body colour should be same
// 		// should maybe have a flag for long sleeve/trousers, so then it also sets the leg/arm colours to lower/upper colours
// 		// will need to create like a dictionary of colours matching to the different skin tones
// 		// also then have to do some face gen
// 		// prob should move this code somewhere else, so it's like a function with a position;
// 	}
// }