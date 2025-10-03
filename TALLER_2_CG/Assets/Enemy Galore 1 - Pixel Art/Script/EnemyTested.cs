using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyTested : MonoBehaviour
{
    public GameObject player;
    float distance;

    private void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(10f, 10f, 10f);
        else transform.localScale = new Vector3(-10f, 10f, 10f);

        Gizmos.color = Color.green;
        Debug.DrawRay(transform.position, Vector3.right * 1.5f);
        Debug.DrawRay(transform.position, Vector3.left * 1.5f);

        distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < 10f)
        {
            //Animation_2_Run();
        }
        else
        {
            //Animation_1_Idle();
        }

        float step = 3f * Time.deltaTime * transform.position.x;
        if (distance < step)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }

        float attackRange = 1.5f;
        if (distance < attackRange)
        {
            //Animation_6_Attack();
            Attack();

        }



        distance = Mathf.Abs(player.transform.position.x - transform.position.x);
    }

    private void Attack()
    {
        Debug.Log("Enemy Attacks!");


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player!");
            //Vector2 
        }
    }
}



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

