using JetBrains.Annotations;
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ajustes de movimiento")]
    public float speed = 5f;
    public float jumpForce;

    private Rigidbody2D RB2D;
    private Animator animator;

    private float horizontal;
    public LayerMask groundLayer;
    private bool grounded;
    private bool attacking;

    private bool takeD = false;
    public float dirGolpe;
    public int health = 7;
    public int damage = 1;

    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Captura de inputs
        horizontal = Input.GetAxis("Horizontal");

        // Dirección del sprite
        if (horizontal < 0.0f) transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        // Animaciones
        animator.SetBool("Running", horizontal != 0.0f);
        animator.SetBool("Jumping", !grounded);
        animator.SetBool("Attacking", attacking);

        // Raycast para detectar suelo
        Debug.DrawRay(transform.position, Vector3.down * 1.8f, Color.red);
        Gizmos.color = Color.red;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.8f, groundLayer);
        grounded = hit.collider != null;

        //if (Physics2D.Raycast(transform.position, Vector2.down, 1.8f, groundLayer))
        //{ grounded = true;}
        //else  
        //{ grounded = false;}

        // Salto
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            Jump();
        }

        // Restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        // Ataque
        if (Input.GetKeyDown(KeyCode.Space) && !attacking && grounded)
        {
            attack();
        }

        //recibir daño
        




    }

    private void attack()
    {
        Debug.Log("Player Attacks!");
        attacking = true;
        
        Invoke("stopAttack", 0.7f);
    }
    private void stopAttack()
    {
        attacking = false;
    }
    public void StopTakeDamage()
    {
        takeD = false;
        RB2D.linearVelocity = Vector2.zero;
    }

    public void TakeDamage(int damage, float golpeX)
    {
        if (!takeD)
        {
            takeD = true;
            health -= 1;
            Debug.Log("player health: " + health);
            Vector2 push = new Vector2(transform.position.x - golpeX, 70f).normalized;
            RB2D.AddForce(push * 5f, ForceMode2D.Impulse);

            //animator.SetTrigger("Hurt");
            
            if (health <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(DesactivateDamage());
            }
        }
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        RB2D.linearVelocity = Vector2.zero;
        this.enabled = false; // evita más inputs
        StartCoroutine(LoadDeathScene());
    }


    void FixedUpdate()
    {
        // Movimiento horizontal usando físicas
        RB2D.linearVelocity = new Vector2(horizontal * speed, RB2D.linearVelocity.y);        
    }

    private void Jump()
    {
        RB2D.AddForce(Vector2.up*jumpForce);
    }
    IEnumerator DesactivateDamage()
    {
        yield return new WaitForSeconds(0.8f);
        takeD = false;
    }
    private IEnumerator LoadDeathScene()
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("ScoreScene");
    }
}

