using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;

    public Vector3 velocity = new Vector3(0f, 0f);
    public float accelerationTime;
    public float maxSpeed;

    private Vector3 target = new Vector3(0f, 0f);

    private void Update()
    {
        target = playerTransform.position - transform.position;
        target.Normalize(); //normalize so acceleration doesn't end up going crazy

        EnemyMovement(target, accelerationTime, maxSpeed);
    }

    void EnemyMovement(Vector3 direction, float accelTime, float maxSpd)
    {
        float accel = (maxSpd) / accelTime;
        accel *= Time.deltaTime;
        velocity += direction * accel;
        velocity = new Vector3(Mathf.Clamp(velocity.x, maxSpd * -1, maxSpd), Mathf.Clamp(velocity.y, maxSpd * -1, maxSpd)); //make sure speed doesn't exceed the limit

        transform.position = transform.position + (velocity * Time.deltaTime);
    }

}
