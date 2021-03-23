using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    //whether the note can currently be hit with the button
    private bool canBePressed;
    [Tooltip("the key that needs to be pressed to hit the note")]
    public KeyCode keyToPress;
    [Tooltip("the different effects to display when you get a good, great, perfect, or miss")]
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    [Tooltip("the x position of the center of the buttons, used for note offsets")]
    public float buttonPos;
    [Tooltip("how close to the center of the button you have to be to get a perfect")]
    public float perfectOffset;
    [Tooltip("how close to the center of the button you have to be to get a great")]
    public float greatOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the correct button is pressed
        if (Input.GetKeyDown(keyToPress))
        {
            //if the note can currently be hit
            if (canBePressed)
            {
                //check for how close you are to the center of the button to determine score and make the correct hit effect show up
                float points;
                //if you are more than .25 away from the center then you get a good
                if (Mathf.Abs(transform.position.x - buttonPos) > greatOffset)
                {
                    points = GameManager.instance.good;
                    Debug.Log("good");
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                //if you are more than .1 away from the center of the button you get a great
                else if (Mathf.Abs(transform.position.x - buttonPos) > perfectOffset)
                {
                    points = GameManager.instance.great;
                    Debug.Log("great");
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                //if you are less than .1 away from the center of the button you get a perfect
                else
                {
                    points = GameManager.instance.perfect;
                    Debug.Log("perfect");
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                //sets the object to be inactive
                gameObject.SetActive(false);

                //update the score with the given number of points
                GameManager.instance.NoteHit(points);
            }
        }
    }
    //When the main note collider enters the button area, allows the note to be hit
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }
    //When the main note collider exits the button area, checks if the note has been hit or not, and triggers a miss if it has not
    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeInHierarchy) { 
            if (other.tag == "Activator")
            {
                canBePressed = false;
    
                GameManager.instance.NoteMiss();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
}
