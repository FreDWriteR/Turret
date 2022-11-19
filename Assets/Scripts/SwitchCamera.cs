using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DemoStatick, DemoTrack, PlayerView;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DemoStatick.GetComponent<Camera>().enabled = true;
            DemoTrack.GetComponent<Camera>().enabled = false;
            PlayerView.GetComponent<Camera>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DemoStatick.GetComponent<Camera>().enabled = false;
            DemoTrack.GetComponent<Camera>().enabled = true;
            PlayerView.GetComponent<Camera>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerView.GetComponent<Camera>().enabled = true;
            DemoStatick.GetComponent<Camera>().enabled = false;
            DemoTrack.GetComponent<Camera>().enabled = false;
        }
    }
}
