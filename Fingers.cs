using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
public class Fingers : MonoBehaviour
{    
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
    //[SerializeField]
     float L_Palm_Vel_X, L_Palm_Vel_Y,L_Palm_Vel_Z, R_Palm_Vel_X, R_Palm_Vel_Y, R_Palm_Vel_Z;
    void Start()
    {
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

    }

    // Update is called once per frame
    void Update()
    {
        print(L_index_c.transform.rotation.x.ToString());
    }
}
