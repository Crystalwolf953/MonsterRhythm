using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    public float bpm;
    public bool hasStarted;
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
            transform.position -= new Vector3(0f, scrollSpeed * bpm * Time.deltaTime, 0f);
        }
    }
}
