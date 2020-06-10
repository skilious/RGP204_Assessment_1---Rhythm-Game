using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public delegate void BeatOnHitAction(int trackNumber);
    public static event BeatOnHitAction beatOnHitEvent;

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

    public AudioClip song;
    private AudioSource audioSource { get { return GetComponent<AudioSource>(); } }

    public int[] notes;

    public float finishLineX, finishLineY, removeLineX;
    public float[] startLineX, startLineY;

    public SongInfo songInfo;

    private Queue<NoteNode>[] queueForTracks;
    private NoteNode[] previousNoteNodes;

    public SongInfo.Track[] tracks;

    [Header("Spawn point")]
    public float[] trackSpawnPosY;

    public static float BeatsShownOnScreen = 1f;

    public float offsetPerfect;
    //Check if the note is actually reversed
    public bool reversed;
    void Start()
    {
        audioSource.clip = song;
        songLength = songInfo.song.length;

        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        //60f - 1 minute
        secPerBeat = 60f / songInfo.bpm;

        //record the time when the song starts;
        dspTimeSong = (float)AudioSettings.dspTime;

        //Get the audio to play.
        GetComponent<AudioSource>().Play();

        //Grabs the lines that the notes are going to travel.
        len = trackSpawnPosY.Length;

        queueForTracks = new Queue<NoteNode>[len];
        previousNoteNodes = new NoteNode[len];
        trackNextIndices = new int[len];
        for (int i = 0; i < len; i++)
        {
            trackNextIndices[i] = 0;
            queueForTracks[i] = new Queue<NoteNode>();
            previousNoteNodes[i] = null;
        }
        tracks = songInfo.tracks; //keep a refernce of the tracks
                                  //Check the length of the song.

        InputManager.inputEvent += PlayerInputted;
    }

    void PlayerInputted(int trackNumber)
    {
        Debug.Log(trackNumber);
        if(previousNoteNodes[trackNumber] != null)
        {
            beatOnHitEvent?.Invoke(trackNumber);
        }
        else if(queueForTracks[trackNumber].Count != 0)
        {
            NoteNode frontNode = queueForTracks[trackNumber].Peek();

            float offsetY = Mathf.Abs(frontNode.gameObject.transform.position.x - finishLineX);
            if(offsetY < offsetPerfect)
            {
                Debug.Log("perfect!");
                beatOnHitEvent?.Invoke(trackNumber);
                queueForTracks[trackNumber].Dequeue();
                frontNode.gameObject.SetActive(false);
            }
        }
    }
    void Update()
    {
        //calculate the position in seconds.
        songPos = (float)(AudioSettings.dspTime - dspTimeSong);
        //calculate the position in beats.
        float songPosInBeats = songPos / secPerBeat + BeatsShownOnScreen;

        for (int i = 0; i < len; i++)
        {
            int nextIndex = trackNextIndices[i];
            SongInfo.Track currTrack = tracks[i];
            int randomizer, yRand;
            if (Random.value < 0.5f)
            {
                randomizer = 0;
            }
            else
                randomizer = 1;

            yRand = Random.Range(0, 3);
            if (nextIndex < currTrack.notes.Length && currTrack.notes[nextIndex].note < songPosInBeats)
            {
                SongInfo.Note currNote = currTrack.notes[nextIndex];
                if (randomizer == 1)
                {
                    Debug.Log("Reversed");
                    reversed = true;
                }
                else if (randomizer == 0)
                {
                    reversed = false;
                }
                NoteNode noteNode = NoteObjPool.instance.GetNode(startLineX[randomizer], startLineY[yRand], finishLineX, finishLineY, trackSpawnPosY[i], 0, currNote.note, removeLineX, reversed);
                queueForTracks[i].Enqueue(noteNode);

                trackNextIndices[i]++;
            }
        }
    }
}
