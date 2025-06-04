using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    [HideInInspector] public Transform[] points;

    void Awake()
    {
        CacheChildPoints();
    }

#if UNITY_EDITOR
    // Esto permite que se actualice en el editor si cambias los hijos
    void OnValidate()
    {
        CacheChildPoints();
    }
#endif

    private void CacheChildPoints()
    {
        int childCount = transform.childCount;
        points = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
