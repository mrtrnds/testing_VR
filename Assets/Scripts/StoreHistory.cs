using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreHistory : MonoBehaviour
{
    private static UndoRedo<ObjectState> myList;

    // Start is called before the first frame update
    void Awake()
    {
        UndoRedo<ObjectState> createdList = new UndoRedo<ObjectState>();
        myList = createdList;
    }

    public UndoRedo<ObjectState> Get()
    {
        return myList;
    }
}
