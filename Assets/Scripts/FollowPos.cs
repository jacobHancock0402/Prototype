using UnityEngine;

public class FollowPos : MonoBehaviour {
    public GameObject target;
    void Update() {
        //float diffY = (gameObject.transform.position.y - target.transform.position.y);
        //float diffX = (gameObject.transform.position.x - target.transform.position.x);
        //float total = diffX + diffY;
        //float xShare = diffX / total;
        //float yShare = diffY / total;
        //yShare = diff

        // i'd want to do some arithmetic with rotation here or like a point a head to show where to place on arm with displacement on y and x
        // otherwise looks quite stupid with hanging in air
        // everything else is ok but body sort of angles in walking
        
        Collider2D collider = gameObject.GetComponent<BoxCollider2D>();
        gameObject.transform.position = new Vector3(target.transform.position.x + (collider.bounds.max[0] - collider.bounds.center[0]), target.transform.position.y, 0 );
    }
}