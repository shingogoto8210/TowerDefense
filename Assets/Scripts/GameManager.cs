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
    [SerializeField]
    private CharaGenerator charaGenerator;

    void Start()
    {
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));
        isEnemyGenerate = true;
        StartCoroutine(enemyGenerator.PrepareteEnemyGenerate(this));
    }

    /// <summary>
    /// �G�̏���List�ɒǉ�
    /// </summary>
    public void AddEnemyList()
    {
        generateEnemyCount++;
    }

    /// <summary>
    /// �G�̐������~���邩����
    /// </summary>
    public void JudgeGenerateEnemyEnd()
    {
        if(generateEnemyCount >= maxEnemyCount)
        {
            isEnemyGenerate = false;
        }
    }
}
