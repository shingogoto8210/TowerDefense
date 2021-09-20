using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEffectManager : MonoBehaviour
{
    public static BattleEffectManager instance;
    public GameObject destroyCharaEffectPrefab;
    public GameObject destroyEnemyEffectPrefab;
    public GameObject hitEnemyEffectPrefab;
    public GameObject hitDefenseBaseEffectPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// EffectType�Ŏw�肵���G�t�F�N�g�̃v���t�@�u���擾
    /// </summary>
    /// <param name="effectType"></param>
    /// <returns></returns>
    public GameObject GetEffect(EffectType effectType)
    {
        return effectType switch
        {
            EffectType.Destroy_Chara => destroyCharaEffectPrefab,
            EffectType.Destroy_Enemy => destroyEnemyEffectPrefab,
            EffectType.Hit_Enemy => hitEnemyEffectPrefab,
            EffectType.Hit_DefenseBase => hitDefenseBaseEffectPrefab,
            _=>destroyCharaEffectPrefab,
        };
    }
}
