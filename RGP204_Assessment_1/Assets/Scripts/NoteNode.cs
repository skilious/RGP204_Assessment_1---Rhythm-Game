using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NoteNode : MonoBehaviour
{
    [NonSerialized] public float startX, startY, endX, endY;
    [NonSerialized] public float removeLineX;
    [NonSerialized] public float beat;
    [NonSerialized] public bool reversed;
    public void Initalise(float startX, float startY, float endX, float endY, float posY, float posZ, float targetBeat, float removeLineX, bool reversed)
    {
        //set position.
        this.startX = startX;
        this.endX = endX;
        this.endY = endY;
        this.startY = startY;
        this.beat = targetBeat;
        this.removeLineX = removeLineX;
        this.reversed = reversed;
        transform.position = new Vector3(startX, posY, posZ);
    }

    void Update()
    {
        switch(reversed)
        {
            case true:
                {
                    transform.position = new Vector3(startX + (endX - startX) * (1f - (beat - SongManager.songPos / SongManager.secPerBeat) / SongManager.BeatsShownOnScreen), startY + (endY - startY) * (1f - (beat - SongManager.songPos / SongManager.secPerBeat) / SongManager.BeatsShownOnScreen), transform.position.z);
                    break;
                }

            case false:
                {
                    transform.position = new Vector3(startX + (endX - startX) * (1f - (beat - SongManager.songPos / SongManager.secPerBeat) / SongManager.BeatsShownOnScreen), startY + (endY - startY) * (1f - (beat - SongManager.songPos / SongManager.secPerBeat) / SongManager.BeatsShownOnScreen), transform.position.z);
                    break;
                }
        }

        if (transform.position.x < removeLineX && !reversed)
        {
            gameObject.SetActive(false);
        }

        else if (transform.position.x > removeLineX && reversed)
        {
            gameObject.SetActive(false);
        }
    }
}
