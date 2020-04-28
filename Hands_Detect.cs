using UnityEngine;
using System.Collections.Generic;
using Leap;
using Leap.Unity;

public class Hands_Detect : MonoBehaviour {
LeapProvider provider;

void Start ()
{
    provider = FindObjectOfType<LeapProvider>() as LeapProvider;
}

void Update ()
{

    /*Frame frame = provider.CurrentFrame;
    foreach (Hand hand in frame.Hands)
    {
         if (hand.IsLeft) {
            Debug.Log ("Left hand is present");
        } else if (hand.IsRight) {
            Debug.Log ("Right hand is present");
        }
        if(hand.IsLeft && hand.IsRight == true){
            Debug.Log ("Both hand are present");
        }
    }*/

    Frame frame = provider.CurrentFrame; // controller is a Controller object
    if(frame.Hands.Count > 1){
    List<Hand> hands = frame.Hands;
    Hand firstHand = hands [0];
    Hand Second_tHand = hands [1];
    Debug.Log ("both");
    }
}
}
