using UnityEngine;

public class ControlFlashEmission : MonoBehaviour {

public ParticleSystem.EmissionModule em;
public ParticleSystem system;
public float startTime = 0;

void Start() {
    system = GetComponent<ParticleSystem>();
    em = GetComponent<ParticleSystem>().emission;
}
void Update() {
    if(Time.time - startTime > 0.5f)
    {
        em.enabled = false;
    }
}

public void ActivateEmission() {
    system.Play();
    em.enabled = true;
    startTime = Time.time;
}
}