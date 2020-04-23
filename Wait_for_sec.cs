using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引入庫  
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Leap;
using Leap.Unity;
public class Wait_for_sec : MonoBehaviour
{
    LeapProvider provider;
    public HandModelBase leftHandModel;
    public HandModelBase RightHandModel;
    [Tooltip("Velocity (m/s) move toward ")]
    public float deltaVelocity = 0.7f;
    public float accq_freq = 0;
    //這裡傳進來你要開啟的手指 緊握手指 {} 傳一個手指{Finger.FingerType.TYPE_RING}...以此類推，當傳進5個值得時候代表 手張開，當傳進0個值的時候代表 握手
    Finger.FingerType[] arr = {
     //Finger.FingerType.TYPE_THUMB,
     Finger.FingerType.TYPE_INDEX, Finger.FingerType.TYPE_MIDDLE 
     //,Finger.FingerType.TYPE_RING ,Finger.FingerType.TYPE_PINKY
     };

    //public ArrayList judge = new ArrayList();
    //public var myList = new List<bool>();  
    public List<bool> judge;
    float twoFingerDistance = 0.07f;

    

    // Start is called before the first frame update
    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        for(int i =0; i<=19; i++)
        {
            judge.Add(false);
        }
        InvokeRepeating("LaunchProjectile", 2.0f, accq_freq);
    }

    void LaunchProjectile()
    {   
        Frame frame = provider.CurrentFrame; // controller is a Controller object
        /*if(frame.Hands.Count > 1)
        {
            List<Hand> hand = frame.Hands;
            Hand firstHand = hand [0];
            Hand Second_tHand = hand [1];
            Debug.Log ("both");
        }*/

        /************************gestures********************************************/
        judge.RemoveAt(0);
        if (!leftHandModel.IsTracked) 
        {
            judge.Add(false);
            return;
        }
        Hand leftHand = leftHandModel.GetLeapHand();
        Hand RightHand = RightHandModel.GetLeapHand();
        float Pitch = leftHand.Direction.Pitch;
        float yaw = leftHand.Direction.Yaw;
        float roll = leftHand.Direction.Roll;
        print(yaw);
        //judge.Remove(null);
        
        if (IsMoveDown(leftHand))
        {
            judge.Add(true); 
            //print("下滑");
        }
        else
        {
            judge.Add(false);
        }
    }

    // Update is called once per frame
     void FixedUpdate()
    {   
        int length = judge.Count;
        int count=0;
        //print(judge.Count);
         foreach(bool flag in judge )
        {
            if(flag)
            {
                count++;
            }
        }
         if(count > 5)
         { Debug.Log("yayayayayaya");}
        /************************gestures********************************************/
       /*  if (!leftHandModel.IsTracked) return;
        Hand leftHand = leftHandModel.GetLeapHand();
        Hand RightHand = RightHandModel.GetLeapHand();
        //judge.Remove(null);
        judge.RemoveAt(0);

        if (IsMoveDown(leftHand))
        {
            judge.Add(true); 
            print("左手下滑");
        }
        else
        {
            judge.Add(false);
        } */
    }

    bool CheckFingerOpenToHand(Hand hand, Finger.FingerType[] fingerTypesArr,float deltaCloseFinger = 0.05f)
    {
        List<Finger> listOfFingers = hand.Fingers;
        float count = 0;
        // 遍歷5個手指
        for (int f = 0; f < listOfFingers.Count; f++)
        {
            Finger finger = listOfFingers[f];
            // 判讀每個手指的指尖位置和掌心位置的長度是不是小於某個值，以判斷手指是否貼著掌心
            if ((finger.TipPosition - hand.PalmPosition).Magnitude < deltaCloseFinger)
            {
                // 如果傳進來的陣列長度是0，有一個手指那麼 count + 1，continue 跳出，不執行下面陣列長度不是0 的邏輯
                if (fingerTypesArr.Length == 0)
                {
                    count++;
                    continue;
                }
                // 傳進來的陣列長度不是 0，
                for (int i = 0; i < fingerTypesArr.Length; i++)
                {
                    // 假如本例子傳進來的是食指和中指，邏輯走到這裡，如果你的食指是緊握的，下面會判斷這個手指是不是食指，返回 false
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
        // 這裡除以length 是因為上面陣列在每次 for 迴圈 count ++ 會執行 length 次
        return (count/ fingerTypesArr.Length == 5 - fingerTypesArr.Length);
    }

    /// <summary>
    /// 判斷是否抓取
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    bool isGrabHand(Hand hand)
    {
        return hand.GrabStrength > 0.8f;
    }

    /// <summary>
    /// 判斷是不是握拳
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
    /// 判斷手指是否全張開
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    bool IsOpenFullHand(Hand hand)
    {
        return hand.GrabStrength == 0;
    }

    /// <summary>
    /// 手滑向左邊
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    protected bool IsMoveLeft(Hand hand)   // 手划向左邊
    {
        //x軸移動的速度   deltaVelocity = 0.7f   
        return hand.PalmVelocity.x < -deltaVelocity;
    }

    /// <summary>
    /// 手滑向右邊
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    protected bool IsMoveRight(Hand hand)
    {
        return hand.PalmVelocity.x > deltaVelocity;
    }

    /// <summary>
    /// 手滑向上邊
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    protected bool IsMoveUp(Hand hand)
    {
        return hand.PalmVelocity.y > deltaVelocity;
    }

    /// <summary>
    /// 手滑向下邊
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    protected bool IsMoveDown(Hand hand)
    {
        return hand.PalmVelocity.y < -deltaVelocity;
    }


    /// <summary>
    /// 判斷四指是否靠向掌心
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
