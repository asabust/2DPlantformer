using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloneSkillController : MonoBehaviour
{
    [SerializeField] private float colorFadeSpeed = 1;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackRadius;

    private Transform closestTarget;
    private float cloneTimer;
    private SpriteRenderer sprite;
    private Animator animator;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sprite.color = new Color(1, 1, 1, sprite.color.a - (Time.deltaTime * colorFadeSpeed));
            if (sprite.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetupClone(float cloneDuration, bool canAttack) //(Transform _transform)
    {
        if (canAttack)
        {
            animator.SetInteger("attackNumber", Random.Range(1, 4));
        }

        // transform.position = _transform.position;
        cloneTimer = cloneDuration;
        sprite.color = Color.white;
        FaceClosestTarget();
    }

    public void AnimationTrigger()
    {
        cloneTimer = -1f;
        animator.SetInteger("attackNumber", 0);
    }

    public void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius);
        foreach (var hit in colliders)
        {
            hit.GetComponent<Enemy>()?.Damage();
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 25);
        float closestDistance = Mathf.Infinity;

        foreach (var hit in collider2Ds)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hit.transform;
                }
            }
        }

        if (closestTarget != null)
        {
            if (transform.position.x > closestTarget.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}