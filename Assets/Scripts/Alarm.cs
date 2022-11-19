using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    // Start is called before the first frame update
    public bool bAlarm = false;
    void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Raider"))
        {
            bAlarm = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Raider"))
        {
            bAlarm = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
