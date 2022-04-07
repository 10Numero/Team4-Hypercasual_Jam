using System.Collections;
using UnityEngine;

public class LightOffAbility : AAbility
{
    public SceneReferences sceneRef;
    public float radiusLightoff;

    void Start()
    {
        base.Start();

        // sceneRef.Instance.worldLight.enabled = false;
        StartCoroutine(DelayLight());
        sceneRef.Instance.worldLight.intensity = 0.5f;

        //Debug.Log("gardien manager instance ? " + GardiensManager.Instance);
        GardiensManager.Instance.ChangeGardiensRadius(radiusLightoff, abilityDuration);

        IEnumerator DelayLight()
        {
            yield return new WaitForSeconds(abilityDuration);
            sceneRef.Instance.worldLight.intensity = 1;
            DestroyAbility();
        }
        // Async.Delay(abilityDuration, delegate
        // {
        //                 sceneRef.Instance.worldLight.intensity = 1;
        // });
    }
}
