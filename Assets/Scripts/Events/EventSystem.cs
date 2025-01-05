using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class TaskEvent : UnityEvent
{
    public bool IsInvoking { get; private set; }

    public new void Invoke()
    {
        IsInvoking = true;
        base.Invoke();
    }
    public void Devoke()
    {
        IsInvoking = false;
    }
}

[System.Serializable]
public class TaskInfo
{
    public MonoBehaviour targetScript;
    public string completeEventName;
    public string incompleteEventName;
    public string progressEventName;

    public GameObject complete;
    public GameObject incomplete;
    public TMP_Text progressText;

    public int totalProgress;
    private int progress;


    public void Subscribe()
    {
        if (targetScript != null)
        {
            // Find the UnityEvent in the target script
            System.Reflection.FieldInfo completeEventField = targetScript.GetType().GetField(completeEventName);

            if (completeEventField != null && (completeEventField.FieldType == typeof(TaskEvent) | completeEventField.FieldType == typeof(UnityEvent)))
            {
                // Get the current UnityEvent value
                TaskEvent scriptEvent = (TaskEvent)completeEventField.GetValue(targetScript);

                // Subscribe to the event
                scriptEvent.AddListener(Complete);
            }
            else
            {
                if(completeEventName != "")
                {
                    Debug.LogError($"UnityEvent '{completeEventName}' not found in script '{targetScript.GetType().Name}'.");
                }
            }


            // Find the UnityEvent in the target script
            System.Reflection.FieldInfo incompleteEventField = targetScript.GetType().GetField(incompleteEventName);

            if (incompleteEventField != null && (incompleteEventField.FieldType == typeof(TaskEvent) | incompleteEventField.FieldType == typeof(UnityEvent)))
            {
                // Get the current UnityEvent value
                TaskEvent scriptEvent = (TaskEvent)incompleteEventField.GetValue(targetScript);

                // Subscribe to the event
                scriptEvent.AddListener(Incomplete);
            }
            else
            {
                if(incompleteEventName != "")
                {
                    Debug.LogError($"UnityEvent '{incompleteEventName}' not found in script '{targetScript.GetType().Name}'.");
                }
            }


            // Find the UnityEvent in the target script
            System.Reflection.FieldInfo progessEventField = targetScript.GetType().GetField(progressEventName);

            if (progessEventField != null && (progessEventField.FieldType == typeof(TaskEvent) | progessEventField.FieldType == typeof(UnityEvent)))
            {
                // Get the current UnityEvent value
                TaskEvent scriptEvent = (TaskEvent)progessEventField.GetValue(targetScript);

                // Subscribe to the event
                scriptEvent.AddListener(Progress);
            }
            else
            {
                if(progressEventName != "")
                {
                    Debug.LogError($"UnityEvent '{progressEventName}' not found in script '{targetScript.GetType().Name}'.");
                }
            }
        }
        else
        {
            Debug.LogError("Target script or UnityEvent is not assigned.");
        }
    }

    public void Unsubscribe()
    {
        if (targetScript != null)
        {
            // Find the UnityEvent in the target script
            System.Reflection.FieldInfo completeEventField = targetScript.GetType().GetField(completeEventName);

            if (completeEventField != null && (completeEventField.FieldType == typeof(TaskEvent) | completeEventField.FieldType == typeof(UnityEvent)))
            {
                // Get the current UnityEvent value
                TaskEvent scriptEvent = (TaskEvent)completeEventField.GetValue(targetScript);

                // Subscribe to the event
                scriptEvent.RemoveListener(Complete);
            }
            else
            {
                if(completeEventName != "")
                {
                    Debug.LogError($"UnityEvent '{completeEventName}' not found in script '{targetScript.GetType().Name}'.");
                }
            }


            // Find the UnityEvent in the target script
            System.Reflection.FieldInfo incompleteEventField = targetScript.GetType().GetField(incompleteEventName);

            if (incompleteEventField != null && (incompleteEventField.FieldType == typeof(TaskEvent) | incompleteEventField.FieldType == typeof(UnityEvent)))
            {
                // Get the current UnityEvent value
                TaskEvent scriptEvent = (TaskEvent)incompleteEventField.GetValue(targetScript);

                // Subscribe to the event
                scriptEvent.RemoveListener(Incomplete);
            }
            else
            {
                if(incompleteEventName != "")
                {
                    Debug.LogError($"UnityEvent '{incompleteEventName}' not found in script '{targetScript.GetType().Name}'.");
                }
            }


            // Find the UnityEvent in the target script
            System.Reflection.FieldInfo progessEventField = targetScript.GetType().GetField(progressEventName);

            if (progessEventField != null && (progessEventField.FieldType == typeof(TaskEvent) | progessEventField.FieldType == typeof(UnityEvent)))
            {
                // Get the current UnityEvent value
                TaskEvent scriptEvent = (TaskEvent)progessEventField.GetValue(targetScript);

                // Subscribe to the event
                scriptEvent.RemoveListener(Progress);
            }
            else
            {
                if(progressEventName != "")
                {
                    Debug.LogError($"UnityEvent '{progressEventName}' not found in script '{targetScript.GetType().Name}'.");
                }
            }
        }
    }

    void Complete()
    {
        complete.SetActive(true);
        incomplete.SetActive(false);
    }

    void Incomplete()
    {
        incomplete.SetActive(true);
        complete.SetActive(false);
    }

    void Progress()
    {
        progress++;
        progressText.text = "(" + progress + "/" + totalProgress + ")";
    }
}

public class EventSystem : MonoBehaviour
{
    public TaskInfo[] taskInfos;
    private bool[] prevCompleted;

    [HideInInspector] public SoundEvent goalCompleteEvent;

    [Header("End State")]
    public bool isFullyComplete;
    public GameObject leaveText;
    public Doors labDoor;

    void Start() 
    {
        prevCompleted = new bool[taskInfos.Length];

        for (int i = 0; i < taskInfos.Length; i++)
        {
            taskInfos[i].Subscribe();
        }
    }

    void Update()
    {
        //Check If All Tasks Have Been Completed
        isFullyComplete = true;
        for (int i = 0; i < taskInfos.Length; i++)
        {
            if(!taskInfos[i].complete.activeSelf)
            {
                isFullyComplete = false;
            }
        }
        leaveText.SetActive(false);
        labDoor.isLocked = true;
        if(isFullyComplete)
        {
            leaveText.SetActive(true);
            labDoor.isLocked = false;
        }

        for (int i = 0; i < taskInfos.Length; i++) //Update Sound Event For Completed Tasks
        {
            if(taskInfos[i].complete.activeSelf && prevCompleted[i] != taskInfos[i].complete.activeSelf)
            {
                goalCompleteEvent.Invoke();
            }
        }

        for (int i = 0; i < prevCompleted.Length; i++) //Update prevCompleted For Sound Event
        {
            prevCompleted[i] = taskInfos[i].complete.activeSelf;
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < taskInfos.Length; i++)
        {
            taskInfos[i].Unsubscribe();
        }
    }
}
