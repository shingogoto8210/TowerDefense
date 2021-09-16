using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;
    public CharaDataSO charaDataSO;
    public AttackRangeSizeSO attackRangeSizeSO;
    public EnemyDataSO enemyDataSO;
    public StageDataSO stageDataSO;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// AttackRangeType‚©‚çBoxCollider—p‚ÌSize‚ðŽæ“¾
    /// </summary>
    /// <param name="attackRangeType"></param>
    /// <returns></returns>
    public Vector2 GetAttackRangeSize(AttackRangeType attackRangeType)
    {
        return attackRangeSizeSO.attackRangeSizeList.Find(x => x.attackRangeType == attackRangeType).size;
    }
}
