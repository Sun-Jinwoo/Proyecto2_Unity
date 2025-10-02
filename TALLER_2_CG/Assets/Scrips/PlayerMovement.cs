using JetBrains.Annotations;
using UnityEngine;

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
        if (horizontal < 0.0f) transform.localScale = new Vector3(-1, 1, 1);
        else if (horizontal > 0.0f) transform.localScale = new Vector3(1, 1, 1);

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
    }
    private void stopAttack()
    {
        attacking = false;
    }

    public void TakeDamage()
    {
        Debug.Log("Player Takes Damage!");
        if (!takeD)
        {
            Debug.Log("Player Takes Damage 2!");
            takeD = true;
            Vector2 push = new Vector2(transform.position.x - dirGolpe, 5f).normalized;
            RB2D.AddForce(push * 5f, ForceMode2D.Impulse);
        }
    }

    public void StopTakeDamage()
    {
        takeD = false;
        RB2D.linearVelocity = Vector2.zero;
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
}

