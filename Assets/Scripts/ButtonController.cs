using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //the sprite rendered
    private SpriteRenderer theSR;
    [Tooltip("the image to display when the button isn't pressed")]
    public Sprite defaultImage;
    [Tooltip("the image to display when the button is pressed")]
    public Sprite pressedImage;

    public KeyCode keyToPress;
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //update if the button is pressed
        if (Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = pressedImage;
        }
        //update if the button is released
        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage;
        }
    }
}
