using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    public Vector3 velocity = new Vector3(0f, 0f);
    public float accelerationTime;
    public float maxSpeed;
    public float friction;

    void Update()
    {
        playerMovement(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), accelerationTime, maxSpeed);
    }

    void playerMovement(float horizontal, float vertical, float accelTime, float maxSpd)
    {
        float accel = (maxSpd * Time.deltaTime) / accelTime;
        accel *= Time.deltaTime;
        velocity += new Vector3(horizontal * accel, vertical * accel);
        velocity *= 1 - (friction * Time.deltaTime);
        transform.position = transform.position + velocity;
        
    }

}
