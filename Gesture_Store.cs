using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using System;
using System.Text;
using System.IO;
public class Gesture_Store : MonoBehaviour
{
    LeapProvider provider;
    public GameObject L_Palm;
    public GameObject R_Palm;
    public GameObject L_index_end;
    public GameObject L_middle_end;
    public GameObject L_pinky_end;
    public GameObject L_ring_end;
    public GameObject L_thumb_end;

    public GameObject R_index_end;
    public GameObject R_middle_end;
    public GameObject R_pinky_end;
    public GameObject R_ring_end;
    public GameObject R_thumb_end;
    public float L_Palm_Vel_X, L_Palm_Vel_Y,L_Palm_Vel_Z, R_Palm_Vel_X, R_Palm_Vel_Y, R_Palm_Vel_Z;

    bool state = true;
    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        Invoke("Stop",10f);
    }

    void Update()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if(Left_Hand_Detected())
            {
                L_Palm_Vel_X = hand.PalmVelocity.x;
                L_Palm_Vel_Y = hand.PalmVelocity.y;
                L_Palm_Vel_Z = hand.PalmVelocity.z;
            }
            
            if(Right_Hand_Detected())
            {
                R_Palm_Vel_X = hand.PalmVelocity.x;
                R_Palm_Vel_Y = hand.PalmVelocity.y;
                R_Palm_Vel_Z = hand.PalmVelocity.z;
            }
            
        }
        if(state)
        {write();}
    }

    bool Left_Hand_Detected()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsLeft) 
            {
                return true;
            } 
        }
       return false;
    }
    bool Right_Hand_Detected()
    {
        Frame frame = provider.CurrentFrame;
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight) 
            {
                return true;
            } 
        }
        return false;
    }
    void write()
    {
        StreamWriter sw = new StreamWriter(@"D:\Happy.txt", true);
        string L_Palm_Data = "Left_Palm_Angle"+":"+
                              L_Palm.transform.rotation.x.ToString()+","+
                              L_Palm.transform.rotation.y.ToString()+","+
                              L_Palm.transform.rotation.z.ToString()+";"+
                              "Left_Palm_Position"+":"+
                              L_Palm.transform.position.x.ToString()+","+
                              L_Palm.transform.position.y.ToString()+","+
                              L_Palm.transform.position.z.ToString()+";"+
                              "Left_Palm_Velocity"+":"+
                              L_Palm_Vel_X.ToString()+","+
                              L_Palm_Vel_Y.ToString()+","+
                              L_Palm_Vel_Z.ToString()+";"+
                              "Right_Palm_Angle"+":"+
                              R_Palm.transform.rotation.x.ToString()+","+
                              R_Palm.transform.rotation.y.ToString()+","+
                              R_Palm.transform.rotation.z.ToString()+";"+
                              "Right_Palm_Position"+":"+
                              R_Palm.transform.position.x.ToString()+","+
                              R_Palm.transform.position.y.ToString()+","+
                              R_Palm.transform.position.z.ToString()+";"+
                              "Right_Palm_Velocity"+":"+
                              R_Palm_Vel_X.ToString()+","+
                              R_Palm_Vel_Y.ToString()+","+
                              R_Palm_Vel_Z.ToString()+";"+
                              "L_Index_end_Angle"+":"+
                              L_index_end.transform.rotation.x.ToString()+","+
                              L_index_end.transform.rotation.y.ToString()+","+
                              L_index_end.transform.rotation.z.ToString()+";"+
                              "L_Index_end_Position"+":"+
                              L_index_end.transform.position.x.ToString()+","+
                              L_index_end.transform.position.y.ToString()+","+
                              L_index_end.transform.position.z.ToString()+";"+
                              "L_middle_end_Angle"+":"+
                              L_middle_end.transform.rotation.x.ToString()+","+
                              L_middle_end.transform.rotation.y.ToString()+","+
                              L_middle_end.transform.rotation.z.ToString()+";"+
                              "L_middle_end_Position"+":"+
                              L_middle_end.transform.position.x.ToString()+","+
                              L_middle_end.transform.position.y.ToString()+","+
                              L_middle_end.transform.position.z.ToString()+";"+
                              "L_ring_end_Angle"+":"+
                              L_ring_end.transform.rotation.x.ToString()+","+
                              L_ring_end.transform.rotation.y.ToString()+","+
                              L_ring_end.transform.rotation.z.ToString()+";"+
                              "L_ring_end_Position"+":"+
                              L_ring_end.transform.position.x.ToString()+","+
                              L_ring_end.transform.position.y.ToString()+","+
                              L_ring_end.transform.position.z.ToString()+";"+
                              "L_pinky_end_Angle"+":"+
                              L_pinky_end.transform.rotation.x.ToString()+","+
                              L_pinky_end.transform.rotation.y.ToString()+","+
                              L_pinky_end.transform.rotation.z.ToString()+";"+
                              "L_pinky_end_Position"+":"+
                              L_pinky_end.transform.position.x.ToString()+","+
                              L_pinky_end.transform.position.y.ToString()+","+
                              L_pinky_end.transform.position.z.ToString()+";"+
                              "L_thumb_end_Angle"+":"+
                              L_thumb_end.transform.rotation.x.ToString()+","+
                              L_thumb_end.transform.rotation.y.ToString()+","+
                              L_thumb_end.transform.rotation.z.ToString()+";"+
                              "L_thumb_end_Position"+":"+
                              L_thumb_end.transform.position.x.ToString()+","+
                              L_thumb_end.transform.position.y.ToString()+","+
                              L_thumb_end.transform.position.z.ToString()+";"+
                              "R_Index_end_Angle"+":"+
                              R_index_end.transform.rotation.x.ToString()+","+
                              R_index_end.transform.rotation.y.ToString()+","+
                              R_index_end.transform.rotation.z.ToString()+";"+
                              "R_Index_end_Position"+":"+
                              R_index_end.transform.position.x.ToString()+","+
                              R_index_end.transform.position.y.ToString()+","+
                              R_index_end.transform.position.z.ToString()+";"+
                              "R_middle_end_Angle"+":"+
                              R_middle_end.transform.rotation.x.ToString()+","+
                              R_middle_end.transform.rotation.y.ToString()+","+
                              R_middle_end.transform.rotation.z.ToString()+";"+
                              "R_middle_end_Position"+":"+
                              R_middle_end.transform.position.x.ToString()+","+
                              R_middle_end.transform.position.y.ToString()+","+
                              R_middle_end.transform.position.z.ToString()+";"+
                              "R_ring_end_Angle"+":"+
                              R_ring_end.transform.rotation.x.ToString()+","+
                              R_ring_end.transform.rotation.y.ToString()+","+
                              R_ring_end.transform.rotation.z.ToString()+";"+
                              "R_ring_end_Position"+":"+
                              R_ring_end.transform.position.x.ToString()+","+
                              R_ring_end.transform.position.y.ToString()+","+
                              R_ring_end.transform.position.z.ToString()+";"+
                              "R_pinky_end_Angle"+":"+
                              R_pinky_end.transform.rotation.x.ToString()+","+
                              R_pinky_end.transform.rotation.y.ToString()+","+
                              R_pinky_end.transform.rotation.z.ToString()+";"+
                              "R_pinky_end_Position"+":"+
                              R_pinky_end.transform.position.x.ToString()+","+
                              R_pinky_end.transform.position.y.ToString()+","+
                              R_pinky_end.transform.position.z.ToString()+";"+
                              "R_thumb_end_Angle"+":"+
                              R_thumb_end.transform.rotation.x.ToString()+","+
                              R_thumb_end.transform.rotation.y.ToString()+","+
                              R_thumb_end.transform.rotation.z.ToString()+";"+
                              "R_thumb_end_Position"+":"+
                              R_thumb_end.transform.position.x.ToString()+","+
                              R_thumb_end.transform.position.y.ToString()+","+
                              R_thumb_end.transform.position.z.ToString();
                              
        sw.WriteLine(L_Palm_Data);
        sw.Flush();
        sw.Close();
    }

    void Stop()
    {
        state = false;
    }
}
