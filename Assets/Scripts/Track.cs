using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Track : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Raider, Room;
    private bool EndWaitToTargeting = false, EndWaitToReturn = false, BeginReturn = true;
    IEnumerator coroutine;

    void Start()
    {
        
    }

    IEnumerator Targeting()
    {
        float damping = 1.2f;
        BeginReturn = true;
        if (!EndWaitToTargeting) {
            EndWaitToReturn = false;
            yield return new WaitForSeconds(2f);
            EndWaitToTargeting = true;
        }

        if (EndWaitToTargeting)
        {
            Vector3 lookPos = Raider.transform.position - gameObject.transform.position;
            Quaternion CurrentRotationTurret = gameObject.transform.rotation;
            Quaternion rotationTurret = Quaternion.LookRotation(lookPos);
            gameObject.transform.rotation = Quaternion.Slerp(CurrentRotationTurret, rotationTurret, Time.deltaTime * damping);
        }
    }

    IEnumerator ReturnToBeginPosition()
    {
        if (BeginReturn)
        {
            float t = 0;
            BeginReturn = false;
            if (!EndWaitToReturn)
            {
                EndWaitToTargeting = false;
                yield return new WaitForSeconds(2f);
                EndWaitToReturn = true;
            }
            if (EndWaitToReturn)
            {
                Quaternion CurrentRotationTurret = gameObject.transform.rotation;
                Quaternion rotationTurret = new (0, 0, 0, 1);
                while (gameObject.transform.rotation != rotationTurret)
                {
                    gameObject.transform.rotation = Quaternion.Slerp(CurrentRotationTurret, rotationTurret, t);
                    t += 3f * Time.deltaTime;
                    yield return new WaitForSeconds(0.005f);

                }
            }

        }
        
    }




    // Update is called once per frame
    void Update()
    {
        if (Room.GetComponent<Alarm>().bAlarm)
        {
            coroutine = Targeting();
            StartCoroutine(coroutine);
        }
        else
        {
            coroutine = ReturnToBeginPosition();
            StartCoroutine(coroutine);
        }
    }
}
