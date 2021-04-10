using UnityEngine;
public class muscle_holder : MonoBehaviour
{
    public _Muscle muscle;
    public bool activated = true;

    void Update()
    {	if(!activated)
    	{	
    		muscle.activated = false;
    	}
    }
}