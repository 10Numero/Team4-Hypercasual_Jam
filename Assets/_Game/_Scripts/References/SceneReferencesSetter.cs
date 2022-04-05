using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReferencesSetter : MonoBehaviour
{
    [SerializeField] private SceneReferences _sceneReferences;
    [SerializeField] private SceneComponentsReferences _sceneCompRefs;

    void Awake()
    {
        (_sceneReferences as IReferenceSetter<SceneComponentsReferences>).SetInstance(_sceneCompRefs);
    }
}
