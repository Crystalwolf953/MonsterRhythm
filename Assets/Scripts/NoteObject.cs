using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                double points;
                if(Mathf.Abs(transform.position.y) > 0.25)
                {
                    points = GameManager.instance.good;
                    Debug.Log("good");
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if(Mathf.Abs(transform.position.y) > .1)
                {
                    points = GameManager.instance.great;
                    Debug.Log("great");
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    points = GameManager.instance.perfect;
                    Debug.Log("perfect");
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                gameObject.SetActive(false);

                
                GameManager.instance.NoteHit(points);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }
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
