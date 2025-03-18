using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone Info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration = 0.5f;

    [Space]
    [SerializeField] private bool canAttack = true;

    public void CreateClone(Vector3 position)
    {
        GameObject newClone = Instantiate(clonePrefab, position, Quaternion.identity);
        newClone.GetComponent<CloneSkillController>()?.SetupClone(cloneDuration, canAttack);
    }
}