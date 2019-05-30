using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{

    public Image startBlink;

    public float interval = 1f;
    public float delay = 0.5f;
    public bool currentState = true;
    public bool defaultState = true;
    bool isBlinking = false;

    // Start is called before the first frame update
    void Start()
    {
        startBlink.enabled = defaultState;
        StartBlink();
    }

    public void StartBlink()
    {
        if(isBlinking)
        {
            return;
        }

        if(startBlink != null)
        {
            isBlinking = true;
            InvokeRepeating("ToggleState", delay, interval);
        }
    }

    public void ToggleState()
    {
        startBlink.enabled = !startBlink.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
