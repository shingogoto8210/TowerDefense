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
    private GameManager gameManager;
    private UIManager uiManager;

    public void SetUpDefenseBase(GameManager gameManager,int currentDefenseBaseDurability,UIManager uiManager)
    {
        this.gameManager = gameManager;
        this.uiManager = uiManager;
        if (GameData.instance.isDebug)
        {
            maxDefenseBaseDurability = GameData.instance.defenseBaseDurability;
        }
        else
        {
            maxDefenseBaseDurability = currentDefenseBaseDurability;
        }
        this.defenseBaseDurability = maxDefenseBaseDurability;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyController enemy))
        {
            defenseBaseDurability = Mathf.Clamp(defenseBaseDurability - enemy.attackPower, 0, maxDefenseBaseDurability);
            coreHPSlider.UpdateSlider(defenseBaseDurability);
            CreateDamageEffect();
            if(defenseBaseDurability <= 0 && gameManager.currentGameState == GameManager.GameState.Play)
            {
                Debug.Log("Game Over");
                StartCoroutine(gameManager.GameOver());
            }
            enemy.DestroyEnemy();
        }
    }

    private void CreateDamageEffect()
    {
        GameObject effect = Instantiate(BattleEffectManager.instance.GetEffect(EffectType.Hit_DefenseBase), transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);

    }
}
