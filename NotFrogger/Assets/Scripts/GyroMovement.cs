using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroMovement : MonoBehaviour
{
    public Vector3 objectAcceleration = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        GetComponent<Camera>().transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        objectAcceleration.x = Input.acceleration.x;
        objectAcceleration.y = Input.acceleration.y;
        transform.position += objectAcceleration;
    }

    protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.acceleration " + Input.acceleration.x + " " + Input.acceleration.y + " " +Input.acceleration.z);
        GUILayout.Label("objectAcceleration " + objectAcceleration.x + " " + objectAcceleration.y + " " + objectAcceleration.z);
        GUILayout.Label("phone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
    }
}
