using _10KDLL.Threading;

public class LightOffAbility : AAbility
{
    public SceneReferences sceneRef;
    public float radiusLightoff;
    private void Start()
    {
        base.Start();
        
        sceneRef.Instance.worldLight.enabled = false;
        
        GardiensManager.Instance.ChangeGardiensRadius(radiusLightoff, abilityDuration);
        
        Async.Delay(abilityDuration, delegate
        {
            sceneRef.Instance.worldLight.enabled = true;
        });
    }
}
