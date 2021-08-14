using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyControllerPrefab;
    [SerializeField]
    private PathData pathData;
    private GameManager gameManager;

    //public bool isEnemyGenerate;
    //public int generateInterval;
    //public int generateEnemyCount;
    //public int maxEnemyCount;

    //void Start()
    //{
    //    isEnemyGenerate = true;
    //    StartCoroutine(PreparateEnemyGenerate());
    //}

    public IEnumerator PreparateEnemyGenerate(GameManager gameManager)
    {
        this.gameManager = gameManager;
        int timer = 0;

        //isEnemyGenerate = true‚È‚çŒJ‚è•Ô‚·
        while(gameManager.isEnemyGenerate)
        {
            timer++;
            if(timer > gameManager.generateIntervalTime)
            {
                timer = 0;
                GenerateEnemy();
                gameManager.AddEnemyList();
                gameManager.JudgeGenerateEnemyEnd();
                //generateEnemyCount++;
                //if(generateEnemyCount > maxEnemyCount)
                //{
                //    isEnemyGenerate = false;
                //}
            }
            yield return null;
        }
    }

    public void GenerateEnemy()
    {
        EnemyController enemyController = Instantiate(enemyControllerPrefab, pathData.generateTran.position, Quaternion.identity);
    }
}
