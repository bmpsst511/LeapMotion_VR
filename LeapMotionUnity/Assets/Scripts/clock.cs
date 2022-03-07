using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class clock : MonoBehaviour {
    [SerializeField] TextMeshProUGUI m_Object;
    private float m_Timer;
    private int m_Hour;//時
    private int m_Minute;//分
    private int m_Second;//秒

    bool startCount = false;

// Use this for initialization
    void Start () 
    {

    }

// Update is called once per frame
    void Update () 
    {
        if(startCount)
        {
            m_Timer += Time.deltaTime;
            m_Second = (int)m_Timer;     
            if (m_Second > 59.0f)
            {
                m_Second = (int)(m_Timer - (m_Minute * 60));
            }
            m_Minute = (int)(m_Timer / 60);       
            if (m_Minute > 59.0f)
            {
                m_Minute = (int)(m_Minute - (m_Hour * 60));
            }
            m_Hour = m_Minute / 60;
            if (m_Hour >= 24.0f)
            {
                m_Timer = 0;
            }
            m_Object.text = string.Format("{0:d2}:{1:d2}:{2:d2}", m_Hour,m_Minute,m_Second); 
        }
    }

    public void activate() //this function call by StageRoot/Cube UI Panel/Cube UI Buttons/Cube UI Button
    {
        startCount = true;
    }
    
    public void stop() //this function call by StageRoot/Cube UI Panel/Cube UI Buttons/Cube UI Button(1)
    {
        startCount = false;
    }
}