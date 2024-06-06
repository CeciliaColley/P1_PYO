using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Act()
    {
        enemy.DetermineTarget();

    }

}
