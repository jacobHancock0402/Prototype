using UnityEngine;
public class CheckColl: MonoBehaviour {
public GameObject Player;
public bool connect;
public DistanceJoint2D distance;
public Vector3 dir;
public CheckGrab helperFunct;
void Start()
{
    distance = Player.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
    distance.connectedBody = gameObject.GetComponent<Rigidbody2D>();
    connect = true;
    distance.distance = 0.5f;
    dir = Player.transform.position - transform.position;
    helperFunct = Player.GetComponent<CheckGrab>();
    if(Player.tag == "rArm")
    {
        helperFunct.ScaleRightMasses();
    }
    else
    {
        helperFunct.ScaleLeftMasses();
    }

}

void Update()
{
    if(dir.magnitude > 1f)
    {
        dir = Player.transform.position - transform.position;
        gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * 10, ForceMode2D.Impulse);
    }
    //if ((Vector3.Distance(Player.transform.position, gameObject.transform.position) < 15) && connect == true)
    //{
        //DistanceJoint2D distance = Player.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
        //distance.connectedBody = gameObject.GetComponent<Rigidbody2D>();
        //connect = true;
    //}
}

void OnCollisionEnter2D(Collision2D coll)
{
    if ((coll.gameObject.tag == "rArm" || coll.gameObject.tag == "Player" || coll.gameObject.tag == "lArm" || coll.gameObject.tag == "lHand" || coll.gameObject.tag == "rHand") && connect != true)
    {
        //DistanceJoint2D distance = Player.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
       // distance.connectedBody = gameObject.GetComponent<Rigidbody2D>();
        //connect = true;
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