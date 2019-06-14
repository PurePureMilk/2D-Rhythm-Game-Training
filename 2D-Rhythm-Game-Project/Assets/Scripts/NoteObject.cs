using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    //A script for the arrow/note objects detecting whether the note can be pressed based on position

    public bool canBePressed;
    public KeyCode keyToPress;

    //variables use to bring the effect in the world when a note is hit or missed
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    // Use this for initialization
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
                gameObject.SetActive(false);
                Destroy(this.gameObject);
                //if you hit the key when it canBePressed, print you hit a note
                //GameManager.instance.NoteHit();
                
                if(Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    //make a new effect object at the position of the arrow and with the rotation of the effect sprite
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);

                }
                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);

                }
            }
        }
    }

    //if the an arrow is colliding, then canBePressed is true
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(gameObject.transform.position);

        if (other.tag == "Activator")
        {
            canBePressed = false;

            float positionY = gameObject.transform.position.y;

            //when the note passed the buttons
            if (positionY < -0.419)
            {
                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
                Destroy(this.gameObject);


            }
        }
    }
}
