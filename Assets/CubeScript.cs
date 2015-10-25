using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void updatePalm(Leap.Hand leapHand, GameObject palmObject)
    {
        if (leapHand.IsValid)
        {
            Vector3 position = new Vector3(leapHand.PalmPosition.x, leapHand.PalmPosition.y, leapHand.PalmPosition.z);
            palmObject.transform.localPosition = position;
        }
    }

    // Update is called once per frame
    void Update () {

        //HandController hc = HandController.Instance;

        //if (hc != null)
        //{
        //    Debug.Log("hc found");
        //    HandModel[] hands = hc.GetAllGraphicsHands();

        //    transform.position = hands[0].fingers[0].transform.position;
        //}
        //else
        //{
        //    Debug.Log("hc not found");
        //}

    }
}
