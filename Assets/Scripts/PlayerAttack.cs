using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Vector2 facing;
    private Animator animator;

    private bool attacking;
    private float timer;
    private float attackTimer = 0.25f;


    private float cooldownTime = 0.25f;
    private float cooldownTimer;
    private bool onCooldown;

    [SerializeField] private GameObject AttackTemp;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    void Update()
    {
        if (attacking)
        {
            timer += Time.deltaTime;
            if(timer >= attackTimer)
            {
                attacking = false;
                animator.SetBool("IsAttacking", attacking);
                timer = 0;
            }
        }
        if (onCooldown)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= cooldownTime)
            {
                onCooldown = false;
            }
        }
    }

    public void Attack()
    {
        animator.SetFloat("AttackX", playerMovement.GetUnalteredFacing().x);
        animator.SetFloat("AttackY", playerMovement.GetUnalteredFacing().y);
        attacking = true;

        animator.SetBool("IsAttacking", attacking);

        facing = playerMovement.GetFacing();

        if (!onCooldown)
        {
            Instantiate(AttackTemp, transform.position + (Vector3)facing, Quaternion.identity);
            onCooldown = true;
        }

    }

    public void EndAnim()
    {
        animator.ResetTrigger("IsAttacking");
    }
}
