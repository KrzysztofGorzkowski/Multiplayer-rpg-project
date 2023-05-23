using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class EnemyBehaviour : NetworkBehaviour
{
    public delegate void EnemyAttacked();
    public static event EnemyAttacked Attacked;

    private GameObject _player;
    private Vector3 _direction;        //direction of movement
    private bool _canTrackThePlayer;   //tells if the enemy can follow the player
    public Animator animator;

    public EnemyStats _enemyStats;

    public float _attackDelay = 0.0F;

    float totalAngle = 360;
    private static int NUMBER_OF_RAYS = 50;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

    // Start is called before the first frame update
    void Start()
    {
        //_player = GameManager.GetPlayer().getPlayerObject();    //getting player object               //wa¿ne
        //animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) < _enemyStats.viewDistance)
        {
            bool seePlayer = false;
            float angle = totalAngle / (float)NUMBER_OF_RAYS;
            RaycastHit2D[] hits = new RaycastHit2D[NUMBER_OF_RAYS];
            for (int i = 0; i < NUMBER_OF_RAYS; i++)
            {
                Vector2 direction = Quaternion.Euler(0, 0, i * angle) * transform.right;
                hits[i] = Physics2D.Raycast(transform.position, direction, _enemyStats.viewDistance, LayerMask.GetMask("Action"));
                Debug.DrawRay(transform.position, direction * Mathf.Sqrt(Mathf.Pow(transform.position.x-hits[i].point.x, 2) + Mathf.Pow(transform.position.y - hits[i].point.y, 2)), Color.green);
            }
            for(int i = 0; i< NUMBER_OF_RAYS; i++)
            {
                if (hits[i].collider != null)
                {
                    Debug.Log("Enemy looking for a player");
                    if (hits[i].collider.transform.CompareTag("Player") || hits[i].collider.transform.CompareTag("PlayerWand"))
                    {
                        Debug.Log("Enemy see Player");
                        seePlayer = true;
                    }
                }
            }
            if (seePlayer)
            {
                _canTrackThePlayer = true;
                animator.SetBool("SeePlayer", true);
            }
            else
            {
                _canTrackThePlayer = false;
                animator.SetBool("SeePlayer", false);
            }
        }
    }

    void FixedUpdate()
    {
        _direction = _player.transform.position - transform.position;
        _direction.Normalize();
        if (_canTrackThePlayer)
        {
            transform.GetComponent<Rigidbody2D>().MovePosition(transform.position + _direction * _enemyStats.movementSpeed * Time.fixedDeltaTime);     //change of position
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.collider.CompareTag("Player") || collision.collider.CompareTag("PlayerWand")) && Time.time > _attackDelay)
        {
            _attackDelay = Time.time + _enemyStats.speedAttack;
            Attack();
        }
    }
    private void Attack()
    {
        animator.SetTrigger("Attack");
        _player.GetComponent<PlayerStatsManager>().TakeDamage(_enemyStats.damage);    //taking damage from an enemy

        if (Attacked != null)
        {
            //Debug.Log("Attacked Invoked");
            Attacked();
        }
    }


}


