using UnityEngine;
using System.Collections;
using Leap;

public class Main : MonoBehaviour {

    //Controller controller;

	// Use this for initialization
	void Start () {

        //controller = new Controller();
        //controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
    }
	
	// Update is called once per frame
	void Update () {

        GameObject indexPoint = GameObject.Find("Index-Tip");
        if (indexPoint != null)
        {
            if (indexPoint.transform.position.magnitude > 1.0f)
            {
                Debug.Log("Hand leaving sphere");

                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Renderer r = sphere.GetComponent<Renderer>();
                r.material.color = Color.red;
                sphere.transform.localScale = indexPoint.transform.position.normalized * 100.0f;
            }

            //GameObject hudValueButton = GameObject.Find("HUD-Value");
            GameObject hud = GameObject.Find("HUD");
            if (hud != null)
            {
                GameObject hudDiagnosticsButton = GameObject.Find("HUD-Diagnostics");
                TextMesh hudText = GameObject.Find("HUD-Text").GetComponent<TextMesh>();

                Debug.Log("Calculating distance");

                if ((indexPoint.transform.position - hudDiagnosticsButton.transform.position).magnitude < 0.283f)
                {
                    hudText.text = "Diagnosis:\nLocation: (0, 0)\nHealth: 87%\nIssues: None";
                }
                else
                {
                    hudText.text = "";
                }
            }

        }


        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
