
using System.IO;
using System;
using UnityEngine;
using static UnityEditor.Progress;

public static class ObjectCreater
{
    private static Transform parent;
    private static Transform Parent
    {
        get
        {
            if (parent == null)
            {
                parent = new GameObject("sceneObjects").transform;
            }
            return parent;
        }
    }

    public static GameObject CreateEffect(string path, Vector3 position, int id = -1, Transform parent = default)
    {
        return CreateEffect(path, position, Vector3.zero, Vector3.one, id, parent);
    }

    public static GameObject CreateEffect(string path, Vector3 position, Vector3 rotation, Vector3 scale, int id = -1, Transform parent = default)
    {
        return CreateObject(path, position, rotation, scale, parent);
    }

    public static GameObject CreateObject(string path, Transform parent, Vector3 position)
    {
        return CreateObject(path, position, Vector3.zero, Vector3.one, parent: parent);
    }

    public static GameObject CreateObject(string path, Vector3 position)
    {
        return CreateObject(path, position, Vector3.zero, Vector3.one);
    }

    public static GameObject CreateObject(string path, Vector3 position, Vector3 rotation, Vector3 scale, Transform parent = default)
    {
        var prefab = Resources.Load<GameObject>(path);
        if (prefab == null)
        {
            return null;
        }
        var go = GameObject.Instantiate(prefab);
        if (parent == null)
        {
            go.transform.position = position;
            //go.transform.SetParent(Parent);
        }
        else
        {
            go.transform.SetParent(parent.transform, false);
            go.transform.localPosition = position;
        }
        go.transform.rotation = Quaternion.Euler(rotation);
        go.transform.localScale = scale;
        return go;
    }
}
