using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer theSR;
    //what the button looks like when it is not pressed
    public Sprite defaultImage;
    //what the button looks like when it is pressed/darken
    public Sprite pressedImage;

    //the key you want to press down
    public KeyCode keyToPress;

    // Use this for initialization
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //if the key is pressed, then the sprite turns to the pressed image
        if (Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = pressedImage;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImage;
        }


    }
}
