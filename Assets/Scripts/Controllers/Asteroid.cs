using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    private Vector3 target = new Vector3();
    private Vector3 originalPosition = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        //set a target on project start so the asteroid has somewhere to go

        target = new Vector3(Random.Range(-1f, 1f),  //get a random range between max and min distance as x and y
                             Random.Range(-1f, 1f));

        target.Normalize(); //normalize to a max absolute magnitude of 1
        target *= maxFloatDistance; //multiply it by max distance to target the end of the radius
        target += transform.position;
        originalPosition = transform.position; //update original position and target to world space
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement(moveSpeed, arrivalDistance, maxFloatDistance);
    }

    void AsteroidMovement(float move, float arrival, float maxDist)
    {
        transform.position += (target - originalPosition).normalized * move * Time.deltaTime;
        
        if((target - transform.position).magnitude <= arrival)
        {
            target = new Vector3(Random.Range(-1f, 1f),  //get a random range between max and min distance as x and y
                             Random.Range(-1f, 1f));

            target.Normalize(); //normalize to a max absolute magnitude of 1
            target *= maxFloatDistance; //multiply it by max distance to target the end of the radius
            target += transform.position;
            originalPosition = transform.position; //update original position and target to world space
        }
    }
}
