using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using UnityEngine;

public class Gestures : MonoBehaviour {
    public HandModelBase leftHandModel;
    //public GameObject spot_light;
    public Animator anim;
    public AnimatorStateInfo BS;
    public int flower_stand = Animator.StringToHash("Base Layer.flower_stand");
    public int bloom = Animator.StringToHash("Base Layer.bloom");

    public LightUp lightup;

    [Tooltip("Velocity (m/s) move toward ")]
    public float deltaVelocity = 0.7f;
    Finger.FingerType[] arr = {
     //Finger.FingerType.TYPE_THUMB,
     Finger.FingerType.TYPE_INDEX, Finger.FingerType.TYPE_MIDDLE 
     //,Finger.FingerType.TYPE_RING ,Finger.FingerType.TYPE_PINKY
     };

    void Start() {
         //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!leftHandModel.IsTracked) return;
        Hand leftHand = leftHandModel.GetLeapHand();
        //if (IsMoveLeft(leftHand))
        //{
        //   print("Move to Left");
        //}
        //if (IsMoveRight(leftHand))
        //{
        //    print("Move to Right");
        //}
        //if (IsMoveUp(leftHand))
        //{
        //    print("Move to Up");
        //}
        //if (IsMoveDown(leftHand))
        //{
        //    print("Move to Down");
        //}

        if (IsCloseHand(leftHand))
        {
            anim.SetBool("bloom",false);
        //    print("play anim");  
        }
        //if (IsOpenFullHand(leftHand))
        //{
        //    print("Open Full Hand");
           //  anim.Play("roll");
        //}
        //if (IsOpenFullHand(leftHand))
        //{
        //    print("IsOpenFullHand");
        //}
        //if (CheckFingerCloseToHand(leftHand))
        //{
        //    print("CheckFingerCloseToHand");
        //}

        /* if (CheckFingerOpenToHand(leftHand,arr))
        {
            print("ok");
            point_light.SetActive(false);

        }
        if (IsOpenFullHand(leftHand))
        {
            print("open");
            anim.Play("roll");
        }*/
        
    }

    
    /// <summary>
    /// </summary>
    /// <param name="hand"></param>
    /// <param name="arr"></param>
    /// <returns></returns>
    bool CheckFingerOpenToHand(Hand hand, Finger.FingerType[] fingerTypesArr,float deltaCloseFinger = 0.05f)
    {
        List<Finger> listOfFingers = hand.Fingers;
        float count = 0;
        for (int f = 0; f < listOfFingers.Count; f++)
        {
            Finger finger = listOfFingers[f];
            // 
            if ((finger.TipPosition - hand.PalmPosition).Magnitude < deltaCloseFinger)
            {
                if (fingerTypesArr.Length == 0)
                {
                    count++;
                    continue;
                }
                for (int i = 0; i < fingerTypesArr.Length; i++)
                {
                    //  false
                    if (finger.Type == fingerTypesArr[i])
                    {
                        return false;
                    }
                    else
                    {
                        count++;
                    }
                }

            }
        }
        if (fingerTypesArr.Length == 0)
        {
            return count == 5;
        }
        return (count/ fingerTypesArr.Length == 5 - fingerTypesArr.Length);
    }

    /// <summary>
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    bool isGrabHand(Hand hand)
    {
        return hand.GrabStrength > 0.8f;
    }

    /// <summary>
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    bool IsCloseHand(Hand hand)
    {
        List<Finger> listOfFingers = hand.Fingers;
        int count = 0;
        for (int f = 0; f < listOfFingers.Count; f++)
        {
            Finger finger = listOfFingers[f];
            if ((finger.TipPosition - hand.PalmPosition).Magnitude < 0.05f)
            {
                count++;
            }
        }
        return (count == 4);
    }

    /// <summary>
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    bool IsOpenFullHand(Hand hand)
    {
        return hand.GrabStrength == 0;
    }

    /// <summary>
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    protected bool IsMoveLeft(Hand hand)
    {
        return hand.PalmVelocity.x < -deltaVelocity;
    }

    /// <summary>
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    protected bool IsMoveRight(Hand hand)
    {
        return hand.PalmVelocity.x > deltaVelocity;
    }

    /// <summary>
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    protected bool IsMoveUp(Hand hand)
    {
        return hand.PalmVelocity.y > deltaVelocity;
    }

    /// <summary>
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    protected bool IsMoveDown(Hand hand)
    {
        return hand.PalmVelocity.y < -deltaVelocity;
    }


    /// <summary>
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    bool CheckFingerCloseToHand(Hand hand)
    {
        List<Finger> listOfFingers = hand.Fingers;
        int count = 0;
        for (int f = 0; f < listOfFingers.Count; f++)
        {
            Finger finger = listOfFingers[f];
            if ((finger.TipPosition - hand.PalmPosition).Magnitude < 0.05f)
            {
                if (finger.Type == Finger.FingerType.TYPE_THUMB)
                {
                    return false;
                }
                else
                {
                    count++;
                }
            }
        }
        return (count == 4);
    }

   
}
