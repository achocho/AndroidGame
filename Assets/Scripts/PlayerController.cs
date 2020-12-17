using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    float moveSpeed;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask lay;
    float timer = 0;
    public bool GameStarted;
    protected Joystick joystick;
    private float OldPos = 0.0f;
    protected JoyButton joyButton;
    public bool Movement;
    public bool Dead;
   private Vector3 moving;
    public EventSystem events;
    public Text ScoreText;
    public GameObject PlayerDeathEffect;
    public int Score;
    public GameObject Player;
    public GameObject GameOver;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {

        Player.SetActive(true);
        joyButton = FindObjectOfType<JoyButton>();
        joystick = FindObjectOfType<Joystick>();
        Score = PlayerPrefs.GetInt("Score");
        Movement = true;
        GameStarted = true;
      
        Dead = false;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (events.Paused)
        {
            Movement = false;

        }
        else 
        {
            Movement = true;
        }
        ScoreText.text = "Score: "+Score;   
        if (GameStarted) 
        {
            Time.timeScale=1;
        }
        if (isGrounded())
        {
            timer = 0;
        }
        else
        {
            timer += 0.05f;
        }
        if (timer > 10)
        {
            Die();
        }
        if ((joyButton.Pressed && isGrounded()&&Movement)||(Input.GetKeyDown(KeyCode.Space)&&isGrounded()&&Movement))
        {
            animator.SetBool("jumping", true);
            float JumpVelocity = 20f;
            rb.velocity = Vector2.up * JumpVelocity;
        }
        else
        {
            animator.SetBool("jumping", false);
        }
        HandleMovement();
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, lay);
        return hit.collider != null;
    }
    private void HandleMovement()
    {
        moving = rb.velocity;
        float MidAirControl = 3f;
        moveSpeed = 7f;
        if (Movement)
        {
            if (moving.magnitude == 0)
            {
                animator.SetBool("walking", false);
            }
            else
            {
                animator.SetBool("walking", true);
                Debug.Log("Moving");
            }
            if (isGrounded())
            {
               
                rb.velocity = new Vector2(joystick.Horizontal * moveSpeed, rb.velocity.y);
            }
            if (transform.position.x > OldPos)
            {

                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (transform.position.x < OldPos)
            {

                transform.eulerAngles = new Vector3(0, -180, 0);
            }

            OldPos = transform.position.x;
        }
    
        }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        
        {
            Movement = false;
            Die();
            Dead = true;
         
        }
    }
    public void Die() 
    {
        Player.SetActive(false);
        Instantiate(PlayerDeathEffect,gameObject.transform.position,Quaternion.identity);
        Debug.Log("Dead");
        Movement = false;
        FindObjectOfType<AudioManager>().PlaySound("GameOver");
      
     
        GameStarted = false;
    
     
       
 
        GameOver.SetActive(true);
    }
   
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Score",0);
        PlayerPrefs.SetInt("Revived", 0);
    }
}