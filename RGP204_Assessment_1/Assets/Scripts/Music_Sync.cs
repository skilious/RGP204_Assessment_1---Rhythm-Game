using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Sync : MonoBehaviour
{
    float songPosition;
    float songPosInBeats;

    float secPerBeat;
    float dspTimeSong;

    float bpm;
    void Start()
    {
        //calculate how many seconds is one beat
        //declaration of bpm later
        secPerBeat = 60f / bpm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
