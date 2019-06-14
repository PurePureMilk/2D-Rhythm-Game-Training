using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //this class is used to control when the music starts playing
    //keeps tracks of the effects of when a note is hit

    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    //used to only have one instance of the object
    public static GameManager instance;

    //keeps track of your score
    public int currentScore;
    //default value per note
    public int scorePerNote = 100;
    //value of a note with a good hit
    public int scorePerGoodNote = 125;
    //value of a note with a perfect hit
    public int scorePerPerfectNote = 150;

    //the current multiplier to add to score, ex. 1, 2, 3
    public int currentMultiplier;
    //keeps track on how many notes you have to the next multiplier threshold
    public int multiplierTracker;
    //the # of notes you need to hit the threshold, need 4 notes to get to 2, 8 notes to get to 3 etc.
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    //number of notes in the song
    public float totalNotes;
    //number of normal notes the player hit
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    //how to make the window popup with results
    public GameObject resultsScreen;
    //variables for the score text so that they are changeble according to player score
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        //find the number of note objects with a script to count the number of notes
        totalNotes = FindObjectsOfType<NoteObject>().Length;
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
        else //if the music started playing
        {
            //if the music ended, show the results
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);

                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;
                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal;
                if(percentHit >= 95)
                {
                    rankVal = "S";
                }
                else if (percentHit >= 90)
                {
                    rankVal = "A";
                }
                else if (percentHit >= 80)
                {
                    rankVal = "B";
                }
                else if (percentHit >= 70)
                {
                    rankVal = "C";
                }
                else if (percentHit >= 60)
                {
                    rankVal = "D";
                }
                else if (percentHit >= 50)
                {
                    rankVal = "E";
                }
                else
                {
                    rankVal = "F";
                }

                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
            }
        }
    }

    //used to see if you hit a note
    public void NoteHit()
    {
        //Debug.Log("Hit On Time");

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

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    //Is called in the Note Object Update Function to check the accuracy when a note can be pressed
    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }

    //used to see if you missed a note
    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        //when you miss, the multiplier resets
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
        missedHits++;
    }
}
