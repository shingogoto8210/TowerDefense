using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    private EnemyController enemy;
    [Header("‘Ï‹v’l")] public int coreHP;
    public int currentCoreHP;

    private void Start()
    {
        currentCoreHP = coreHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out enemy))
        {
            currentCoreHP = Mathf.Clamp(currentCoreHP - enemy.attackPower, 0, coreHP);
            if(currentCoreHP <= 0)
            {
                Debug.Log("Game Over");
            }
            enemy.DestroyEnemy();
        }
    }
}
