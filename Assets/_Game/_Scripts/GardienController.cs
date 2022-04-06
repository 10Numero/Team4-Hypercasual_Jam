using System;
using System.Collections;
using System.Collections.Generic;
using _10KDLL.Threading;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
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

    public CanvasGroup feedbackCanvas;
    
    public FieldOfView gardienFov;
    
    private int patternIndex;

    private bool isStopped;

    private TweenerCore<Vector3, Vector3, VectorOptions> _tweenMove;
    private TweenerCore<Quaternion, Vector3, QuaternionOptions> _tweenRot;

    private void Awake()
    {
        patternIndex = 0;

        feedbackCanvas.alpha = 0;
    }

    public void StopGardien()
    {
        isStopped = true;
        _tweenMove.Kill();
        _tweenRot.Kill();
    }

    public void PauseGardien()
    {
        isStopped = true;
        _tweenMove.Pause();
        _tweenRot.Pause();
    }

    public void ResumeGardien()
    {
        if (_tweenMove != null)
            _tweenMove.Play();
        else
        {
            isStopped = false;
            NextPoint();
        }

        if (_tweenRot != null)
            _tweenRot.Play();
        else
        {
            isStopped = false;
            NextPoint();
        }
    }

    private void Reset()
    {
        gardienFov = GetComponent<FieldOfView>();
    }

    private void Start()
    {
        GardiensManager.Instance.RegisterGardien(this);
        NextPoint();
    }

    void NextPoint()
    {
        if (isStopped)
            return;
        
        currentPath = paths[patternIndex];
        
        _tweenMove = transform.DOMove(currentPath.endPath.position, currentPath.movementDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => Async.Delay(currentPath.timeBeforeNextPath, NextPoint));
        
        _tweenRot = transform.DORotate(currentPath.endPath.rotation.eulerAngles, currentPath.rotationDuration);
        
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

    public void HereSomething(float __duration, Vector3 __soundPosition)
    {
        feedbackCanvas.alpha = 1;
        PauseGardien();
        
        transform.LookAt(__soundPosition);
        
        Async.Delay(__duration, delegate
        {
            feedbackCanvas.alpha = 0;
            isStopped = false;
            ResumeGardien();
        });
    }
}
