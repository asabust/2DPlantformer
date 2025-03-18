using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision info")]
    public Transform attackCheck;
    public float attackRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 0.4f;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance = 0.4f;
    [SerializeField] protected Transform stairsCheck;
    [SerializeField] protected float stairsCheckDistance = 0.4f;
    [SerializeField] protected LayerMask groundLayer;

    [Header("konckback info")]
    [SerializeField] protected Vector2 konckbackForce;
    [SerializeField] protected float konckbackDuration = 0.07f;
    protected bool isKnockedBack = false;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

#region Components

    public EntityFX fx;
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

#endregion

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
    }

    public virtual void Damage()
    {
        Debug.Log($"{gameObject.name} was damaged");
        fx.PlayFlashEffect();
        if (konckbackDuration > 0) StartCoroutine(KnockBack());
    }

    protected virtual IEnumerator KnockBack()
    {
        isKnockedBack = true;
        rb.velocity = new Vector2(konckbackForce.x * -facingDir, konckbackForce.y);
        yield return new WaitForSeconds(konckbackDuration);
        isKnockedBack = false;
    }

    public void SetVelocity(float x, float y)
    {
        if (isKnockedBack) return;

        rb.velocity = new Vector2(x, y);
        FlipController(x);
    }

    public void StopMove()
    {
        if (isKnockedBack) return;
        rb.velocity = Vector2.zero;
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController(float _x)
    {
        if ((_x > 0 && !facingRight) || (_x < 0 && facingRight))
            Flip();
    }


#region Collision

    public virtual bool IsGrounded() =>
        Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

    public virtual bool IsWall() =>
        Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, groundLayer);

    public virtual RaycastHit2D IsStairsDetected() =>
        Physics2D.Raycast(stairsCheck.position, Vector2.right * facingDir, stairsCheckDistance, groundLayer);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
        if (stairsCheck)
        {
            Gizmos.DrawLine(stairsCheck.position,
                new Vector3(stairsCheck.position.x + stairsCheckDistance, stairsCheck.position.y));
        }
    }

#endregion
}