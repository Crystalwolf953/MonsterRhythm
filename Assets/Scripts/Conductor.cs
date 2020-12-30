using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conductor : MonoBehaviour
{
    // =============================
    // For more details on how this works, look at this: https://www.gamasutra.com/blogs/GrahamTattersall/20190515/342454/Coding_to_the_Beat__Under_the_Hood_of_a_Rhythm_Game_in_Unity.php
    // =============================

    [Header("Debugging")]

    public Text debugText;

    [Header("General")]
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //The number of seconds for each song beat
    public float secPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public float dspSongTime;

    [Header("Phase Looping")]
    public float beatsPerPhase = 128;
    public int phaseLoops = 0;
    public float phaseLoopBeat;

    [Header("Sections")]
    public float beatsPerSection = 64;
    public int sectionLoops = 0;
    public float sectionLoopBeat;
    public string sectionName;

    // Start Tracking
    public bool tracking = false;

    public void StartTimekeeping()
    {
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        //Allow Tracking
        tracking = true;
    }

    public void StopTimekeeping()
    {
        //Record the time when the music starts
        dspSongTime = 0;

        //Allow Tracking
        tracking = false;

        sectionLoops = 0;
        phaseLoops = 0;
    }

    void Update()
    {
        if (tracking == true)
        {
            //determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime);

            //determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;

            //calculate the Phase position
            if (songPositionInBeats >= (phaseLoops + 1) * beatsPerPhase)
                phaseLoops++;
            phaseLoopBeat = songPositionInBeats - phaseLoops * beatsPerPhase;

            //calculate the Section position
            if (songPositionInBeats >= (sectionLoops + 1) * beatsPerSection)
                sectionLoops++;
            sectionLoopBeat = songPositionInBeats - sectionLoops * beatsPerSection;
            if (sectionLoops % 2 == 0)
            {
                sectionName = "Attack";
            } else
            {
                sectionName = "Defend";
            }

        }

        debugText.text = "BPM: " + songBpm + "\nSong Position (in beats): " + songPositionInBeats + "\n=====================" + "\nBeats per Phrase: " + beatsPerPhase + "\n# Phase Loops: " + phaseLoops + "\nBeats per Section: " + beatsPerSection + "\nCurrent Section: " + sectionName;
    }
}
