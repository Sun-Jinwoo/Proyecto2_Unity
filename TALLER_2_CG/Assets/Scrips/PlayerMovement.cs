using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce;

    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    private float Horizontal;
    private float Vertical;
    private bool grounded;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        Vertical = Input.GetAxis("Vertical");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1, 1, 1);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1, 1, 1);

        animator.SetBool("Running", Horizontal != 0.0f);
        animator.SetBool("Jumping", !grounded);

        // Movimiento
        Vector3 movement = new Vector3(Horizontal, 0.0f, Vertical);
        //transform.Translate(movement * speed * Time.deltaTime, Space.World);
        transform.position += movement * speed * Time.deltaTime;

        // Dibuja el raycast en la vista Scene
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.5f, Color.red);

        // Comprobar suelo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);
        grounded = hit.collider != null;


        if (Input.GetKeyDown(KeyCode.W) && grounded == true)
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    private void jump()
    {
        Rigidbody2D.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);      
    }
}
