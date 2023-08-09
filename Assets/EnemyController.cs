using System.ComponentModel;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float WalkPointRange;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public GameObject Projectile;
    public float sightRangeZ;
    public float attackRangeZ;


    void Awake()
    {
        playerInAttackRange = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(new Vector3(transform.position.x , transform.position.y, transform.position.z + sightRangeZ), sightRange,whatIsPlayer); 
        playerInAttackRange = Physics.CheckSphere(new Vector3(transform.position.x , transform.position.y, transform.position.z + attackRangeZ), attackRange,whatIsPlayer); 

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    void Patroling()
    {
        if(!walkPointSet) SearchWalkPoint(); 

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 1f, whatIsGround)) walkPointSet = true;
    }

    void ChasePlayer()
    {
        agent.SetDestination(Player.position);
        transform.LookAt(Player);
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(Player);

        if(!alreadyAttacked)
        {

            //AttackCode;
            Rigidbody rb = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32, ForceMode.Impulse);
            rb.AddForce(transform.up * 8, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(transform.position.x , transform.position.y, transform.position.z + attackRangeZ), attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x , transform.position.y, transform.position.z + sightRangeZ), sightRange);
    }
}
