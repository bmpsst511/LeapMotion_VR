using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task : MonoBehaviour {
    public GameObject[] tasks;
    int taskIndex;
    bool excuteOnce = false;

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