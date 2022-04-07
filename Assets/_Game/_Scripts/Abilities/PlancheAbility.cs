using System;
using System.Collections;
using System.Collections.Generic;
// using _10KDLL.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlancheAbility : AAbility
{
    public SceneReferences sceneRef;

    private void Update()
    {
        transform.LookAt(GetClosestAgentPosition());
    }

    Transform GetClosestAgentPosition()
    {
        Transform closestGardien = null;
        var closestDist = Mathf.Infinity;

        foreach (var gardien in GardiensManager.Instance.gardiens)
        {
            var dst = Vector3.Distance(sceneRef.Instance.player.transform.position, gardien.transform.position);

            if (dst < closestDist)
            {
                closestDist = dst;
                closestGardien = gardien.transform;
            }
        }

        return closestGardien;
    }

    // void EndAbility()
    // {
    //     Destroy(gameObject);
    //     Destroy(abilityUi.gameObject);
    // }
}