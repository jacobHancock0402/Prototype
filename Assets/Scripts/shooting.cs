using UnityEngine;
public class shooting : MonoBehaviour {
public bool shoot;
public GameObject BulletSpawner;
public GameObject BulletPrefab;
public int BulletSpeed = 5;
public bool A = false;
public GameObject chainPrefab;
public Vector3 NewestChild;
public Rigidbody2D NewestBody;
public Stickman stick;

void Start()
{
    stick = gameObject.transform.root.GetComponent<Stickman>();
}

void Update()
{
}

public void Fire(float angle, Vector3 direct)
    {
        if (gameObject.layer == 9)
        {
            direct.Normalize();
        Debug.DrawRay(gameObject.transform.position, direct);
        GameObject initObject = null;
        LayerMask mask = LayerMask.GetMask("World");
        Vector2 dir = new Vector2(direct.x, direct.y);
        Vector2 origin = new Vector2(gameObject.transform.parent.position.x, gameObject.transform.parent.position.y);
        RaycastHit2D hit = Physics2D.Raycast(origin, dir, 500f, mask);
        if(hit.collider != null){
            Debug.Log("lhi");
            if (hit.collider.gameObject.tag == "World")
            {
                Debug.Log("Boo");
                Rigidbody2D rigid = gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();
                Vector3 EndPoint = new Vector3(hit.point.x, hit.point.y, gameObject.transform.position.z);
                Vector3 StartPoint = gameObject.transform.root.GetChild(3).position;
                float diffX = EndPoint.x - StartPoint.x;
                float diffY = EndPoint.y - StartPoint.y;
                float numNeeded;
                BoxCollider2D collider = chainPrefab.GetComponent<BoxCollider2D>();

            
        
                //BoxCollider2D collider = link.AddComponent<BoxCollider2D>();
                numNeeded = Mathf.Abs(Mathf.Round(diffX / Mathf.Abs((collider.bounds.max[0] - collider.bounds.center[0])) + Mathf.Abs(diffY / (collider.bounds.max[1] - collider.bounds.center[1]))));
                Debug.Log(numNeeded);
                Debug.Log((EndPoint - StartPoint).magnitude);
                float diffXEach = diffX / numNeeded;
                float diffYEach = diffX / numNeeded;
                stick.NowHoldingR = true;
                for (int i=0;i<numNeeded;i++)
                {
                    int count = gameObject.transform.childCount - 1;
                    GameObject link = Instantiate(chainPrefab) as GameObject;
                    if (i==0)
                    {
                        NewestChild = new Vector3(hit.point.x, hit.point.y, 0);
                        NewestBody = collider.gameObject.GetComponent<Rigidbody2D>();
                        initObject = link;
                    }
                    link.transform.position = new Vector3(
                    NewestChild.x - diffXEach, 
                    NewestChild.y - diffYEach, 
                    gameObject.transform.position.z);
                    link.transform.SetParent(hit.collider.gameObject.transform);
                    Rigidbody2D body = link.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
                    body.mass = 0.1f;
                    if (i!=0)
                    {
                        HingeJoint2D connection = link.AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;
                        connection.connectedBody = NewestBody;
                    }
                    else
                    {
                        FixedJoint2D connection = link.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                        connection.connectedBody = NewestBody;
                    }
                    NewestChild = link.transform.position;
                    NewestBody = body;
                    shoot = false;
                    if(A == false)
                    {
                        //link.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90);
                    }
                    //else
                    //{
                        //link.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90);
                    //}
                    if(i == numNeeded - 1)
                    {
                        CheckColl coll = link.AddComponent(typeof(CheckColl)) as CheckColl;
                        DistanceJoint2D distance = link.AddComponent(typeof(DistanceJoint2D )) as DistanceJoint2D ;
                        distance.autoConfigureDistance = true;
                        distance.autoConfigureConnectedAnchor = true; 
                        distance.connectedBody = initObject.GetComponent<Rigidbody2D>();
                        coll.Player = gameObject.transform.parent.gameObject;
                    }
                    //body.velocity = way * BulletSpeed;

                }
        }
        }
}
else
{
    
        float distance = direct.magnitude;
        Vector2 way = direct / distance;
        direct.Normalize();
        if (shoot == false)
        {
            GameObject b = Instantiate(BulletPrefab) as GameObject;
            shoot = false;
            b.tag = "Bullet";
            b.transform.position = gameObject.transform.GetChild(0).position;
            if(A == false)
            {
                b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90);
            }
            else
            {
                 b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle + 90);
            }
            b.GetComponent<Rigidbody2D>().velocity = way * BulletSpeed;
        }
        }
    }
}