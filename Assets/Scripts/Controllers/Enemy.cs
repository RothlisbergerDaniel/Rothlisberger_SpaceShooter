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
        pointAtPlayer(target);
    }

    void EnemyMovement(Vector3 direction, float accelTime, float maxSpd)
    {
        float accel = (maxSpd) / accelTime;
        accel *= Time.deltaTime;
        velocity += direction * accel;
        velocity = new Vector3(Mathf.Clamp(velocity.x, maxSpd * -1, maxSpd), Mathf.Clamp(velocity.y, maxSpd * -1, maxSpd)); //make sure speed doesn't exceed the limit

        transform.position = transform.position + (velocity * Time.deltaTime);
    }

    void pointAtPlayer(Vector3 direction)
    {
        float aim = Mathf.Atan2(direction.y, direction.x);  //set aim angle to the angle of the given target vector (player position - enemy position)
        transform.rotation = Quaternion.identity;           //then reset rotation
        transform.Rotate(0, 0, (Mathf.Rad2Deg * aim) - 90); //so that the ship can rotate to the correct angle.
                                                            //Subtract 90 degrees to make the front point to the player.
    }

}
