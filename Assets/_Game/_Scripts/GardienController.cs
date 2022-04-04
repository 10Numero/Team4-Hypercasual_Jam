using System;
using System.Collections;
using System.Collections.Generic;
using _10KDLL.Threading;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class GardienController : MonoBehaviour
{
    [System.Serializable]
    public class Path
    {
        public string pathName;
        public Transform endPath;
        public float movementDuration;
        public float rotationDuration;
        public float timeBeforeNextPath;
    }

    [SerializeField] Path[] paths;
    [ReadOnly, SerializeField] Path currentPath;
    
    private int patternIndex;

    private void Awake()
    {
        patternIndex = 0;
    }

    private void Start()
    {
        NextPoint();
    }

    void NextPoint()
    {
        currentPath = paths[patternIndex];
        
        transform.DOMove(currentPath.endPath.position, currentPath.movementDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => Async.Delay(currentPath.timeBeforeNextPath, NextPoint));
        
        transform.DORotate(currentPath.endPath.rotation.eulerAngles, currentPath.rotationDuration);
        
        patternIndex = patternIndex == paths.Length - 1 ? 0 : patternIndex + 1;
    }

    [Button]
    void SetupPathDirection()
    {
        for (var i = 0; i < paths.Length; i++)
        {
            paths[i == paths.Length - 1 ? 0 : i + 1].endPath.eulerAngles = 
                Quaternion.LookRotation(paths[i].endPath.position - paths[i == paths.Length - 1 ? 0 : i + 1].endPath.position).eulerAngles
                                           + new Vector3(0, 180, 0);
        }
    }

    [Button]
    void SetupGardien()
    {
        transform.position = paths[paths.Length - 1].endPath.position;
        transform.rotation = paths[1].endPath.rotation;
    }
}
