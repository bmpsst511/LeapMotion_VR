using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using System.IO;

public class Gesture_Store : MonoBehaviour
{   
    /*Auto find hidden handmodels, palms, wrists, finger joints*/
    /*自動抓取隱藏的手部各子母物件*/
     GameObject HandModels, LoPoly_Rigged_Hand_Left, L_Wrist, 
     L_index_meta, L_index_a, L_index_b,
     L_middle_meta, L_middle_a, L_middle_b,
     L_pinky_meta, L_pinky_a, L_pinky_b,
     L_ring_meta, L_ring_a, L_ring_b,
     L_thumb_meta, L_thumb_a,
     LoPoly_Rigged_Hand_Right, R_Wrist, 
     R_index_meta, R_index_a, R_index_b, 
     R_middle_meta, R_middle_a, R_middle_b, 
     R_pinky_meta, R_pinky_a, R_pinky_b, 
     R_ring_meta, R_ring_a, R_ring_b, 
     R_thumb_meta, R_thumb_a;
    
     [SerializeField]
     GameObject L_Palm, R_Palm, L_index_c, L_middle_c, L_pinky_c, L_ring_c, L_thumb_b,
     R_index_c, R_middle_c, R_pinky_c, R_ring_c, R_thumb_b;
    /*Auto find hidden handmodels, palms, wrists, finger joints*/
    /*自動抓取隱藏的手部各子母物件*/
    //[SerializeField]
     float L_Palm_Vel_X, L_Palm_Vel_Y,L_Palm_Vel_Z, R_Palm_Vel_X, R_Palm_Vel_Y, R_Palm_Vel_Z;
     
     //build a state to stop the aquire process.
     bool state = true;
     public bool allowToCollcet = false; 
     //建立一個狀態,判斷何時該停止收集資料
    void Start()
    {
        /*In start function grab all the objects you need in one time*/
        /*在Start函式裡,在開始時抓取所有需要的物件,比較省效能*/
        HandModels = GameObject.Find("HandModels");
        LoPoly_Rigged_Hand_Left = HandModels.transform.Find("LoPoly Rigged Hand Left").gameObject;
        L_Wrist = LoPoly_Rigged_Hand_Left.transform.Find("L_Wrist").gameObject;
        L_Palm = L_Wrist.transform.Find("L_Palm").gameObject;

        L_index_meta = L_Palm.transform.Find("L_index_meta").gameObject;
        L_index_a = L_index_meta.transform.Find("L_index_a").gameObject;
        L_index_b = L_index_a.transform.Find("L_index_b").gameObject;
        L_index_c = L_index_b.transform.Find("L_index_c").gameObject;

        L_middle_meta = L_Palm.transform.Find("L_middle_meta").gameObject;
        L_middle_a = L_middle_meta.transform.Find("L_middle_a").gameObject;
        L_middle_b = L_middle_a.transform.Find("L_middle_b").gameObject;
        L_middle_c = L_middle_b.transform.Find("L_middle_c").gameObject;
        
        L_pinky_meta = L_Palm.transform.Find("L_pinky_meta").gameObject;
        L_pinky_a = L_pinky_meta.transform.Find("L_pinky_a").gameObject;
        L_pinky_b = L_pinky_a.transform.Find("L_pinky_b").gameObject;
        L_pinky_c = L_pinky_b.transform.Find("L_pinky_c").gameObject;
        
        L_ring_meta = L_Palm.transform.Find("L_ring_meta").gameObject;
        L_ring_a = L_ring_meta.transform.Find("L_ring_a").gameObject;
        L_ring_b = L_ring_a.transform.Find("L_ring_b").gameObject;
        L_ring_c = L_ring_b.transform.Find("L_ring_c").gameObject;
        
        L_thumb_meta = L_Palm.transform.Find("L_thumb_meta").gameObject;
        L_thumb_a = L_thumb_meta.transform.Find("L_thumb_a").gameObject;
        L_thumb_b = L_thumb_a.transform.Find("L_thumb_b").gameObject;
        
        LoPoly_Rigged_Hand_Right = HandModels.transform.Find("LoPoly Rigged Hand Right").gameObject;
        R_Wrist = LoPoly_Rigged_Hand_Right.transform.Find("R_Wrist").gameObject;
        R_Palm = R_Wrist.transform.Find("R_Palm").gameObject;

        R_index_meta = R_Palm.transform.Find("R_index_meta").gameObject;
        R_index_a = R_index_meta.transform.Find("R_index_a").gameObject;
        R_index_b = R_index_a.transform.Find("R_index_b").gameObject;
        R_index_c = R_index_b.transform.Find("R_index_c").gameObject;

        R_middle_meta = R_Palm.transform.Find("R_middle_meta").gameObject;
        R_middle_a = R_middle_meta.transform.Find("R_middle_a").gameObject;
        R_middle_b = R_middle_a.transform.Find("R_middle_b").gameObject;
        R_middle_c = R_middle_b.transform.Find("R_middle_c").gameObject;
        
        R_pinky_meta = R_Palm.transform.Find("R_pinky_meta").gameObject;
        R_pinky_a = R_pinky_meta.transform.Find("R_pinky_a").gameObject;
        R_pinky_b = R_pinky_a.transform.Find("R_pinky_b").gameObject;
        R_pinky_c = R_pinky_b.transform.Find("R_pinky_c").gameObject;

        R_ring_meta = R_Palm.transform.Find("R_ring_meta").gameObject;
        R_ring_a = R_ring_meta.transform.Find("R_ring_a").gameObject;
        R_ring_b = R_ring_a.transform.Find("R_ring_b").gameObject;
        R_ring_c = R_ring_b.transform.Find("R_ring_c").gameObject;
        
        R_thumb_meta = R_Palm.transform.Find("R_thumb_meta").gameObject;
        R_thumb_a = R_thumb_meta.transform.Find("R_thumb_a").gameObject;
        R_thumb_b = R_thumb_a.transform.Find("R_thumb_b").gameObject;
        /*In start function grab all the objects you need in one time*/
        /*在Start函式裡,在開始時抓取所有需要的物件,比較省效能*/

        //Using invoke stop the acquire function within 20s.
        Invoke("Stop",20f);
        //使用Invoke函式, 程式執行後20秒自動停止

    }

    // Update is called once per frame
    void Update()
    {
        if(allowToCollcet == true){
        if(state)//when state is true operate the write function.
        {write();}
        }
    }
    void write()
    {
        //創建並寫入於txt檔
        StreamWriter sw = new StreamWriter(@"C:\\Gesture_data_acquire_unity\Liang_disgust.txt", true);//9 people get
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
            L_index_c.transform.rotation.x.ToString()+","+
            L_index_c.transform.rotation.y.ToString()+","+
            L_index_c.transform.rotation.z.ToString()+";"+
            "L_Index_end_Position"+":"+
            L_index_c.transform.position.x.ToString()+","+
            L_index_c.transform.position.y.ToString()+","+
            L_index_c.transform.position.z.ToString()+";"+
            "L_middle_end_Angle"+":"+
            L_middle_c.transform.rotation.x.ToString()+","+
            L_middle_c.transform.rotation.y.ToString()+","+
            L_middle_c.transform.rotation.z.ToString()+";"+
            "L_middle_end_Position"+":"+
            L_middle_c.transform.position.x.ToString()+","+
            L_middle_c.transform.position.y.ToString()+","+
            L_middle_c.transform.position.z.ToString()+";"+
            "L_ring_end_Angle"+":"+
            L_ring_c.transform.rotation.x.ToString()+","+
            L_ring_c.transform.rotation.y.ToString()+","+
            L_ring_c.transform.rotation.z.ToString()+";"+
            "L_ring_end_Position"+":"+
            L_ring_c.transform.position.x.ToString()+","+
            L_ring_c.transform.position.y.ToString()+","+
            L_ring_c.transform.position.z.ToString()+";"+
            "L_pinky_end_Angle"+":"+
            L_pinky_c.transform.rotation.x.ToString()+","+
            L_pinky_c.transform.rotation.y.ToString()+","+
            L_pinky_c.transform.rotation.z.ToString()+";"+
            "L_pinky_end_Position"+":"+
            L_pinky_c.transform.position.x.ToString()+","+
            L_pinky_c.transform.position.y.ToString()+","+
            L_pinky_c.transform.position.z.ToString()+";"+
            "L_thumb_end_Angle"+":"+
            L_thumb_b.transform.rotation.x.ToString()+","+
            L_thumb_b.transform.rotation.y.ToString()+","+
            L_thumb_b.transform.rotation.z.ToString()+";"+
            "L_thumb_end_Position"+":"+
            L_thumb_b.transform.position.x.ToString()+","+
            L_thumb_b.transform.position.y.ToString()+","+
            L_thumb_b.transform.position.z.ToString()+";"+
            "R_Index_end_Angle"+":"+
            R_index_c.transform.rotation.x.ToString()+","+
            R_index_c.transform.rotation.y.ToString()+","+
            R_index_c.transform.rotation.z.ToString()+";"+
            "R_Index_end_Position"+":"+
            R_index_c.transform.position.x.ToString()+","+
            R_index_c.transform.position.y.ToString()+","+
            R_index_c.transform.position.z.ToString()+";"+
            "R_middle_end_Angle"+":"+
            R_middle_c.transform.rotation.x.ToString()+","+
            R_middle_c.transform.rotation.y.ToString()+","+
            R_middle_c.transform.rotation.z.ToString()+";"+
            "R_middle_end_Position"+":"+
            R_middle_c.transform.position.x.ToString()+","+
            R_middle_c.transform.position.y.ToString()+","+
            R_middle_c.transform.position.z.ToString()+";"+
            "R_ring_end_Angle"+":"+
            R_ring_c.transform.rotation.x.ToString()+","+
            R_ring_c.transform.rotation.y.ToString()+","+
            R_ring_c.transform.rotation.z.ToString()+";"+
            "R_ring_end_Position"+":"+
            R_ring_c.transform.position.x.ToString()+","+
            R_ring_c.transform.position.y.ToString()+","+
            R_ring_c.transform.position.z.ToString()+";"+
            "R_pinky_end_Angle"+":"+
            R_pinky_c.transform.rotation.x.ToString()+","+
            R_pinky_c.transform.rotation.y.ToString()+","+
            R_pinky_c.transform.rotation.z.ToString()+";"+
            "R_pinky_end_Position"+":"+
            R_pinky_c.transform.position.x.ToString()+","+
            R_pinky_c.transform.position.y.ToString()+","+
            R_pinky_c.transform.position.z.ToString()+";"+
            "R_thumb_end_Angle"+":"+
            R_thumb_b.transform.rotation.x.ToString()+","+
            R_thumb_b.transform.rotation.y.ToString()+","+
            R_thumb_b.transform.rotation.z.ToString()+";"+
            "R_thumb_end_Position"+":"+
            R_thumb_b.transform.position.x.ToString()+","+
            R_thumb_b.transform.position.y.ToString()+","+
            R_thumb_b.transform.position.z.ToString();
                              
        sw.WriteLine(L_Palm_Data);
        sw.Flush();
        sw.Close();
    }

   // stop the acquire function
   void Stop()
    {
      state = false;
    }
    
}
