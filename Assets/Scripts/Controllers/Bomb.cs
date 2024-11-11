using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public Boolean isLaunched;

    public float launchSpeed;
    public float lifespan;
    private float timeActive;
    private Vector3 velocity = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        if(isLaunched) //update values if the bomb is "launched"
        {
            velocity = new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.rotation.z), Mathf.Cos(Mathf.Deg2Rad * transform.rotation.z), 0f);
            velocity.Normalize();
            velocity *= launchSpeed; //set velocity based on rotation angle
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isLaunched)
        {
            moveBomb(); //move the bomb, but only if it's "launched"
        }
    }

    void moveBomb()
    {
        transform.Translate(velocity * Time.deltaTime);
        timeActive += Time.deltaTime; //increment time alive
        if(timeActive > lifespan)
        {
            Destroy(gameObject); //destroy if it exceeds its lifespan
        }
    }
}
