using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    private int listPosition = 0;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        drawConstellation(drawingTime);
    }

    void drawConstellation(float time)
    {
        timer += Time.deltaTime; //track how far along the line is
        Vector3 direction = starTransforms[listPosition + 1].position - starTransforms[listPosition].position; //get the direction to draw the line in
        Debug.DrawLine(starTransforms[listPosition].position, starTransforms[listPosition].position + (direction * timer * (1 / time)), Color.white);
                                                                                                     //Multiply by the timer (drawing progress) times the
                                                                                                     //reciprocal of drawing time to get where the line should
                                                                                                     //be at any point in time
        if(timer >= time)
        {
            timer = 0; //reset timer
            listPosition++; //increment the start to draw from
            if(listPosition >= starTransforms.Count - 1) //count as less than one so it doesn't try to go out of bounds
            {
                listPosition = 0; //reset list position
            }
        }
    }
}
