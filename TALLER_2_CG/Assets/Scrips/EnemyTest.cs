using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyTest : MonoBehaviour
{
    public GameObject player;
    private Vector2 movement;

    [Header("Ajustes de enemigo")]
    public float speed = 2f;             // Velocidad de patrulla
    public float detectionRange = 3f;    // Rango para detectar al jugador
    public int damage = 1;               // Daño que inflige al jugador
    public int push = 10;               // Fuerza de empuje al recibir daño
    public int puntosOtorgados = 100;

    [Header("Referencias")]
    public Rigidbody2D RB2D;
    private Animator animator;

    //private bool isMovingRight = true;
    private bool detectingPlayer = false;
    private bool isDead = false;
    private bool takeD = false;

    private int health = 5;
    private int enemyhealth = 3;

    private void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isDead) return;

        Vector3 direction = player.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(10f, 10f, 10f);
        else transform.localScale = new Vector3(-10f, 10f, 10f);

        Gizmos.color = Color.green;
        Debug.DrawRay(transform.position, Vector3.right * 1.5f);
        Debug.DrawRay(transform.position, Vector3.left * 1.5f);

        float distance = Vector2.Distance(transform.position, player.transform.position);
        detectingPlayer = distance <= detectionRange;
        animator.SetBool("Run", RB2D.linearVelocity.x != 0);

        if (detectingPlayer)
        {
            AttackToPlayer();
        }

        float attackRange = 1.5f;
        if (distance < attackRange)
        {
            AttackToEnemy();
        }

        distance = Mathf.Abs(player.transform.position.x - transform.position.x);


    }
  
    private void AttackToPlayer()
    {
        PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
        if (playerScript != null)
        {
            playerScript.TakeDamage(damage, transform.position.x);
        }
    }
    
    private void AttackToEnemy()
    {
        Debug.Log("Enemy Attacks!");
        PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
        if (playerScript != null)
        {
            EnemyDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Inflige daño directo al jugador al tocarlo
            PlayerMovement playerScript = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerScript != null)
            {
                playerScript.dirGolpe = transform.position.x;
                EnemyDamage();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Golpe"))
        {
            Debug.Log("Enemy hit!" + enemyhealth);
            Vector2 dirDan = new Vector2(collision.gameObject.transform.position.x, 0);
            //TakeDamage(dirDan, 1);
            enemyhealth -= 1;
            StartCoroutine(DesactivateDamage());

            if (enemyhealth <= 0)
            {
                Die();
            }
        }
    }

    
    public void EnemyDamage()
    {
        Debug.Log("Player Takes Damage!");
        if (!takeD)
        {
            Debug.Log("Player Takes Damage 2!");
            takeD = true;
            Vector2 push = new Vector2(transform.position.x - detectionRange, 10f).normalized;
            RB2D.AddForce(push * 50f, ForceMode2D.Impulse);
            enemyhealth -= 1;
            StartCoroutine(DesactivateDamage());

            if (isDead) return;

            enemyhealth -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

    }
    private void Die()
    {
        isDead = true;
        RB2D.linearVelocity = Vector2.zero; 
        animator.SetTrigger("Death");
        //aGameManager.Instance.AddScore(puntosOtorgados);
        Destroy(gameObject, 1f);            // Se destruye después de 1s (da tiempo a animación)
    }    
    IEnumerator DesactivateDamage()
    {
        yield return new WaitForSeconds(0.8f);
        takeD = false;
    }

}


/*
  void FixedUpdate()
  {
      Patrol();
  }


private void Patrol()
  {
      float moveDir = isMovingRight ? 1 : -1;

      // Aplicamos velocidad en el Rigidbody
      RB2D.linearVelocity = new Vector2(moveDir * speed, RB2D.linearVelocity.y);

      // --- Detectar muro en la dirección de movimiento ---
      RaycastHit2D wallInfo = Physics2D.Raycast(
          transform.position,             // Origen
          Vector2.right * moveDir,        // Dirección depende del lado
          1f,                           // Distancia
          LayerMask.GetMask("Ground")     // Solo chequea colisión con suelo/muro
      );

      Debug.DrawRay(transform.position, Vector2.right * moveDir * 0.5f, Color.blue);

      // Si choca contra un muro que NO sea el jugador → voltear
      if (wallInfo.collider != null && !wallInfo.collider.CompareTag("Player"))
      {
          Flip();
      }

      // --- Detectar borde para no caerse ---
      Vector2 groundCheckPos = new Vector2(transform.position.x + moveDir * 0.5f, transform.position.y);
      RaycastHit2D groundInfo = Physics2D.Raycast(
          groundCheckPos, Vector2.down, 2.3f, LayerMask.GetMask("Ground")
      );

      Debug.DrawRay(groundCheckPos, Vector2.down * 2.3f, Color.red);

      // Si no hay suelo debajo → voltear
      //if (groundInfo.collider == null)
      //{
      //    Flip();
      //}
  }

  private void Flip()
  {
      isMovingRight = !isMovingRight;
      Vector3 scale = transform.localScale;
      scale.x *= -1;
      transform.localScale = scale;
  }
  */


// animar el golpe del enemigo
// corregir el raycast que pega con las paredes pero no con paredes bajas
// corregir el impulso al recibir daño
// darle puntos de vida funcionales




//[SerializeField] private Animator[] EnemyAnims;

//public void Animation_1_Idle()
//{
//    for (int i = 0; i < EnemyAnims.Length; i++)
//    {
//        if (EnemyAnims[i].gameObject.activeSelf == true)
//        {
//            EnemyAnims[i].SetBool("Run", false);
//            Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is Idling");
//        }
//    }
//}
//public void Animation_2_Run()
//{
//    for (int i = 0; i < EnemyAnims.Length; i++)
//    {
//        if (EnemyAnims[i].gameObject.activeSelf == true)
//        {
//            EnemyAnims[i].SetBool("Run", true);
//            Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is Running");

//        }
//    }
//}
//public void Animation_3_Hit()
//{
//    for (int i = 0; i < EnemyAnims.Length; i++)
//    {
//        if (EnemyAnims[i].gameObject.activeSelf == true)
//        {
//            EnemyAnims[i].SetBool("Run", false);
//            EnemyAnims[i].SetTrigger("Hit");
//            Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is being Hit");
//        }
//    }
//}
//public void Animation_4_Death()
//{
//    for (int i = 0; i < EnemyAnims.Length; i++)
//    {
//        if (EnemyAnims[i].gameObject.activeSelf == true)
//        {
//            EnemyAnims[i].SetBool("Run", false);
//            EnemyAnims[i].SetTrigger("Death");
//            Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " has died");
//        }
//    }
//}
//public void Animation_5_Ability()
//{
//    for (int i = 0; i < EnemyAnims.Length; i++)
//    {
//        if (EnemyAnims[i].gameObject.activeSelf == true)
//        {
//            EnemyAnims[i].SetBool("Run", false);
//            EnemyAnims[i].SetBool("Ability", true);
//            Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is using its First Ability");
//        }
//    }
//}
//public void Animation_5_Ability2()
//{
//    for (int i = 0; i < EnemyAnims.Length; i++)
//    {
//        if (EnemyAnims[i].gameObject.activeSelf == true)
//        {
//            EnemyAnims[i].SetBool("Run", false);
//            EnemyAnims[i].SetBool("Ability 2", true);
//            Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is using its Second Ability");
//        }
//    }
//}
//public void Animation_5_Ability3()
//{
//    for (int i = 0; i < EnemyAnims.Length; i++)
//    {
//        if (EnemyAnims[i].gameObject.activeSelf == true)
//        {
//            EnemyAnims[i].SetBool("Run", false);
//            EnemyAnims[i].SetBool("Ability 3", true);
//            Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is using its Third Ability");
//        }
//    }
//}
//public void Animation_6_Attack()
//{
//    for (int i = 0; i < EnemyAnims.Length; i++)
//    {
//        if (EnemyAnims[i].gameObject.activeSelf == true)
//        {
//            EnemyAnims[i].SetBool("Run", false);
//            EnemyAnims[i].SetTrigger("Attack");
//            Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is using using its Primary Attack");
//        }
//    }
//}

//public void Animation_7_Attack2()
//{
//    for (int i = 0; i < EnemyAnims.Length; i++)
//    {
//        if (EnemyAnims[i].gameObject.activeSelf == true)
//        {
//            EnemyAnims[i].SetBool("Run", false);
//            EnemyAnims[i].SetTrigger("Attack 2");
//            Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is using using its Secondary Attack");
//        }
//    }
//}
//public void Animation_8_Attack3()
//{
//    for (int i = 0; i < EnemyAnims.Length; i++)
//    {
//        if (EnemyAnims[i].gameObject.activeSelf == true)
//        {
//            EnemyAnims[i].SetBool("Run", false);
//            EnemyAnims[i].SetTrigger("Attack 3");
//            Debug.Log("The enemy " + EnemyAnims[i].gameObject.name + " is using using its Tertiary Attack");
//        }
//    }
//}

