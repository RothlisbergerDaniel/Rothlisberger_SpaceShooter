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

    public float detectRadius;
    public int detectPoints;

    public GameObject powerupPrefab;
    public float powerupRadius;
    public int powerupCount;

    void Update()
    {
        PlayerMovement(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), accelerationTime, maxSpeed, decelerationTime, maxDeceleration);
        pointAtMouse();
        EnemyRadar(detectRadius, detectPoints);

        if(Input.GetKeyDown(KeyCode.P))
        {
            SpawnPowerups(powerupRadius, powerupCount);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            launchBomb();
        }
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

    void EnemyRadar(float radius, int circlePoints)
    {
        float angle;
        for (int i = 0; i < circlePoints; i++)
        {
            angle = (360 / circlePoints) * i;
            angle *= Mathf.Deg2Rad;
            Vector3 circlePoint1 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            circlePoint1 *= radius;
            angle = (360 / circlePoints) * (i + 1);
            angle *= Mathf.Deg2Rad;
            Vector3 circlePoint2 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            circlePoint2 *= radius;

            circlePoint1 += transform.position;
            circlePoint2 += transform.position;
            
            Vector3 inRadius = transform.position - enemyTransform.position;
            if(inRadius.magnitude <= radius)
            {
                Debug.DrawLine(circlePoint1, circlePoint2, Color.red);
            } else
            {
                Debug.DrawLine(circlePoint1, circlePoint2, Color.green);
            }
            
        }
    }

    void SpawnPowerups(float radius, int numberOfPowerups)
    {
        float angle;
        for (int i = 0; i < numberOfPowerups; i++)
        {
            angle = (360 / numberOfPowerups) * i;
            angle *= Mathf.Deg2Rad;
            Vector3 circlePoint = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
            circlePoint *= radius;

            circlePoint += transform.position;

            Instantiate(powerupPrefab, circlePoint, Quaternion.identity);
            

        }
    }

    void pointAtMouse()
    {
        Vector3 mouseActual = new Vector3();
        mouseActual = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get the mouse's position in world space
        Vector3 target = new Vector3();
        target = transform.position - mouseActual; //get the vector difference between player and mouse positions
        float aim;
        aim = Mathf.Atan2(target.y, target.x); //use target vector to calculate aim angle
        
        transform.rotation = Quaternion.identity; //reset rotation
        transform.Rotate(0, 0, (Mathf.Rad2Deg * aim) + 90); //rotate to point at the mouse - adding 90 makes it face forward.
    }

    void launchBomb()
    {
        GameObject launchedBomb = Instantiate(bombPrefab, transform.position, transform.rotation);
        launchedBomb.GetComponent<Bomb>().isLaunched = true; //create a bomb prefab, and update its "launched" status to true
    }

}
