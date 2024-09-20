using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject BallPrefab;
    public GameObject BallsHolder;
    public float speed;

    //Detect when press the trigger
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            GameObject ball = Instantiate(BallPrefab, transform.position, Quaternion.identity, BallsHolder.transform);
            ball.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
    }

    public void DeleteAllBalls()
    {
        foreach(Transform ball in BallsHolder.transform)
        {
            Destroy(ball.gameObject);
        }
    }
}
