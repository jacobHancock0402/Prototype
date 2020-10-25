using UnityEngine;
using System.Collections;
public class ViewConeL: MonoBehaviour
{
    public bool active;


    public AI obj;
    public float time = 0f;

    void Start() {
        obj = gameObject.transform.root.gameObject.GetComponent<AI>();


    }
    // need to change to parented object if multiple AIs
    void Update() {

        if (obj.Right)
        {
            active = false;
        }
        else
        {
            active = true;
        }

        if (Time.time - time > 4)
        {

            obj.State = "Patrol";
        }


    }
    void OnCollisionEnter2D(Collision2D coll) {
        Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag == "Detection" && active)
        {
            obj.State = "Chasing";
            Debug.Log(obj.State);
        }
    }

    void OnCollisionExit2D(Collision2D coll){
        if (coll.gameObject.tag == "Detection" && active)
        {
            obj.State = "Patrol";
        }
    }



}
