using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ajustes de movimiento")]
    public float speed = 5f;
    public float jumpForce;

    private Rigidbody2D RB2D;
    private Animator animator;

    private float horizontal;
    private bool grounded;

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

        // Raycast para detectar suelo
        Debug.DrawRay(transform.position, Vector3.down * 6f, Color.red);
        Gizmos.color = Color.red;


        if (Physics2D.Raycast(transform.position, Vector2.down, 1.8f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

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

