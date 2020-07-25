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
public class Dynamic_Gesture_Recognition : MonoBehaviour
{
    LeapProvider provider;
    public HandModelBase leftHandModel;
    public HandModelBase RightHandModel;
    [Tooltip("Velocity (m/s) move toward ")]
    public float deltaVelocity = 0.7f; //手部滑動的速度閥
    public float hit_velocity_valve = 1.2f;
    public float accq_freq = 0;//invoke repeating的擷取頻率

    public List<bool> judge_LeftHanddown; //宣告一個bool屬性的list給左手向下揮
    public List<bool> judge_LeftHandup;//宣告一個bool屬性的list給左手向上揮

    [Tooltip("Velocity (m/s) move toward ")]
    //這裡傳進來你要開啟的手指 緊握手指 {} 傳一個手指{Finger.FingerType.TYPE_RING}...以此類推，當傳進5個值得時候代表 手張開，當傳進0個值的時候代表 握手
    Finger.FingerType[] arr = 
    {
     //Finger.FingerType.TYPE_THUMB,
     Finger.FingerType.TYPE_INDEX, Finger.FingerType.TYPE_MIDDLE 
     //,Finger.FingerType.TYPE_RING ,Finger.FingerType.TYPE_PINKY
    };

    [SerializeField]
    float velocityX, velocityY, velocityZ;
    // Start is called before the first frame update
    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;

        //在Start先塞20個false給judge array list
        for(int i =0; i<=19; i++)
        {
            judge_LeftHanddown.Add(false);
        }
        for(int i =0; i<=19; i++)
        {
            judge_LeftHandup.Add(false);
        }

        InvokeRepeating("Dynamic_Gestures", 2.0f, accq_freq); //Invoke playmode後兩秒開始執行,之後每隔acc_freq執行一次
    }

     void Dynamic_Gestures()
    {   
        Frame frame = provider.CurrentFrame; // controller is a Controller object

        //移除list的開頭數據,在無判斷到手勢的時候一直刷新list
        judge_LeftHanddown.RemoveAt(0);
        judge_LeftHandup.RemoveAt(0);

        //偵測不到手的模型時將false塞入list裡
        if (!leftHandModel.IsTracked) 
        {
            judge_LeftHanddown.Add(false);
            judge_LeftHandup.Add(false);
            return;
        }
        
        Hand leftHand = leftHandModel.GetLeapHand();
        Hand RightHand = RightHandModel.GetLeapHand();

        //手部的旋轉角度
        float Pitch = leftHand.Direction.Pitch;
        float yaw = leftHand.Direction.Yaw;
        float roll = leftHand.Direction.Roll;
        //print(yaw);
        
        //判斷到手部模型並判斷有下滑手勢時,將true塞入list裡
        if (IsMoveDown(leftHand))
        {
            judge_LeftHanddown.Add(true); 
            //print("下滑");
        }
        else
        {
            judge_LeftHanddown.Add(false);
        }

        if (IsMoveUp(leftHand))
        {
            judge_LeftHandup.Add(true); 
            //print("上滑");
        }
        else
        {
            judge_LeftHandup.Add(false);
        }
    }

      // Update is called once per frame
     void FixedUpdate()
    {   
        if (!leftHandModel.IsTracked) return;
        Hand leftHand = leftHandModel.GetLeapHand();
        //給定初始次數為0
        int count_down=0;
        int count_up=0;
        //print(judge.Count);

        //如果判斷到list裡的flag為true則次數+1
        foreach(bool flag in judge_LeftHanddown )
        {
            if(flag)
            {
                count_down++;
            }
        }

        foreach(bool flag in judge_LeftHandup)
        {
            if(flag)
            {
                count_up++;
            }
        }
         //當次數>5的時候執行需要的動作
         if(count_down > 6 && count_up > 6)
         { 
            //需要的功能 
            Debug.Log("User dynamic touch");
            //Anim.SetBool("hap",true);
         }

        velocityX = leftHand.PalmVelocity.x; //手向前伸的速度
        velocityY = leftHand.PalmVelocity.y;
        velocityZ = leftHand.PalmVelocity.z;
        ///大力拍打娃娃的手勢
         if ( IsOpenFullHand(leftHand) && velocityX > hit_velocity_valve)
        {
            print("拍打");
        }
        else if (IsOpenFullHand(leftHand) && velocityY > hit_velocity_valve)
        {
            print("拍打");
        }
        else if(IsOpenFullHand(leftHand) && velocityZ > hit_velocity_valve)
        {
            print("拍打");
        }

        if(CheckFingerOpenToHand(leftHand, arr) && velocityX > deltaVelocity)
        {
            print("戳");
        }
      
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

    /// 判斷是否抓取
    bool isGrabHand(Hand hand)
    {
        return hand.GrabStrength > 0.8f;
    }

    /// 判斷是不是握拳
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

    /// 判斷手指是否全張開
    bool IsOpenFullHand(Hand hand)
    {
        return hand.GrabStrength == 0;
    }

    /// 手滑向左邊
    protected bool IsMoveLeft(Hand hand)   
    {
        //x軸移動的速度   deltaVelocity = 0.7f   
        return hand.PalmVelocity.x < -deltaVelocity;
    }

    /// 手滑向右邊
    protected bool IsMoveRight(Hand hand)
    {
        return hand.PalmVelocity.x > deltaVelocity;
    }

    /// 手滑向上邊
    protected bool IsMoveUp(Hand hand)
    {
        return hand.PalmVelocity.y > deltaVelocity;
    }

    /// 手滑向下邊
    protected bool IsMoveDown(Hand hand)
    {
        return hand.PalmVelocity.y < -deltaVelocity;
    }
}
