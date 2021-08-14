using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyGenerator enemyGenerator;
    public bool isEnemyGenerate;
    public int generateIntervalTime;
    public int generateEnemyCount;
    public int maxEnemyCount;

    void Start()
    {
        isEnemyGenerate = true;
        StartCoroutine(enemyGenerator.PreparateEnemyGenerate(this));
    }

    /// <summary>
    /// “G‚Ìî•ñ‚ğList‚É’Ç‰Á
    /// </summary>
    public void AddEnemyList()
    {
        generateEnemyCount++;
    }

    /// <summary>
    /// “G‚Ì¶¬‚ğ’â~‚·‚é‚©”»’è
    /// </summary>
    public void JudgeGenerateEnemyEnd()
    {
        if(generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }
}
