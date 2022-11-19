using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMonitoring : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bullet;
    private bool EndWaitOn, EndWaitFire;
    public Vector3 directionRay;
    IEnumerator coroutine;
    void Start()
    {
        
    }

    Vector3 CalculateDirection()
    {
        return Bullet.transform.position - gameObject.transform.position;
    }

    void DestroyBullets()
    {
        var Bullets = GameObject.FindGameObjectsWithTag("Bullet");
        Bullet.GetComponent<Fire>().readyFire = false;
        foreach (GameObject DelBullet in Bullets)
        {
            if (DelBullet.name != "Bullet")
            {
                Destroy(DelBullet);
            }
        }
    }
    IEnumerator WaitFewSeconds()
    {
        if (!EndWaitOn)
        {
            yield return new WaitForSeconds(2f);
            EndWaitOn = true;
            if (!EndWaitFire)
            {
                yield return new WaitForSeconds(2f);
                EndWaitFire = true;
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 originRay = Bullet.transform.position;
        directionRay = CalculateDirection();
        RaycastHit hit;
        if (Physics.Raycast(originRay, directionRay, out hit))
        {
            if (hit.collider.gameObject.name == "Raider")
            {
                coroutine = WaitFewSeconds();
                StartCoroutine(coroutine);
                if (EndWaitOn)
                {
                    gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    if (EndWaitFire)
                    {
                        Bullet.GetComponent<Fire>().readyFire = true;
                    }
                }
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
                DestroyBullets();
                EndWaitOn = false;
                EndWaitFire = false;
            }
        }
    }
}
