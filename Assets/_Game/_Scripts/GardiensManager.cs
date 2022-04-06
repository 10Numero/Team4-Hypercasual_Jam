using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _10KDLL.Threading;
using Unity.Collections;
using UnityEngine;

public class GardiensManager : MonoBehaviour
{
    [ReadOnly] public List<GardienController> gardiens = new List<GardienController>();
    public static GardiensManager Instance { get; private set; }

    public void RegisterGardien(GardienController __gardien) => gardiens.Add(__gardien);
    public void UnRegisterGardien(GardienController __gardien) => gardiens.Remove(__gardien);
    private void Awake()
    {
        Instance = this;
    }

    public void ChangeGardiensRadius(float __newRadius, float __duration = 0)
    {
        if (__duration != 0)
        {
            Async.Delay(__duration, delegate
            {
                foreach (var gardien in gardiens)
                {
                    var originalViewRadius = gardien.gardienFov.viewRadius;
                    gardien.gardienFov.viewRadius = originalViewRadius;
                }
            });
        }
        
        foreach (var gardien in gardiens)
        {
            gardien.gardienFov.viewRadius = __newRadius;
        }
    }

    public void ChangeGardienAngle(float __newAngle, float __duration = 0)
    {
        if (__duration != 0)
        {
            Async.Delay(__duration, delegate
            {
                foreach (var gardien in gardiens)
                {
                    var originalViewAngle = gardien.gardienFov.viewAngle;
                    gardien.gardienFov.viewAngle = originalViewAngle;
                }
            });
        }
        
        foreach (var gardien in gardiens)
        {
            gardien.gardienFov.viewAngle = __newAngle;
        }
    }

    public void ChangeGardienSettings(float __newRadius, float __newAngle, float __duration = 0)
    {
        ChangeGardiensRadius(__newRadius, __duration);
        ChangeGardienAngle(__newAngle, __duration);
    }
    
    public List<GardienController> GetGardienInRadius(Vector3 __pos, float __radius)
    {
        List<GardienController> gardiens = new List<GardienController>();
        foreach (var gardien in from gardien in this.gardiens let dst = Vector3.Distance(__pos, gardien.transform.position) where dst < __radius / 2 select gardien)
        {
            gardiens.Add(gardien);
        }

        return gardiens;
    }
}
