using UnityEngine;

public class DeleteFlash: MonoBehaviour {
    public float startTime;

    void Update()
    {
        if ((Time.time - startTime) > 0.15f)
        {
            Debug.Log("AAAAAAAAAAAA");
            Debug.Log(Time.time);
            Debug.Log(startTime);
            Destroy(gameObject);
            // timing seems alright
            // need to move out front of gun with script i used for gun on hand
            // add sound and maybe randomise / animate flash
            // then Ai with blood, and change bullet
        }
    }
}