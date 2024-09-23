using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    public float velocity = 0.001f;
    private int controlsH;
    private int controlsV;

    void Update()
    {
        playerMovement(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), velocity);
    }

    void playerMovement(float horizontal, float vertical, float velocity)
    {
        transform.position = transform.position + new Vector3(horizontal * velocity * Time.deltaTime, vertical * velocity * Time.deltaTime);
    }

}
