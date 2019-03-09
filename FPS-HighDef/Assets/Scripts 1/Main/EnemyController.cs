using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public int health;
    public NavMeshAgent agent;
    public Transform player;
    public GameController gameController;
    public float attackDistance;
    public float startAttackTime;
    public float attackTime;    
    public bool isAttacking;
    public int attackAmount;
    public float agentSpeed;
    public Animator animator;
    public bool isWalking;
    public float distanceToWalk;
    public GameObject bloodSplash;
    public float deathTime;
    public float bloodTime;
    public bool dead;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("Attack", attackTime, attackTime);
        
    }
    public void Attack()
    {
        if (dead != true)
        {
            if (gameController.dead != true)
            {
                if (isAttacking)
                {
                    StartCoroutine(BloodShow());
                    gameController.Hurt(attackAmount);

                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < distanceToWalk )
        {
            if (gameController.dead != true)
            {
                if (isAttacking != true)
                {
                    agent.isStopped = false; 
                    animator.SetBool("Walking", true);

                }
                else
                {
                    agent.isStopped = true;

                    animator.SetBool("Walking", false);
                }
            }
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("Walking", false);
        }
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            if(IsInvoking("Attack") == false)
                InvokeRepeating("Attack", startAttackTime, attackTime);
            isAttacking = true;
            agent.isStopped = true;
        //transform. = transform.LookAt(player.position);
            animator.SetBool("Attacking", true);
        }
        else
        {
            CancelInvoke("Attack");
            isAttacking = false;
            agent.isStopped = false;
            animator.SetBool("Attacking", false);
        }

        if (health <= 0 && dead != true)
            Die();

        agent.destination = player.position;

    }
    void Die()
    {
        dead = true;
        agent.isStopped = true;
        agent.enabled = false;
        rb.isKinematic = true;
 //       rb.constraints = RigidbodyConstraints.FreezePosition;
        gameController.kills += 1;
        gameController.UpdateUI();
        animator.SetTrigger("Die");
        Destroy(this.gameObject, deathTime);

    }

    public void BeenHit(int damage)
    {   
        health -= damage;
    }
 /*   private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        /*
        int damage;
        if (other.gameObject.CompareTag("Bullet"))
        {
            damage = other.gameObject.GetComponent<BulletController>().damage;
            BeenHit(damage);
        }
        */
        

    }



    public IEnumerator BloodShow()
    {
        bloodSplash.SetActive(true);
        yield return new WaitForSeconds(bloodTime);
        bloodSplash.SetActive(false);

    }
}
