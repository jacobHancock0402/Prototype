using UnityEngine;
using System.Collections;
public class GrappleGun : MonoBehaviour {
public bool shoot;
public GameObject BulletSpawner;
public GameObject BulletPrefab;
public int BulletSpeed = 5;
public bool A = false;
public int length;
public int oldLength;
public GameObject chainPrefab;
public Vector3 NewestChild;
public Rigidbody2D NewestBody;
public bool collided;
public CheckColl coll;
public bool DontAim;

void Start()
{

}

void Update()
{
}
}
        // find a way for it to instantly connect to the player, as right now jumping and shoooting is
