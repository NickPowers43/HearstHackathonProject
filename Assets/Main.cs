using UnityEngine;
using System.Collections;
using Leap;

public enum MenuState
{
    Command = 0,
    Diagnostics = 1,
    Invest = 2,
}

public class Main : MonoBehaviour {

    //Controller controller;
    private MenuState state;
    // Use this for initialization
    void Start () {

        state = MenuState.Command;
        //controller = new Controller();
        //controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
    }

    bool outsideSphere = false;
    Vector3 prevIndexPos = Vector3.zero;

	// Update is called once per frame
	void Update () {

        GameObject camGO = GameObject.Find("CenterEyeAnchor");


        GameObject hud = GameObject.Find("HUD");
        if (hud != null)
        {
            hud.transform.LookAt(camGO.transform);

            GameObject indexPoint = GameObject.Find("Index-Tip");
            if (indexPoint != null)
            {
                if (indexPoint.transform.position.magnitude > 1.0f != outsideSphere)
                {
                    if (indexPoint.transform.position.magnitude > 1.0f)
                    {
                        hud.transform.position = indexPoint.transform.position * 0.8f;
                        Debug.Log("switching sides of the UI sphere");

                        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Renderer r = sphere.GetComponent<Renderer>();
                        r.material.color = Color.red;
                        sphere.transform.localScale = Vector3.one * 0.5f;
                        sphere.transform.position = indexPoint.transform.position.normalized * 100.0f;
                    }

                    outsideSphere = !outsideSphere;
                }


                TextMesh hudText = GameObject.Find("HUD-Text").GetComponent<TextMesh>();
                TextMesh hudTitle = GameObject.Find("HUD-Title").GetComponent<TextMesh>();
                
                Debug.DrawLine(prevIndexPos, indexPoint.transform.position, Color.red, 10.0f, false);
                prevIndexPos = indexPoint.transform.position;

                if (true)
                {
                    GameObject hudDiagnosticsButton = GameObject.Find("HUD-Diagnostics");
                    GameObject investButton = GameObject.Find("HUD-InvestReport");

                    if ((indexPoint.transform.position - hudDiagnosticsButton.transform.position).magnitude < 0.012f)
                    {
                        hudTitle.text = "Diagnosis";
                        hudText.text = "Location:  {lat: 40.708888, long: -73.986944}\nHealth:  65\nIssue: Potholes\nTraffic: heavy\nGenerated Revenue: $100k\nInvestment: $80\nLast Diagnose: 2015-09-25T09:18:33-04:00\nLast Service:  2015-10-25T09:18:33-04:00\nLast Investment: $10\n";
                        hudDiagnosticsButton.SetActive(false);
                        investButton.SetActive(false);

                        state = MenuState.Diagnostics;
                    }
                    else if ((indexPoint.transform.position - investButton.transform.position).magnitude < 0.012f)
                    {
                        hudTitle.text = "Invest/Report";
                        hudText.text = "You invested $35.0 to fix\nthe road!";
                        hudDiagnosticsButton.SetActive(false);
                        investButton.SetActive(false);

                        state = MenuState.Invest;
                    }
                    
                }
                
            }
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
