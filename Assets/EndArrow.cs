using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndArrow : MonoBehaviour
{
    [Tooltip("the main note object that contains this one")]
    public HeldNoteObject parentScript;

    //When the note ending enters the button, activate the method in the note
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            parentScript.EnterEndTrigger();
        }
    }
    //When the note ending exits the button, activate the method in the note
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            parentScript.ExitEndTrigger();
        }
        
    }
}
