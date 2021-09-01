using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    //private EnemyController enemy;
    [Header("‘Ï‹v’l")] public int coreHP;
    public int currentCoreHP;
    [SerializeField]
    private CoreHPSlider coreHPSlider;
    [SerializeField]
    //private GameManager gameManager;

    private void Start()
    {
        currentCoreHP = coreHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemy))
        {
            currentCoreHP = Mathf.Clamp(currentCoreHP - enemy.attackPower, 0, coreHP);
            coreHPSlider.UpdateSlider(currentCoreHP);
            if(currentCoreHP <= 0)
            {
                Debug.Log("Game Over");
            }
            enemy.DestroyEnemy();
        }
    }
}
