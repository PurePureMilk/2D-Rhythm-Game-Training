1. Create a new script called GameManager
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //this class is used to control when the music starts playing

    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    //used to only have one instance of the object
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //when the game is booted up, the music and game should not start yet
        if (!startPlaying)
        {
            //if a button is pushed, start the beatscroller and music
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        }
    }

    //used to see if you hit a note
    public void NoteHit()
    {
        Debug.Log("Hit On Time");
    }

    //used to see if you missed a note
    public void NoteMissed()
    {
        Debug.Log("Missed Note");
    }
}
```

2. Create a empty object called Game Manager and add the GameManager script to it.
3. Add an audio source to “The Music” field and uncheck “Play On Awake” on the audio source
4. Add the BeatScroller object to “The BS” reference
5. Update the BeatScroller script so that it is not the one starting the game

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
            /*
            if (Input.anyKeyDown)
            {
                hasStarted = true;
            }
            */
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }

    }
}
```

6. Make sure the tempo of your scroller is the same as the music

Reference:
https://www.youtube.com/watch?v=PMfhS-kEvc0
