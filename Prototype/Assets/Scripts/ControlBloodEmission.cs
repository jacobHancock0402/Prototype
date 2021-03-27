using UnityEngine;

public class ControlBloodEmission : MonoBehaviour
{
	public ParticleSystem.EmissionModule em;
	public ParticleSystem system;
	public float startTime = 0;

	void Start() {
	    system = GetComponent<ParticleSystem>();
	    em = system.emission;
	}
	void Update() {
	    if(Time.time - startTime > 5f && em.enabled)// && startTime != 0f)
	    {
	        em.enabled = false;
	        if(gameObject.transform.parent == null)
	        {
	        	Destroy(gameObject);
	        }
	    }
	}

	public void ActivateEmission() {
	    system.Play();
	    var sys = system.emission;
	    sys.enabled = true;
	    startTime = Time.time;
	}
}