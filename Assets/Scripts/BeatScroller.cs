using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [Tooltip("the beats per minute of the current audio")]
    public float bpm;
    //whether or not the music has started
    private bool hasStarted;
    [Tooltip("the speed at which the beatscroller will move")]
    public float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //calculate the speed at which the notes move
        bpm /= 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            //if (Input.anyKeyDown)
            //{
            //    hasStarted = true;
            //}
        }
        else
        {
            //move the notes down
            transform.position -= new Vector3(scrollSpeed * bpm * Time.deltaTime, 0f, 0f);
        }
    }
    //returns hasStarted
    public bool HasStarted()
    {
        return hasStarted;
    }

    //sets hasStarted to true
    public void Play()
    {
        hasStarted = true;
    }
    //sets hasStarted to false
    public void Stop()
    {
        hasStarted = false;
    }
}
