using System;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    public Transform Target;
    public UnitState State;
    public EnemySet Creep;

    public bool IsAlive;
    public bool IsFoward;
    public float HealthPoint;
    public float MoveSpeed;
    public float AtkSpeed;

    private Animator enemyAnim;
    private AudioSource enemyAudio;

    private float attackTime;

    private void Enemy_Attack()
    {
        var player = Target.GetComponent<PlayerActive>();
        var gm = GameManager.Instance;
        if (attackTime == 0 && player.IsAlive)
        {
            enemyAnim.SetTrigger("Attacking");
            if (Creep == EnemySet.Witch)
            {
                var location = RandomPosition(transform.position);
                var vfx = Instantiate(gm.Origin_CastLight, location, Quaternion.identity);
                Instantiate(gm.Origin_SkeltCreep, location, Quaternion.identity);
                Destroy(vfx, 1f);
            }
            else
            {
                enemyAudio.Play();
            }
            attackTime = AtkSpeed;
        }
    }

    private void Enemy_Stance(Action attack = null)
    {
        enemyAnim.SetBool("IsMoving", false);
        State = UnitState.Idle;
        attack?.Invoke();
    }

    private void Enemy_Movement(bool isFoward)
    {
        var movetoward = Vector3.MoveTowards(transform.position, Target.position,
                                             MoveSpeed * Time.fixedDeltaTime);
        var towardpos = movetoward - transform.position;
        ChangeDirection(towardpos, isFoward);
        if (isFoward)
        {
            transform.position += towardpos;
        }
        else
        {
            transform.position -= towardpos;
        }
        enemyAnim.SetBool("IsMoving", true);
        State = UnitState.Move;
    }

    private void Enemy_Death()
    {
        if (HealthPoint <= 0)
        {
            IsAlive = false;
            HealthPoint = 0;
            State = UnitState.Dead;
            if (Creep == EnemySet.Skeleton)
            {
                Destroy(gameObject);
            }
            else
            {
                enemyAnim.SetTrigger("Falling");
                Destroy(gameObject, 3f);
            }
        }
    }

    private void ChangeDirection(Vector2 direction, bool isFoward)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            var flag = isFoward ? direction.x > 0 : direction.x < 0;
            SetAnimParameter(flag ? Vector2.right : Vector2.left);
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            var flag = isFoward ? direction.y > 0 : direction.y < 0;
            SetAnimParameter(flag ? Vector2.up : Vector2.down);
        }
    }

    private void SetAnimParameter(Vector2 vector)
    {
        enemyAnim.SetFloat("AxisX", vector.x);
        enemyAnim.SetFloat("AxisY", vector.y);
    }

    private Vector3 RandomPosition(Vector3 basepos)
    {
        float x = UnityEngine.Random.Range(-1f, 1f);
        float y = UnityEngine.Random.Range(-1f, 1f);
        var rand = basepos + new Vector3(x, y);
        return rand;
    }

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        Target = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        if (IsAlive)
        {
            var distance = Vector3.Distance(transform.position, Target.position);
            if (distance < 1.5f && IsFoward)
            {
                Enemy_Stance(Enemy_Attack);
            }
            else
            {
                if (IsFoward)
                {
                    Enemy_Movement(true);
                }
                else
                {
                    if (distance < 6f)
                    {
                        Enemy_Stance(Enemy_Attack);
                    }
                    if (distance < 5f)
                    {
                        Enemy_Movement(false);
                    }
                    else if (distance > 6f)
                    {
                        Enemy_Movement(true);
                    }
                }
            }

            if (attackTime > 0f)
            {
                attackTime -= Time.deltaTime;
                State = UnitState.Attack;
            }

            if (attackTime < 0f)
            {
                attackTime = 0f;
                State = UnitState.Idle;
            }

        }
        Enemy_Death();
    }
}

