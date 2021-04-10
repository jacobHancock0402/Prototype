using UnityEngine;

public class CheckParticleColl : MonoBehaviour {

	public float timer = 0;
	public AudioSource source;
	public Stickman stick;
	void OnParticleCollision(GameObject obj)
	{
		if(obj.tag == "World" || obj.tag == "Metallic" && Time.time - timer > 1f)
		{
			// uh this playaudio is iffy
			timer = Time.time;
			stick.PlayAudio(source, gameObject);
		}
	}
	// yeah this works and sounds ok
	// next i think add in health bar or some shit, could also rework some sound like footsteps
	// i think we should have one main health bar, but any shots break arms and shit, rendering it useless
	// but this does some amount of damage each time, perhaps like flash screen or some on hit for better effect
}