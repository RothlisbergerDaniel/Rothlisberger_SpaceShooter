using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;
    public float orbitRadius;
    public float orbitSpeed;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(orbitRadius, orbitSpeed, planetTransform);
    }

    void OrbitalMotion(float radius, float speed, Transform target)
    {
        angle += speed * Time.deltaTime; //add to the angle - constantly rotates

        Vector3 circlePoint = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)); //gets the relevant point from the origin,
                                                                                                               //and converts to radians
        circlePoint *= radius;                    //multiply by radius
        circlePoint += target.transform.position; //and add the transform position so it orbits around the "new origin"

        transform.position = circlePoint; //then update position

    }


}
