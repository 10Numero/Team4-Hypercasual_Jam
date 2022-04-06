using UnityEngine;

public class Cailloux : MonoBehaviour
{
    public CaillouxAbility ability;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ability.OnImpact(transform.position);
            ability.DestroyAbility();
            Destroy(transform.gameObject, 5);
        }
    }
}
