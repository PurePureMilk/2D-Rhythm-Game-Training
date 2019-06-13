Score and Multipliers #3

1. Update GameManager Script

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //this class is used to control when the music starts playing

    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    //used to only have one instance of the object
    public static GameManager instance;

    //keeps track of your score
    public int currentScore;
    //default value per note
    public int scorePerNote = 100;

    public Text scoreText;
    public Text multiText;

    //the current multiplier to add to score, ex. 1, 2, 3
    public int currentMultiplier;
    //keeps track on how many notes you have to the next multiplier threshold
    public int multiplierTracker;
    //the # of notes you need to hit the threshold, need 4 notes to get to 2, 8 notes to get to 3 etc.
    public int[] multiplierThresholds;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
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

        //prevents the counters from going beyond the index of the array
        if (currentMultiplier - 1 < multiplierThresholds.Length) { 
            //increment the multiplier track
            multiplierTracker++;
            //if the multiplierTracker now equals the multiplierThreshold, reset the count and increment the currentMultiplier 
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        multiText.text = "Multiplier: x" + currentMultiplier;

        currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    //used to see if you missed a note
    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        //when you miss, the multiplier resets
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
    }
}

2) Activate/Make a Canvas displaying Score and Text
3) In the fields of the GameManager Object’s Script, set Size to 3, element 0, 1, 2 are 4, 8, 16 respectively
