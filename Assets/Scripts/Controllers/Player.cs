using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    public Vector3 velocity = new Vector3(0f, 0f);
    public float accelerationTime;
    public float maxSpeed;
    public float maxDeceleration;
    public float decelerationTime;

    void Update()
    {
        PlayerMovement(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), accelerationTime, maxSpeed, decelerationTime, maxDeceleration);
    }

    void PlayerMovement(float horizontal, float vertical, float accelTime, float maxSpd, float decelTime, float maxDecel)
    {
        float accel = (maxSpd) / accelTime;
        accel *= Time.deltaTime;
        velocity += new Vector3(horizontal * accel, vertical * accel);
        velocity = new Vector3(Mathf.Clamp(velocity.x, maxSpd * -1, maxSpd), Mathf.Clamp(velocity.y, maxSpd * -1, maxSpd)); //make sure speed doesn't exceed the limit

        transform.position = transform.position + (velocity * Time.deltaTime);

        float decel = (maxDecel) / decelTime;
        decel *= Time.deltaTime;
        if (Mathf.Abs(velocity.x) <= decel)
        {
            velocity.x = 0;
        } 
        else
        {
            velocity.x -= decel * Mathf.Sign(velocity.x); //get the sign of velocity and subtract decel times that from original velocity
        }
        if (Mathf.Abs(velocity.y) <= decel)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y -= decel * Mathf.Sign(velocity.y); //same as a bove, but for y
        }
        

    }

}
