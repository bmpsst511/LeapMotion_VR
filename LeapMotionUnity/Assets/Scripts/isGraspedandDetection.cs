using Leap.Unity;
using Leap.Unity.Interaction;
using UnityEngine;

public class isGraspedandDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ContactBone.cup2Detected);
        if (InteractionBehaviour.grasp)
        {
            //Debug.Log(cup_1.name);
            if(ContactBone.cup1Detected)
            {
                Debug.Log("grap obj_1");
            }
            if(ContactBone.cup2Detected)
            {
                Debug.Log("grab obj_2");
            }
            if(ContactBone.cup3Detected)
            {
                Debug.Log("grab obj_3");
            }
        }
    }
}
