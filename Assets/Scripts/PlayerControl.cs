using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class PlayerControl : MonoBehaviour
{
    private Animator animator;
    private float _horizontal;
    [SerializeField] private Rigidbody2D myRBD;

    [SerializeField] private float velocityModifier;

    [SerializeField] private float jumpForce;

    [SerializeField] private int maxJumps = 2;
    [SerializeField] private int gravity;
    [SerializeField] private int jumpCount = 0;

    int life = 10;

    public static event Action<int> OnCollisionItem;

    public static event Action OnCollisionActiveteSequence;
    public static event Action OnCollisionActivateFall;
    public static event Action OnCollisionActivateSide;
    void Start()
    {
    if (myRBD == null)
        myRBD = GetComponent<Rigidbody2D>();

    if (animator == null)
        animator = GetComponent<Animator>();
    }

    public void OnMovement(InputAction.CallbackContext move)
    {
        _horizontal = move.ReadValue<Vector2>().x;
    }
    public void OnJump(InputAction.CallbackContext jump)
    {
        
        
        if (jump.performed && jumpCount < maxJumps)
        {
            myRBD.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("jump_trigger");
            jumpCount += 1;
            //myRBD.linearVelocityY
        }
        if (jump.canceled)
        {
            isInTheAir = true;
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {

            jumpCount = 0;
            isInTheAir = false;
            animator.SetTrigger("land_trigger");
        }

        if (collision.gameObject.tag == "Coin")
        {
            OnCollisionItem?.Invoke(5);
            Destroy(collision.gameObject);
            Debug.Log("Coin");
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ActiveSide")
        { 
            collision.gameObject.SetActive(false);
            Debug.Log("activate side");
            OnCollisionActivateSide?.Invoke();
        }
        if (collision.gameObject.tag == "ActiveFall")
        {
            collision.gameObject.SetActive(false);
            Debug.Log("activate fall");
            OnCollisionActivateFall?.Invoke();
        }
        if (collision.gameObject.tag == "ActiveSequence")
        {
            collision.gameObject.SetActive(false);
            Debug.Log("activate sequence");
            OnCollisionActiveteSequence?.Invoke();
        }

    }
    void Update()
    {
        // Debug.Log(jumpCount);
        animator.SetBool("running", _horizontal != 0.0f);

        if (_horizontal < 0.0f)
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    else if (_horizontal > 0.0f)
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    [SerializeField] bool isInTheAir;
    public void FixedUpdate()
    {
        myRBD.linearVelocity = new Vector2(_horizontal * velocityModifier, myRBD.linearVelocity.y);

        if(isInTheAir == true)
        {
            myRBD.gravityScale = gravity;
        }
        else
        {
            myRBD.gravityScale = 1f;
        }
    }
}
