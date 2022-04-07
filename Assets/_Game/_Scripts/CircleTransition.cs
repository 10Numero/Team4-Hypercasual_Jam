using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CircleTransition : MonoBehaviour
{
    [SerializeField] private SceneReferences sceneRef;
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerController player;
    [SerializeField] private RectTransform mask;
    [SerializeField] private float animationDuration;
    
    private void Awake()
    {
        GameManager.OnWin += OnLooseOrWin;
        GameManager.OnLoose += OnLooseOrWin;
    }

    void Reset()
    {
        player = FindObjectOfType<PlayerController>();
        cam = FindObjectOfType<Camera>();
    }

    void OnLooseOrWin()
    {
        Debug.Log("log");
        mask.transform.position = cam.WorldToScreenPoint(player.transform.position);

        StartCoroutine(MaskAnimation());
        IEnumerator MaskAnimation()
        {
            var time = 0.0f;
            while (time < animationDuration)
            {
                time += Time.deltaTime;
                mask.transform.localScale = Vector3.Lerp(Vector3.one * 30, Vector3.one * 2, time / animationDuration);
                yield return null;
            }
        }
    }

    private void OnDisable()
    {
        GameManager.OnWin -= OnLooseOrWin;
        GameManager.OnLoose -= OnLooseOrWin;
    }
}
