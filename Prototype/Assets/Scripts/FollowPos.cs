using UnityEngine;
public class FollowPos : MonoBehaviour {
    public GameObject target;
    public SpriteRenderer Sprite;
    void Start() {
        Sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update() {
        //Debug.Log(gameObject.transform.localRotation.eulerAngles);
        //float diffY = (gameObject.transform.position.y - target.transform.position.y);
        //float diffX = (gameObject.transform.position.x - target.transform.position.x);
        //float total = diffX + diffY;
        //float xShare = diffX / total;
        //float yShare = diffY / total;
        //yShare = diff

        // i'd want to do some arithmetic with rotation here or like a point a head to show where to place on arm with displacement on y and x
        // otherwise looks quite stupid with hanging in air
        // everything else is ok but body sort of angles in walking
        float rotation = gameObject.transform.eulerAngles.z;
        //Debug.Log(rotation);
        float rotations = (Mathf.Floor(Mathf.Abs(rotation)/360) * 360);
        if(rotation < 0)
        {
            rotations = -rotations;
        }
        rotation = rotation - rotations;
        Debug.Log(gameObject.transform.eulerAngles.z);
        //Debug.Log(rotation);
        float abs_rot = Mathf.Abs(rotation);
        float quadrot = Mathf.Abs(180-abs_rot);
        float badrot = Mathf.Abs(90-quadrot);
        Collider2D collider = gameObject.GetComponent<BoxCollider2D>();
        float xScalar = Mathf.Abs(90 -  quadrot)/180;
        float yScalar = Mathf.Abs(90 - badrot)/90;

        if(rotation < 360 && rotation > 180)
        {
            yScalar = -yScalar;
        }
        if(rotation > 90 && rotation < 270)
        {
            xScalar = -xScalar;
            Sprite.flipY = true;
        }
        else
        {
            Sprite.flipY = false;
        }
<<<<<<< HEAD
        //gameObject.transform.rotation = Quaternion.Euler(0f,0f,angle)
=======
>>>>>>> 242da17ffddb7cca4e9364343fad6ca7e8037683
        Debug.Log("y");
        //Debug.Log(yScalar);
        Debug.Log("x");
        Debug.Log(xScalar);
        //Debug.Log(quadrot);
<<<<<<< HEAD
        Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0f,0f,-angle + 90);
=======

>>>>>>> 242da17ffddb7cca4e9364343fad6ca7e8037683
        gameObject.transform.position = new Vector3(target.transform.position.x + (xScalar *(collider.bounds.max[0] - collider.bounds.center[0])), target.transform.position.y + (yScalar *(collider.bounds.max[0] - collider.bounds.center[0]))  , 0 );
    }
}