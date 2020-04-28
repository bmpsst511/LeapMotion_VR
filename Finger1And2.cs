using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class Finger1And2 : MonoBehaviour {
    public HandModelBase leftHandModel;
    public HandModelBase rightHandModel;
    float twoFingerDistance = 0.07f;
    //HELLOW
   // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (leftHandModel.IsTracked)
        {
            Hand leftHand = leftHandModel.GetLeapHand();
            if ((leftHand.Fingers[0].TipPosition - leftHand.Fingers[1].TipPosition).Magnitude < twoFingerDistance)
            {
                print("LeftHandï¼?");
              
            }
        }


        
        if (leftHandModel.IsTracked)
        {
            Hand leftHand = leftHandModel.GetLeapHand();
            if ((leftHand.Fingers[0].TipPosition - leftHand.Fingers[1].TipPosition).Magnitude > twoFingerDistance)
            {
                print("??????");
                
            }
        }
       
       
        if (leftHandModel.IsTracked)
        {
            Hand leftHand = leftHandModel.GetLeapHand();
            if ((leftHand.Fingers[0].TipPosition - leftHand.Fingers[1].TipPosition).Magnitude > twoFingerDistance&&
                (leftHand.Fingers[0].TipPosition - leftHand.Fingers[2].TipPosition).Magnitude > twoFingerDistance&&
                (leftHand.Fingers[0].TipPosition - leftHand.Fingers[3].TipPosition).Magnitude > twoFingerDistance&&
                (leftHand.Fingers[0].TipPosition - leftHand.Fingers[4].TipPosition).Magnitude > twoFingerDistance)
            {
                print("äº????å¼µé??");
                
            }
        }
        


        
        if (rightHandModel.IsTracked)
        {
            Hand rightHand = rightHandModel.GetLeapHand();
            if ((rightHand.Fingers[0].TipPosition - rightHand.Fingers[1].TipPosition).Magnitude < twoFingerDistance)
            {
                print("RightHandï¼?");
            }
        }
    }
}



