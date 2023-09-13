using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public enum ExperimentType{Example1, Spring};
    public ExperimentType experimentType;

    [System.Serializable]
    public struct ExperimentTasks
    {
        public string[] tasks;

        public ExperimentTasks(string[] _tasks)
        {
            this.tasks = _tasks;
        }
    }
    public ExperimentTasks[] experimentTasks;
    public bool[] currentTaskList;
    public int currentStateID;

    // Start is called before the first frame update
    void Start()
    {
        if(experimentType == ExperimentType.Example1)
        {
            currentTaskList = new bool[experimentTasks[0].tasks.Length];
        }
        if(experimentType == ExperimentType.Spring)
        {
            currentTaskList = new bool[experimentTasks[1].tasks.Length];
        }

        currentStateID = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < currentTaskList.Length; i++)
        {
            if(currentStateID > i)
            {
                currentTaskList[i] = true;
            }
        }
    }
}
