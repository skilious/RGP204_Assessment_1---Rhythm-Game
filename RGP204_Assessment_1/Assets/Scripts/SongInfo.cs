using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "Song Info")]
public class SongInfo : ScriptableObject
{
    public AudioClip song;

    public float bpm;

    public Track[] tracks;

    [System.Serializable]
    public class Note
    {
        public float note;
        public int times;
    }

    [System.Serializable]
    public class Track
    {
        public Note[] notes;
    }
}
