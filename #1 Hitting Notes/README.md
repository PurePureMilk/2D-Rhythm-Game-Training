1) Import a package with assets with buttons and arrow sprites
2) Create a folder for scripts
3) Add a new script call Button Controller

```
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
```

4) Add this script to each colored button.
5) Assign the correct sprite image in the field of the button controller script
6) Assign the correct Key Input for each button for “Key To Press”
7) Add an arrow to the scene and set up the correct orientations 
* A trick for an arrow to be perfectly aligned to the button is to make the arrow a child of the button and set the X position to 0
8) Fix the layers by adding a new sorting layer for “Arrows” so the arrows appear before the buttons
9) Create a Noteholder game object to hold all the arrows
10) Add a new script called BeatScroller
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    public float beatTempo;

    public bool hasStarted;

    // Use this for initialization
    void Start()
    {
        //120/60 = 2 notes per sec, for example

        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            if (Input.anyKeyDown)
            {
                hasStarted = true;
            }
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }


    }
}
```

11) Set the beat tempo in the inspector to 120
12) Create a script called NoteObject
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;
    public KeyCode keyToPress;

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
        if (other.tag == "Activator")
        {
            canBePressed = false;
        }
    }
}
```

13) Add a 2D Box Collider to all the buttons and a 2D Circular Collider to the arrows
14) Add a 2D Rgidbody to the buttons and change the body type to Kinematic
15) Make sure the buttons have the tag Activator and the 2D box collider have “Is Trigger” checked off
16) Add more arrows to the scene and set the “Key To Press” to the correct input value for each type of arrow

Reference
https://www.youtube.com/watch?v=cZzf1FQQFA0


