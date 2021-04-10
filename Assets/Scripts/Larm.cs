using UnityEngine;
using System.Collections;
public class Larm: MonoBehaviour
{
    public bool active;
    public Stickman Stick;
    public HingeJoint2D Anchor;

    public GameObject player;
    
    void Start() {
        Stick = player.GetComponent<Stickman>();
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon" || coll.gameObject.tag == "Rope") && Stick.holdingL)
        {
            Stick.holdingL = false;
            Rigidbody2D connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
            if (coll.gameObject.tag == "Rope")
            {
                DistanceJoint2D Hinge = gameObject.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
                Hinge.connectedBody = connectedBody;
            }

            else
            {
                HingeJoint2D Hinge = gameObject.AddComponent(typeof(HingeJoint2D )) as HingeJoint2D ;
                Hinge.connectedBody = connectedBody;
            }
            Stick.NowHoldingL = true;
            
            
        }

        if ((coll.gameObject.tag == "World" || coll.gameObject.tag == "Gun" || coll.gameObject.tag == "Weapon") && Stick.grabbingL) {
            Stick.grabbingR = true;
            gameObject.transform.rotation = Quaternion.Euler(0,0, -720);
            coll.gameObject.transform.SetParent(gameObject.transform, true);
            Collider2D collider = coll.gameObject.GetComponent<Collider2D>();
            // might have to add value on + change wether adding or minus depending on direction the arm is in // add a bit of y and x depending on angle like multiply init
            coll.gameObject.transform.position = new Vector3((gameObject.transform.position.x + Mathf.Abs(collider.bounds.max[0] - collider.bounds.center[0])), gameObject.transform.position.y, gameObject.transform.position.z);
            Destroy(coll.gameObject.GetComponent<Rigidbody2D>());
            Stick.NowGrabbingR = true;
            //Stick.grabbingL = false;
            //gameObject.transform.rotation = Quaternion.Euler(0,0, 0);
            //coll.gameObject.transform.SetParent(gameObject.transform, true);
            //coll.gameObject.transform.position = new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y, gameObject.transform.position.z);
            //Destroy(coll.gameObject.GetComponent<Rigidbody2D>());
            //Stick.NowGrabbingL = true;
        }


    }




}