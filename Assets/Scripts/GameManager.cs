using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //the music to be played
    public AudioSource music;
    //the music to be played when you are close to winning
    //public AudioSource winMusic;
    //whether or not the music has begun
    public bool startPlaying;
    //the beatscroller which controlls all of the notes
    public BeatScroller theBS;
    //the single isntance of the game manager
    public static GameManager instance;
    //the current score
    public double score;
    //the current score multiplier
    public double multiplier;
    //the amount of points gained on hitting a note during the attack phase
    public double scorePerNote;
    //the amount of points lost for missing a note during the defend phase
    public double scorePerFail;
    //the text label for score
    public Text scoreText;
    //the text label for multiplier
    public Text multiText;
    //the point threshold to win the battle
    public double pointsToWin;
    //for the phase integer, 1 is attacking, 2 is defending, 3 is the final leg phase idk what it's called.
    public int phase;
    //the length of the attack phase in seconds
    public double attackTime;
    //the length of the defend phase in seconds
    public double defendTime;
    //the time at which the previous phase was started.
    public float startTime;
    //the modifier applied to a note score if you get a great hit
    public double great;
    //the modifier applied to a note score if you get a good hit
    public double good;
    //the modifier applied to a note score if you get a perfect hit
    public double perfect;

    // Start is called before the first frame update
    void Start()
    {
        //initializing all of the values;
        pointsToWin = 2000;
        instance = this;
        score = 1000;
        phase = 1;
        scorePerNote = 100;
        scorePerFail = 200;
        multiplier = 1;
        scoreText.text = "Score: " + score;
        multiText.text = "Multiplier: x" + 1;
        attackTime = 27;
        defendTime = 26;
        perfect = 1;
        great = .8;
        good = .5;
    }

    // Update is called once per frame
    void Update()
    {
        //if the game has not yet started
        if (!startPlaying)
        {
            //if any key is pressed
            if (Input.anyKeyDown)
            {
                //the game starts and the music begins to play
                startPlaying = true;
                theBS.hasStarted = true;
                music.Play();
                //winMusic.Play();
                //winMusic.mute = true;
                startTime = Time.time;
            }
        }
        else
        {
            if(phase == 1 && Time.time > (startTime + attackTime))
            {
                phase = 2;
                startTime = Time.time;
            }
            if(phase == 2 && Time.time > (startTime + defendTime))
            {
                phase = 1;
                startTime = Time.time;
            }
        }
    }
    public void NoteHit(double hitType)
    {
        Debug.Log("Hit on time");
        //If it's the player's attack phase
        if (phase == 1 || phase == 3)
        {
            // "deal damage" by increasing the score
            score += scorePerNote * multiplier * hitType;
        }
        //If it's the player's defend phase
        if(phase == 2)
        {
            //calculate a portion of damage reduction based on your current multiplier
            double shield = (multiplier - 1) * scorePerNote;
            //the the damage reduction is less than the damage taken, reduce your score by the remaining damage
            if (shield < scorePerFail)
            {
                score -= (scorePerFail - shield) * (1 - hitType);
            }
        }
        //increment the multiplier and update the text labels
        multiplier += 0.1;
        scoreText.text = "Score: " + score;
        multiText.text = "Multiplier: x" + multiplier;
    }
    public void NoteMiss()
    {
        Debug.Log("Missed note");
        //if it's the player's defend phase
        if (phase == 2)
        {
            //calculate a portion of damage reduction based on your current multiplier
            double shield = (multiplier - 1) * scorePerNote;
            //the the damage reduction is less than the damage taken, reduce your score by the remaining damage
            if (shield < scorePerFail) {
                score -= (scorePerFail - shield);
            }
            //update text label
            scoreText.text = "Score: " + score;
        }
        //reset multiplier to 1 and update the text label
        multiplier = 1;
        multiText.text = "Multiplier: x" + multiplier;
    }
    public void GameOver()
    {
        Debug.Log("You lost");
    }
    public void Win()
    {
        Debug.Log("You won");
    }
}
