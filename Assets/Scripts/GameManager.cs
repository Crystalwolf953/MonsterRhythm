using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Tooltip("the music to be played")]
    public AudioSource music;
    //the music to be played when you are close to winning
    //public AudioSource winMusic;
    //whether or not the music has begun
    private bool startPlaying;
    [Tooltip("the beatscroller which contains all of the notes")]
    public BeatScroller theBS;
    [Tooltip("the single isntance of the game manager")]
    public static GameManager instance;
    //the current score
    private float score;
    //the current score multiplier
    private float multiplier;
    [Tooltip("the amount of points gained on hitting a note during the attack phase")]
    public float scorePerNote;
    [Tooltip("the amount of points lost for missing a note during the defend phase")]
    public float scorePerFail;
    [Tooltip("the text label for score")]
    public Text scoreText;
    [Tooltip("the text label for multiplier")]
    public Text multiText;
    [Tooltip("the point threshold to win the battle")]
    public float pointsToWin;
    //boolean for whether you are attacking, defending, or in the transition
    private bool atkOrDef;
    //boolean for if you're in a transition
    private bool transition;
    //for the phase integer, increases by 1 for every new phase, starts at 1 and ends at 3;
    private int phase;
    [Tooltip("the length of the attack phase in seconds")]
    public float attackTime;
    [Tooltip("the length of the defend phase in seconds")]
    public float defendTime;
    [Tooltip("the length of the transition phase in seconds")]
    public float transitionTime; 
    //the time at which the previous phase was started.
    private float startTime;
    [Tooltip("the modifier applied to a note score if you get a great hit")]
    public float great;
    [Tooltip("the modifier applied to a note score if you get a good hit")]
    public float good;
    [Tooltip("the modifier applied to a note score if you get a perfect hit")]
    public float perfect;

    // Start is called before the first frame update
    void Start()
    {
        //initializing all of the values;
        //pointsToWin = 2000;
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
        transitionTime = 2;
        perfect = 1;
        great = .8f;
        good = .5f;
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
                theBS.Play();
                music.Play();
                //winMusic.Play();
                //winMusic.mute = true;
                startTime = Time.time;
                atkOrDef = true;
            }
        }
        else
        {
            //If you're in the attacking phase and the time for the attack phase has passed, switch to defend and check if you've passed the point barrier to enter the next phase
            if(atkOrDef && Time.time > (startTime + attackTime))
            {
                
                startTime = Time.time;
                Debug.Log("defend");
                CheckPhase();
                atkOrDef = false;
            }
            //If you're in the defending phase and the time for the defend phase has passed, switch to attack and check if you've passed the point barrier to enter the next phase
            if (!atkOrDef && Time.time > (startTime + defendTime))
            {
                
                startTime = Time.time;
                Debug.Log("attack");
                CheckPhase();
                atkOrDef = true;
            }
            
        }
    }
    public void NoteHit(float hitType)
    {
        Debug.Log("Hit on time");
        //If it's the player's attack phase
        if (atkOrDef)
        {
            // "deal damage" by increasing the score
            score += scorePerNote * multiplier * hitType;
        }
        //increment the multiplier and update the text labels
        multiplier += 0.1f;
        scoreText.text = "Score: " + score;
        multiText.text = "Multiplier: x" + multiplier;
    }
    public void NoteMiss()
    {
        Debug.Log("Missed note");
        //if it's the player's defend phase
        if (!atkOrDef)
        {
            //calculate a portion of damage reduction based on your current multiplier
            float shield = (multiplier - 1) * scorePerNote * 0.5f;
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
        music.Stop();
        Debug.Log("You won");
    }
    public void CheckPhase()
    {
        //if you've passed the required amount of points to move on to the next phase, then shift forward a phase
        if(score >= pointsToWin)
        {
            //if you're in phase 1, enter phase 2 and shift to the correct spot in the music
            if(phase == 1)
            {
                music.time = attackTime + defendTime;
                phase = 2;
                Debug.Log("transitioning to phase 2");
                pointsToWin += 2000;
            }
            //if you're in phase 2, enter phase 3 and shift to the corret spot in the music
            else if(phase == 2)
            {
                music.time = (attackTime * 2) + (defendTime * 2) + transitionTime;
                phase = 3;
                Debug.Log("transitioning to phase 3");
                pointsToWin += 2000;
            }
            //if you're in phase 2, signal a win
            else
            {
                Win();
            }
        }
        //if you didn't move on to the next phase, check if you're attacking or defending
        else
        {
            //if you're defending, then shift the music to the necessary spot
            if (!atkOrDef)
            {
                if(phase == 1)
                {
                    music.time = 0;
                    Debug.Log("restarting phase 1 music");
                }
                else if(phase == 2)
                {
                    music.time = attackTime + defendTime + transitionTime;
                    Debug.Log("restarting phase 2 music");
                }
                else
                {
                    music.time = (attackTime * 2) + (defendTime * 2) + (transitionTime * 2);
                    Debug.Log("restarting phase 3 music");
                }
            }
        }
    }
}
