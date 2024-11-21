using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTriggers : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    public void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}