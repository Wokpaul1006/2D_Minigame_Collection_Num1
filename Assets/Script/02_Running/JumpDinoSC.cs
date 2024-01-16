using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpDinoSC : MonoBehaviour
{
    [SerializeField] AudioSource jumpSFX;
    [SerializeField] JumpManager manager;
    [HideInInspector] Rigidbody2D rb;
    [HideInInspector] SpriteRenderer apparance;
    [SerializeField] Animator dinoAnim;
    private Vector3 originPos;
    public bool allowJump;
    public bool isGrounded;
    private float jumpSpd;
    private void Start()
    {
        originPos = transform.position;
        jumpSpd = 6f;
        allowJump = false;

        //manager = GameObject.Find("RunningManager").GetComponent<JumpManager>();

        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (allowJump == true || Input.GetKeyDown(KeyCode.Space)) { Jump(); }
    }
    private void Jump()
    {
        rb.AddForce(transform.up * jumpSpd, ForceMode2D.Impulse);
        allowJump = false;
        jumpSFX.Play();
        dinoAnim.SetBool("isJump", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            manager.UpdateGameState(2);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            dinoAnim.SetBool("isJump", false);
        }
    }
}
