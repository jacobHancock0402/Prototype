using UnityEngine;
public class Gun : MonoBehaviour {
    public GameObject[] bullets;
    public bool shooting;
    public GameObject BulletSpawner;
    public GameObject BulletPrefab;
    public int BulletSpeed = 5;
    public bool A = false;
    public float recoil;
    public float angle;
    public GameObject Player;
    public Transform Child;
    public SpriteRenderer Sprite;
    public Vector3 direction;
    public Stickman grabbing;
    public Rigidbody2D Rigid;
    public shooting Shoot;
    public bool DontAim;
    public bool Changed;
    public bool DontAi;

    void Start()
    {
        if(gameObject.transform.parent.tag != "rArm")
        {
            Child = gameObject.transform.GetChild(0);
            if(Child.tag == "Gun" || Child.tag == "GrappleGun")
            {
                if(Child.tag == "Gun")
                {
                    Shoot = Child.GetComponent<shooting>();
                }
                Sprite = Child.gameObject.GetComponent<SpriteRenderer>();
            }
            else
            {
                Child = null;
                Sprite = null;
                Shoot = null;
            }
        }
        grabbing = gameObject.transform.root.gameObject.GetComponent<Stickman>();
    }

    void Update()
    {

        if (Changed)
        {
            if(gameObject.transform.parent.tag != "rArm")
            {
                Child = gameObject.transform.GetChild(0);
                if(Child.tag == "Gun" || Child.tag == "GrappleGun")
                {
                    if(Child.tag == "Gun")
                    {
                        Shoot = Child.GetComponent<shooting>();
                    }
                    Sprite = Child.gameObject.GetComponent<SpriteRenderer>();
                }
                else
                {
                    Child = null;
                    Sprite = null;
                    Shoot = null;
                }
            }
            grabbing = gameObject.transform.parent.gameObject.GetComponent<Stickman>();
            Changed = false;
        }
        if (A != true)
        {
            if (grabbing.grabbingL || grabbing.holdingL || grabbing.NowGrabbingR || grabbing.NowHoldingR || grabbing.posturingL || grabbing.posturingR)
            {
                DontAim = true;
            }
            else
            {
                DontAim = false;
            }
            if(DontAim != true)
            {
                //Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                //angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                // angle shouldn't be negative but otherwise it pretty much goes in opposite direction
                //transform.eulerAngles = new Vector3(0, 0, -angle + 90);
                //Rigid.freezeRotation = true;

                if (Input.GetMouseButtonDown(0) && Shoot)
                {
                    Shoot.shoot = true;
                    Shoot.Fire(angle, direction);
                }

            }

        }

        else 
        {
            AI ai = gameObject.transform.parent.gameObject.GetComponent<AI>();
            if (ai.State == "Chasing")
            {   
                // shoot here
                 direction = (Player.transform.position - transform.position).normalized;
                if(Shoot)
                {
                    Shoot.shoot = true;
                    Shoot.Fire(angle,direction);
                }

                // check if melee weapon
            }

            else if (ai.State == "Searching")
            {
                direction = (ai.LastKnownPos - transform.position).normalized;
            }

            else
            {
                direction = transform.position;
            }
            

            angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                // angle shouldn't be negative but otherwise it pretty much goes in opposite direction
            //foreach(_Muscle muscle in grabbing.muscles)
            //{
                //transform.eulerAngles = new Vector3(0, 0, -angle + 90);
                //muscle.restRotation
            //}
        }
        if (Sprite)
        {
        if (-angle + 90 > 90)
        {
            Sprite.flipY = true;
        }

        else
        {
            Sprite.flipY = false;
        }
    }
        
        
    }
    

}