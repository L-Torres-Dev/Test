using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Static class with useful functions for quicker implementation. (May also contain functions for debugging purposes
 */
public static class Utility
{

    public static GameObject FindObject(this GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public static GameObject FindObject(this GameObject parent, int index)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);

        return trs[index].gameObject;
    }

    public static void PrintNames(this GameObject parent)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            UnityEngine.Debug.Log(t.name + "\n");
        }
    }

    public static void Log(string message)
    {
        UnityEngine.Debug.Log(message);
    }
}
