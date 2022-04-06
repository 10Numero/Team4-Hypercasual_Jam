using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private bool canMove;
    public FloatingJoystick joystick;
    public Transform character;
    public CharacterController cc;
    
    private void Awake()
    {
        GameManager.OnGameStart += OnGameStart;
        GameManager.OnWin += OnGameWinOrLoose;
        GameManager.OnLoose += OnGameWinOrLoose;
        canMove = false;
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        joystick.canvas.gameObject.SetActive(false);
    }

    void OnGameStart()
    {
        canMove = true;
        joystick.canvas.gameObject.SetActive(true);
    }

    void OnGameWinOrLoose()
    {
        canMove = false;
        joystick.canvas.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (!canMove)
            return;
        
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var dir = new Vector3(horizontal + joystick.Direction.x, 0, vertical + joystick.Direction.y);
        dir *= speed * 0.001f;

        if(dir.x != 0|| dir.z != 0)
            character.transform.rotation = quaternion.LookRotation(new float3(dir.x, 0, dir.z), Vector3.up);

        cc.Move(dir);
        //transform.Translate(dir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Objectif"))
        {
            GameManager.OnWin.Invoke();
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }
}
