using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoRedo<T>
{
    private static List<ObjectState> undoList = new List<ObjectState>();
    private static List<ObjectState> redoList = new List<ObjectState>();

    public int GetUndoListCount()
    {
        return undoList.Count;
    }

    public int GetRedoListCount()
    {
        return redoList.Count;
    }

    public void PushToUndoList(ObjectState item)
    {
        if (undoList.Count > 19)
        {
            undoList.RemoveAt(undoList.Count - 20);
            undoList.Add(item);
        }
        else
            undoList.Add(item);
    }

    public void PushToRedoList(ObjectState item)
    {
        if (redoList.Count > 19)
        {
            redoList.RemoveAt(redoList.Count - 20);
            redoList.Add(item);
        }
        else
            redoList.Add(item);
    }


    public ObjectState PopFromUndoList()
    {
        if (undoList.Count > 1)
        {
            PushToRedoList(undoList[undoList.Count-1]);
            PushToRedoList(undoList[undoList.Count - 2]);
            ObjectState temp = undoList[undoList.Count - 2];
            undoList.RemoveAt(undoList.Count - 1);
            undoList.RemoveAt(undoList.Count - 1);
            return temp;
        }
        else
            return default(ObjectState);
    }

    public ObjectState PopFromRedoList()
    {
        if (redoList.Count > 1)
        {
            PushToUndoList(redoList[redoList.Count-1]);
            PushToUndoList(redoList[redoList.Count - 2]);
            ObjectState temp = redoList[redoList.Count - 2];
            redoList.RemoveAt(redoList.Count - 1);
            redoList.RemoveAt(redoList.Count - 1);
            return temp;
        }
        else
            return default(ObjectState);
    }
}
