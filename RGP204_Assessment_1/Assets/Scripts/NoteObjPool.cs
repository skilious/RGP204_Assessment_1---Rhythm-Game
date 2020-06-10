using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObjPool: MonoBehaviour
{
    public static NoteObjPool instance;

    public GameObject nodePrefab;

    public int initialAmount;

    private List<NoteNode> objList;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        objList = new List<NoteNode>();

        for (int i = 0; i < initialAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(nodePrefab);
            obj.SetActive(false);
            objList.Add(obj.GetComponent<NoteNode>());
        }
    }

    public NoteNode GetNode(float startX, float startY, float endX, float endY, float posY, float posZ, float targetBeat, float removeLineX, bool reversed)
    {
        //check if there is an inactive instance
        foreach (NoteNode node in objList)
        {
            if (!node.gameObject.activeInHierarchy)
            {
                node.Initalise(startX, startY, endX, endY, posY, posZ, targetBeat, removeLineX, reversed);
                node.gameObject.SetActive(true);
                return node;
            }
        }

        //no inactive instances, instantiate a new GetComponent
        NoteNode NoteNode = ((GameObject)Instantiate(nodePrefab)).GetComponent<NoteNode>();
        NoteNode.Initalise(startX, startY, endX, endY, posY, posZ, targetBeat, removeLineX, reversed);
        objList.Add(NoteNode);
        return NoteNode;

    }
}