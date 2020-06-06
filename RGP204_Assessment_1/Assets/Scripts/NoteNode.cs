using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NoteNode : MonoBehaviour
{
    [NonSerialized] public float startX, endX;
    [NonSerialized] public float removeLineX;
    [NonSerialized] public float beat;
    [NonSerialized] public int times;

    public void Initalise(float startX, float endX, float posY, float posZ, float targetBeat, int times, float removeLineX)
    {
        //set position.
        this.startX = startX;
        this.endX = endX;
        this.beat = targetBeat;
        this.times = times;
        this.removeLineX = removeLineX;

        transform.position = new Vector3(startX, posY, posZ);
    }

    void Update()
    {
        transform.position = new Vector3(startX + (endX - startX) * (1f - (beat - SongManager.songPos / SongManager.secPerBeat) / SongManager.BeatsShownOnScreen), transform.position.y, transform.position.z);

        if(transform.position.x < removeLineX)
        {
            gameObject.SetActive(false);
        }
    }
}
