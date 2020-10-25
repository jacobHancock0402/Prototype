using UnityEngine;
public class CheckColl: MonoBehaviour {
public GameObject Player;
public bool connect;
void Start()
{

}

void Update()
{
    if ((Vector3.Distance(Player.transform.position, gameObject.transform.position) < 15) && connect == true)
    {
        //DistanceJoint2D distance = Player.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
        //distance.connectedBody = gameObject.GetComponent<Rigidbody2D>();
        //connect = true;
    }
}

void OnCollisionEnter2D(Collision2D coll)
{
    if ((coll.gameObject.tag == "rArm" || coll.gameObject.tag == "Player" || coll.gameObject.tag == "lArm") && connect != true)
    {
        DistanceJoint2D distance = Player.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
        distance.connectedBody = gameObject.GetComponent<Rigidbody2D>();
        connect = true;
    }
}

//public void OnCollisionEnter2D(Collision2D coll)
//{
    //collided = true;
    //gameObject.transform.parent.gameObject.GetComponent<GrappleGun>().collided = true;
    //HingeJoint2D Hinge = gameObject.AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;
    //Hinge.connectedBody = coll.gameObject.GetComponent<Rigidbody2D>();
//}
}