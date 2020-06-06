using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Sync : MonoBehaviour
{
    float songPosition;
    float songPosInBeats;

    float secPerBeat;
    float dspTimeSong;

    float bpm = 190;
    void Start()
    {
        //calculate how many seconds is one beat
        //declaration of bpm later
        secPerBeat = 60f / bpm;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the position in seconds
        songPosition = (float)(AudioSettings.dspTime - dspTimeSong);

        //Calculate the position in beats
        songPosInBeats = songPosition / secPerBeat;
    }
}
