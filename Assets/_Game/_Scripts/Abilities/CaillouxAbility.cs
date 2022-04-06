using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CaillouxAbility : AAbility
{
    public float throwForce = 1;
    public float soundRadius = 5;
    public Rigidbody cailloux;
    public Transform explosionPoint;
    void Start()
    {
        base.Start();
        
        cailloux.transform.SetParent(null);
        cailloux.AddExplosionForce(throwForce, explosionPoint.position, 50);
    }

    [Button]
    void Explosion()
    {
        cailloux.AddExplosionForce(throwForce, explosionPoint.position, 2);   
    }

    public void OnImpact(Vector3 __impactPos)
    {
        var gardiens = GardiensManager.Instance.GetGardienInRadius(__impactPos, soundRadius);

        foreach (var gardien in gardiens)
        {
            gardien.HereSomething(abilityDuration, __impactPos);
        }
    }
}
