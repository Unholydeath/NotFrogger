// Create a cube with camera vector names on the faces.
// Allow the device to show named faces as it is oriented.

using UnityEngine;

public class Gyroscope : MonoBehaviour
{
    // Faces for 6 sides of the cube
    private GameObject[] quads = new GameObject[6];

    // Textures for each quad, should be +X, +Y etc
    // with appropriate colors, red, green, blue, etc
    public Texture[] labels;
    public Vector3 deviceAcceleration;
    private Quaternion deviceRotation;
    float timer = 0f;
    float speed = 1000f;

    void Start()
    {
        // make camera solid colour and based at the origin
        //GetComponent<Camera>().backgroundColor = new Color(51.0f / 255.0f, 153.0f / 255.0f, 255.0f / 255.0f);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        GetComponent<Camera>().transform.position = new Vector3(0, 0, 0);
        GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;

        // create the six quads forming the sides of a cube
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);

        quads[0] = createQuad(quad, new Vector3(1, 0, 0), new Vector3(0, 90, 0), "plus x",
            new Color(0.90f, 0.10f, 0.10f, 1), labels[0]);
        quads[1] = createQuad(quad, new Vector3(0, 1, 0), new Vector3(-90, 0, 0), "plus y",
            new Color(0.10f, 0.90f, 0.10f, 1), labels[1]);
        quads[2] = createQuad(quad, new Vector3(0, 0, 1), new Vector3(0, 0, 0), "plus z",
            new Color(0.10f, 0.10f, 0.90f, 1), labels[2]);
        quads[3] = createQuad(quad, new Vector3(-1, 0, 0), new Vector3(0, -90, 0), "neg x",
            new Color(0.90f, 0.50f, 0.50f, 1), labels[3]);
        quads[4] = createQuad(quad, new Vector3(0, -1, 0), new Vector3(90, 0, 0), "neg y",
            new Color(0.50f, 0.90f, 0.50f, 1), labels[4]);
        quads[5] = createQuad(quad, new Vector3(0, 0, -1), new Vector3(0, 180, 0), "neg z",
            new Color(0.50f, 0.50f, 0.90f, 1), labels[5]);

        GameObject.Destroy(quad);
    }

    // make a quad for one side of the cube
    GameObject createQuad(GameObject quad, Vector3 pos, Vector3 rot, string name, Color col, Texture t)
    {
        Quaternion quat = Quaternion.Euler(rot);
        GameObject GO = Instantiate(quad, pos, quat);
        GO.name = name;
        GO.GetComponent<Renderer>().material.color = col;
        GO.GetComponent<Renderer>().material.mainTexture = t;
        GO.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
        return GO;
    }

    protected void Update()
    {
        //timer++;
        float curSpeed = Time.deltaTime * speed;
        deviceAcceleration = Input.acceleration;
        //deviceRotation.y -= Input.acceleration.y * speed;
        //deviceRotation.x -= Input.acceleration.x * speed;
        //deviceRotation.z -= Input.acceleration.z * speed;
        transform.rotation = Quaternion.LookRotation(deviceAcceleration, Vector3.up);
    }

    protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.acceleration " + deviceAcceleration);
        GUILayout.Label("device Rotation " + deviceRotation);
        GUILayout.Label("phone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
        GUILayout.Label("timer: " + timer);
    }

    /********************************************/

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
      
    }

}