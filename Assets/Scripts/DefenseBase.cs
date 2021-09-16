using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseBase : MonoBehaviour
{
    //private EnemyController enemy;
    [Header("‘Ï‹v’l")]
    public int maxDefenseBaseDurability;
    public int defenseBaseDurability;
    [SerializeField]
    private DefenseBaseHPSlider coreHPSlider;
    [SerializeField]
    //private GameManager gameManager;

    private void Start()
    {
        if (GameData.instance.isDebug)
        {
            maxDefenseBaseDurability = GameData.instance.defenseBaseDurability;
        }
        else
        {
            maxDefenseBaseDurability = defenseBaseDurability;
        }
        defenseBaseDurability = maxDefenseBaseDurability;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemy))
        {
            defenseBaseDurability = Mathf.Clamp(defenseBaseDurability - enemy.attackPower, 0, maxDefenseBaseDurability);
            coreHPSlider.UpdateSlider(defenseBaseDurability);
            if(defenseBaseDurability <= 0)
            {
                Debug.Log("Game Over");
            }
            enemy.DestroyEnemy();
        }
    }
}
