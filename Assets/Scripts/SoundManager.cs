using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SoundEvent : UnityEvent
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
public class SoundInfo
{
    public bool doesLoop;
    [HideInInspector] public AudioSource audioSource;
    public AudioClip audioClip;
    public MonoBehaviour targetScript;
    public string eventName;
    [HideInInspector] public SoundEvent triggerEvent;

    public void Subscribe()
    {
        if (targetScript != null)
        {
            // Find the UnityEvent in the target script
            System.Reflection.FieldInfo eventField = targetScript.GetType().GetField(eventName);

            if (eventField != null && (eventField.FieldType == typeof(SoundEvent) | eventField.FieldType == typeof(UnityEvent)))
            {
                // Get the current UnityEvent value
                SoundEvent scriptEvent = (SoundEvent)eventField.GetValue(targetScript);

                // Subscribe to the event
                scriptEvent.AddListener(Play);
                triggerEvent = scriptEvent;
            }
            else
            {
                Debug.LogError($"UnityEvent '{eventName}' not found in script '{targetScript.GetType().Name}'.");
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
            System.Reflection.FieldInfo eventField = targetScript.GetType().GetField(eventName);

            if (eventField != null && (eventField.FieldType == typeof(SoundEvent) | eventField.FieldType == typeof(UnityEvent)))
            {
                // Get the current UnityEvent value
                SoundEvent scriptEvent = (SoundEvent)eventField.GetValue(targetScript);

                // Unsubscribe from the event
                scriptEvent.RemoveListener(Play);
            }
        }
    }

    void Play()
    {
        if (audioSource != null && audioClip != null)
        {
            if(!audioSource.gameObject.activeSelf)
            {
                audioSource.gameObject.SetActive(true);
            }
            if(audioSource.mute)
            {
                audioSource.mute = false;
            }
            audioSource.clip = audioClip;


            if(doesLoop)
            {
                audioSource.loop = true;
                if(!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.loop = false;
                audioSource.Play();
            }
        }
    }

    public void Pause() //Meant For Looping Audio Sources
    {
        audioSource.mute = true;
    }
}

public class SoundManager : MonoBehaviour
{
    public GameObject prefab;
    public SoundInfo[] soundInfos;

    void Start()
    {   
        for (int i = 0; i < soundInfos.Length; i++)
        {
            GameObject source = GameObject.Instantiate(prefab, transform);
            soundInfos[i].audioSource = source.GetComponent<AudioSource>();
            soundInfos[i].Subscribe();
        }
    }

    void Update()
    {
        for (int i = 0; i < soundInfos.Length; i++) //Meant For Looping Audio Sources
        {
            if(soundInfos[i].doesLoop && !soundInfos[i].triggerEvent.IsInvoking)
            {
                soundInfos[i].Pause();
            }
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < soundInfos.Length; i++)
        {
            soundInfos[i].Unsubscribe();
        }
    }
}