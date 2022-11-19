using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 StartPosition;
    private bool EndSpawn = true;
    private float Speed = 200f, distance = 100f, SecPerSpawn = 0.1f;
    public bool readyFire = false;
    private GameObject BulletCube;
    private IEnumerator coroutine;

    void Start()
    {
        if (gameObject.name != gameObject.transform.parent.transform.Find("Bullet").name)
        {
            StartPosition = gameObject.transform.parent.transform.Find("Bullet").transform.position;
            gameObject.transform.position = gameObject.transform.parent.transform.Find("Bullet").transform.position;
            gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * Speed, ForceMode.VelocityChange);
        }
    }

    IEnumerator SpawmCube()
    {
        yield return new WaitForSeconds(SecPerSpawn);
        BulletCube = Instantiate(gameObject, StartPosition, gameObject.transform.rotation);
        BulletCube.GetComponent<Rigidbody>().useGravity = true;
        BulletCube.transform.parent = gameObject.transform.parent;
        BulletCube.GetComponent<MeshRenderer>().enabled = true;
        EndSpawn = true;
    }

    void FixedUpdate()
    {
        if (readyFire)
        {
            if (gameObject.name != gameObject.transform.parent.transform.Find("Bullet").name)
            {
                // Ограничение скорости, иначе объект будет постоянно ускоряться
                if (Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.z) > Speed)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, gameObject.GetComponent<Rigidbody>().velocity.y, 
                                                                                Mathf.Sign(gameObject.GetComponent<Rigidbody>().velocity.z) * Speed);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (readyFire)
        {
            if (gameObject.transform.position.z - StartPosition.z > distance && gameObject.name != gameObject.transform.parent.transform.Find("Bullet").name)
            {
                Destroy(gameObject);
            }
            if (gameObject.name == gameObject.transform.parent.transform.Find("Bullet").name)
            {
                if (EndSpawn) //Если прошло необходимое время между спаунами
                {
                    EndSpawn = false;
                    coroutine = SpawmCube();
                    StartCoroutine(coroutine);
                }
            }
        }
    }
}
