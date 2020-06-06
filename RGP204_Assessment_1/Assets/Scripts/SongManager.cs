using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    //the current position of the song (in seconds).
    public static float songPos;

    //the current position of the song (in beats).
    //public static float songPosInBeats;

    //the duration of a beat.
    public static float secPerBeat;

    public float songLength;

    //how much time (in seconds) has passed since the song started.
    public float dspTimeSong;

    //total tracks
    private int len;
    
    //index for each tracks
    private int[] trackNextIndices;
    public GameObject notePrefab;

    public float note;
    public AudioClip song;
    private AudioSource audioSource { get { return GetComponent<AudioSource>(); } }

    public int[] notes;

    public float startLineX, finishLineX, removeLineX;

    public SongInfo songInfo;

    private Queue<NoteNode>[] queueForTracks;
    private NoteNode[] previousNoteNodes;

    public SongInfo.Track[] tracks;

    [Header("Spawn point")]
    public float[] trackSpawnPosY;
    public static float BeatsShownOnScreen = 1f;

    void Start()
    {
        audioSource.clip = song;
        songLength = songInfo.song.length;

        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        secPerBeat = 60f / songInfo.bpm;

        //record the time when the song starts;
        dspTimeSong = (float)AudioSettings.dspTime;

        //Get the audio to play.
        GetComponent<AudioSource>().Play();

        len = trackSpawnPosY.Length;
        queueForTracks = new Queue<NoteNode>[len];
        previousNoteNodes = new NoteNode[len];
        trackNextIndices = new int[len];
        for(int i = 0; i < len; i++)
        {
            trackNextIndices[i] = 0;
            queueForTracks[i] = new Queue<NoteNode>();
            previousNoteNodes[i] = null;
        }
        tracks = songInfo.tracks; //keep a refernce of the tracks
                                  //Check the length of the song.
    }

    void Update()
    {
        //calculate the position in seconds.
        songPos = (float)(AudioSettings.dspTime - dspTimeSong);
        Debug.Log("Song Position: " + songPos);
        //calculate the position in beats.
        float songPosInBeats = songPos / secPerBeat + BeatsShownOnScreen;
        for (int i = 0; i < len; i++)
        {
            int nextIndex = trackNextIndices[i];
            SongInfo.Track currTrack = tracks[i];

            if (nextIndex < currTrack.notes.Length && currTrack.notes[nextIndex].note < songPosInBeats)
            {
                SongInfo.Note currNote = currTrack.notes[nextIndex];

                NoteNode noteNode = NoteObjPool.instance.GetNode(startLineX, finishLineX, trackSpawnPosY[i], 0, currNote.note, currNote.times, removeLineX);
                queueForTracks[i].Enqueue(noteNode);

                trackNextIndices[i]++;
            }
        }
    }
}
