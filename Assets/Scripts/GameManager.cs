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
    private int destroyEnemyCount;
    [SerializeField]
    public UIManager uiManager;
    [SerializeField]
    private List<CharaController> charaList = new List<CharaController>();

    public enum GameState
    {
        Preparate,
        Play,
        Stop,
        GameUp
    }

    public GameState currentGameState;

    [SerializeField]
    private List<EnemyController> enemiesList = new List<EnemyController>();

    private DefenseBase defenseBase;
    [SerializeField]
    private MapInfo currentMapInfo;
    [SerializeField]
    private DefenseBase defenseBasePrefab;
    [SerializeField]
    private StageDataSO.StageData currentStageData;
    [SerializeField]
    private LogoEffect logoEffect;

    IEnumerator Start()
    {
        SetGameState(GameState.Preparate);
        //RefreshGameData();
        SetUpStageData();
        StartCoroutine(charaGenerator.SetUpCharaGenerator(this));
        defenseBase.SetUpDefenseBase(this, currentStageData.defenseBaseDurability, uiManager);
        yield return StartCoroutine(logoEffect.PlayOpening());
        isEnemyGenerate = true;
        SetGameState(GameState.Play);
        StartCoroutine(enemyGenerator.PrepareteEnemyGenerate(this,currentStageData));
        Debug.Log("�G����");
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// �G�̏���List�ɒǉ�
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemyList(EnemyController enemy)
    {
        enemiesList.Add(enemy);
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

    public void SetGameState(GameState nextGameState)
    {
        currentGameState = nextGameState;
    }

    public void PauseEnemies()
    {
        for(int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].PauseMove();
        }
    }

    public void ResumeEnemies()
    {
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].ResumeMove();
        }
    }

    public void RemoveEnemyList(EnemyController removeEnemy)
    {
        enemiesList.Remove(removeEnemy);
        //Debug.Log(enemiesList.Count);
        //if(enemiesList.Count == 0 && isEnemyGenerate == false)
        //{
            //Debug.Log("GameUp");
        //}
    }

    public void CountUpDestroyEnemyCount(EnemyController enemyController)
    {
        RemoveEnemyList(enemyController);
        destroyEnemyCount++;
        Debug.Log("�j�󂵂��G�̐�" + destroyEnemyCount);
        JudgeGameClear();
    }

    public void JudgeGameClear()
    {
        if(destroyEnemyCount >= maxEnemyCount)
        {
            Debug.Log("Game Clear");
            StartCoroutine(GameClearAndResult());
        }
    }

    public IEnumerator TimeToCurrency()
    {
        int timer = 0;
        while(currentGameState == GameState.Play)
        {
            timer++;
            if(timer >= GameData.instance.currencyIntervalTimer && GameData.instance.currency < GameData.instance.maxCurrency)
            {
                timer = 0;
                GameData.instance.currency = Mathf.Clamp(GameData.instance.currency += GameData.instance.addCurrencyPoint, 0, GameData.instance.maxCurrency);
                uiManager.UpdateDisplayCurrency();
            }
            yield return null;
        }
    }

    /// <summary>
    /// �I�������L�����̏���List�ɒǉ�
    /// </summary>
    /// <param name="chara"></param>
    public void AddCharaList(CharaController chara)
    {
        charaList.Add(chara);
        //�L�������J�E���g
        GameData.instance.charaPlacementCount++;
    } 

    /// <summary>
    /// �I�������L������j�����C����List����폜
    /// </summary>
    /// <param name="chara"></param>
    public void RemoveCharaList(CharaController chara)
    {
        Destroy(chara.gameObject);
        charaList.Remove(chara);
    }

    /// <summary>
    /// ���݂̔z�u���Ă���L�������̎擾
    /// </summary>
    /// <returns></returns>
    public int GetPlacementCharaCount()
    {
        return charaList.Count;
    }

    /// <summary>
    /// �z�u������I������|�b�v�A�b�v�쐬�̏����iCharaController����Ăяo�����j
    /// </summary>
    /// <param name="chara"></param>
    public void PrepareteCreateCharaPopUp(CharaController chara)
    {
        SetGameState(GameState.Stop);
        PauseEnemies();
        uiManager.CreateReturnCharaPopUp(chara, this);
    }

    /// <summary>
    /// �I�������L�����̔z�u�����̊m�F�iReturnSelectCharaPopUp����Ăяo�����j
    /// </summary>
    /// <param name="isReturnChara"></param>
    /// <param name="chara"></param>
    public void JudgeReturnChara(bool isReturnChara, CharaController chara)
    {
        if (isReturnChara)
        {
            RemoveCharaList(chara);
        }
        SetGameState(GameState.Play);
        ResumeEnemies();
        StartCoroutine(TimeToCurrency());
    }

    /// <summary>
    /// �X�e�[�W�f�[�^�̐ݒ�
    /// </summary>
    private void SetUpStageData()
    {
        currentStageData = DataBaseManager.instance.stageDataSO.stageDatasList[GameData.instance.stageNo]; //�Q�[���f�[�^����X�e�[�W�f�[�^���擾
        generateIntervalTime = currentStageData.generateIntervalTime;�@                                    //�G�𐶐�����Ԋu��ݒ�
        maxEnemyCount = currentStageData.mapInfo.appearEnemyInfos.Length;                                  //�o������G�̐���ݒ�

        currentMapInfo = Instantiate(currentStageData.mapInfo);                                           //�}�b�v���쐬
        charaGenerator.SetUpMapInfoandGrid(currentMapInfo.GetMapInfoandGrid());
        //charaGenerator.SetUpMapInfo(currentMapInfo.GetMapInfo());
        //charaGenerator.SetUpMapInfoGrid(currentMapInfo.GetMapInfoGrid());
        defenseBase = Instantiate(defenseBasePrefab, currentMapInfo.GetDefenseBaseTran());�@�@�@�@�@�@�@�@ //DefenseBase ��z�u

        PathData[] pathDatas = new PathData[currentStageData.mapInfo.appearEnemyInfos.Length];             //�G�̌o�H�̏������锠
        for(int i = 0; i < currentStageData.mapInfo.appearEnemyInfos.Length; i++)
        {
            pathDatas[i] = currentStageData.mapInfo.appearEnemyInfos[i].enemyPathData;�@                   //�G1�̂��o�H��ݒ� 
        }
        enemyGenerator.SetUpPathDatas(pathDatas);
    }

    private IEnumerator GameClearAndResult()
    {
        GameUpToCommon();
        yield return StartCoroutine(logoEffect.PlayClear());
        GameData.instance.totalClearPoint += currentStageData.clearPoint;
        GameData.instance.stageNo++;
        if (!GameData.instance.clearStageNosList.Contains(GameData.instance.stageNo))
        {
            GameData.instance.clearStageNosList.Add(GameData.instance.stageNo);
        }
        SceneStateManager.instance.PreparateNextScene(SceneType.StageSelect);
    }

    private void GameUpToCommon()
    {
        SetGameState(GameState.GameUp);
        charaGenerator.InactivatePlacementCharaSelectPopUp();
    }

    public void GameOver()
    {
        GameUpToCommon();
        SceneStateManager.instance.PreparateNextScene(SceneType.StageSelect);
    }
}
