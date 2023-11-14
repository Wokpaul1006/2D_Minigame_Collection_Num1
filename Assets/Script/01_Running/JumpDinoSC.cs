using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpDinoSC : MonoBehaviour
{
    [SerializeField] JumpManager manager;
    private Vector3 originPos;
    public bool allowJump;
    private void Start()
    {
        originPos = transform.position;
        allowJump = false;
        manager = GameObject.Find("RunningManager").GetComponent<JumpManager>();
    }
    private void Update()
    {
        if (allowJump == true || Input.GetKeyDown(KeyCode.Space)) { Jump(); }
    }
    public void EnableJump()
    {
        allowJump = true;
    }
    private void Jump()
    {
        transform.position = new Vector3(originPos.x, Time.deltaTime * 10f, 0);
        allowJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemies")
        {
            manager.ShowPause();
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}