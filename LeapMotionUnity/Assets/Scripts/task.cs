using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task : MonoBehaviour {
    public GameObject[] tasks;
    public GameObject[] cups;
    Vector3 [] cupPositions;
    int taskIndex;
    bool excuteOnce = false;
    float x,y,z;

    void Start(){
        cupPositions = new Vector3[cups.Length];
        for(int i = 0; i<9; i++){
            cupPositions[i] = cups[i].transform.localPosition;
            x = Random.Range(-0.2f, 0.2f);
            y = Random.Range(-0.2f, 0.2f);
            z = Random.Range(-0.2f, 0.2f);
            cupPositions[i].x += x;
            cupPositions[i].y += y;
            cupPositions[i].z += z;
            cups[i].transform.localPosition = cupPositions[i];
        }
        Debug.Log(y);
    }

    public void assignTask() //this function call by StageRoot/Cube UI Panel/Cube UI Buttons/Cube UI Button(1)
    {
        if(!excuteOnce)
        {
            taskIndex = Random.Range(0, 3);
            Debug.Log(taskIndex);
            tasks[taskIndex].SetActive(true);           
            excuteOnce = true;
        }
    }

}