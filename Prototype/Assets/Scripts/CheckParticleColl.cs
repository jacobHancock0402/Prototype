using UnityEngine;

public class CheckParticleColl : MonoBehaviour {

	public float timer = 0;
	public AudioSource source;
	void OnParticleCollision(GameObject obj)
	{
		if(obj.tag == "World" || obj.tag == "Metallic" && Time.time - timer > 1f)
		{
			timer = Time.time;
			source.PlayOneShot(source.clip);
		}
	}
	// yeah this works and sounds ok
	// next i think add in health bar or some ***REMOVED***, could also rework some sound like footsteps
	// i think we should have one main health bar, but any shots break arms and ***REMOVED***, rendering it useless
	// but this does some amount of damage each time, perhaps like flash screen or some on hit for better effect
}