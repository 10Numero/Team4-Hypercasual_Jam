using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var dir = new Vector3(horizontal, 0, vertical);
        dir *= speed * 0.001f;
        
        transform.Translate(dir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Objectif"))
        {
            GameManager.OnWin.Invoke();
        }
    }
}
