using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldNoteObject : MonoBehaviour
{
    //whether the note can currently be hit with the button
    private bool canBePressed;
    //whether the end note can currently be hit with the button
    private bool canBeReleased = false;
    [Tooltip("the key that needs to be pressed to hit the note")]
    public KeyCode keyToPress;
    [Tooltip("the different effects to display when you get a good, great, perfect, or miss")]
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    [Tooltip("the x position of the center of the buttons, used for note offsets")]
    public float buttonPos;
    //whether or not the button is currently being held
    private bool held = false;
    [Tooltip("the initial arrow to hit")]
    public GameObject startArrow;
    [Tooltip("the final arrow to release on")]
    public GameObject endArrow;
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
                Debug.Log("held note start hit");
                //check for how close you are to the center of the button to determine score and make the correct hit effect show up
                float points;
                //if you are more than .25 away from the center then you get a good
                if(Mathf.Abs(transform.position.x - buttonPos) > greatOffset)
                {
                    points = GameManager.instance.good;
                    Debug.Log("good");
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                //if you are more than .1 away from the center of the button you get a great
                else if(Mathf.Abs(transform.position.x - buttonPos) > perfectOffset)
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
                //sets the first arrow to be inactive
                startArrow.SetActive(false);

                //update the score with the given number of points and show the note is now being held
                GameManager.instance.NoteHit(points);
                held = true;
                canBePressed = false;
            }
        }
        //if the correcct button is released
        if (Input.GetKeyUp(keyToPress))
        {
            //if the ending note can currently be hit
            if (canBeReleased)
            {
                Debug.Log("held note end hit");
                //check for how close you are to the center of the button to determine score and make the correct hit effect show up
                float points;
                //if you are more than .25 away from the center then you get a good
                if (Mathf.Abs(endArrow.transform.position.x - buttonPos) > greatOffset)
                {
                    points = GameManager.instance.good;
                    Debug.Log("good");
                    Instantiate(hitEffect, endArrow.transform.position, hitEffect.transform.rotation);
                }
                //if you are more than .1 away from the center of the button you get a great
                else if (Mathf.Abs(endArrow.transform.position.x - buttonPos) > perfectOffset)
                {
                    points = GameManager.instance.great;
                    Debug.Log("great");
                    Instantiate(goodEffect, endArrow.transform.position, goodEffect.transform.rotation);
                }
                //if you are less than .1 away from the center of the button you get a perfect
                else
                {
                    points = GameManager.instance.perfect;
                    Debug.Log("perfect");
                    Instantiate(perfectEffect, endArrow.transform.position, perfectEffect.transform.rotation);
                }
                //sets the second arrow to be inactive
                gameObject.SetActive(false);

                //update the score with the given number of points and show the note is no longer being held
                GameManager.instance.NoteHit(points);
                held = false;
            }
            //otherwise the note is released and can no longer be hit
            else
            {
                held = false;
                Debug.Log("note wasn't held");
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
        if (startArrow.gameObject.activeInHierarchy) { 
            if (other.tag == "Activator")
            {
                canBePressed = false;
    
                GameManager.instance.NoteMiss();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
                endArrow.gameObject.SetActive(false);
            }
        }
    }
    //Called when the ending note hits the button
    public void EnterEndTrigger()
    {
        Debug.Log("Entered End");
        //if the button has been held long enough, you can get points on the release
        if (held == true)
        {
            canBeReleased = true;
        }
    }
    //Called when the ending note leaves the button
    public void ExitEndTrigger()
    {
        //if the note has not been deactivate in the heirarchy, then there is a fail
        if (gameObject.activeInHierarchy)
        {
            held = false;
            canBeReleased = false;
            GameManager.instance.NoteMiss();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            Debug.Log("held note missed");
        }
    }
}
