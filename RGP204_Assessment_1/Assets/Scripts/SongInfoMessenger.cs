using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SongInfoMessenger : MonoBehaviour
{
    public static SongInfoMessenger Instance = null;
    public int characterIndex;
    public SongInfo currentSong;
    public AudioClip[] recordedBeats;

    void Start()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

}
