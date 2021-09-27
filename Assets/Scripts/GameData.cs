using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    [Header("�R�X�g�p�̒ʉ�")]
    public int currency;
    [Header("�ʉ݂̍ő�l")]
    public int maxCurrency;
    [Header("�ʉ݉��Z�܂ł̑ҋ@����")]
    public int currencyIntervalTimer;
    [Header("�ʉ݂̉��Z��")]
    public int addCurrencyPoint;
    [Header("���������L�����̐�")]
    public int charaPlacementCount;
    [Header("�f�o�b�O���[�h�̐؂�ւ�")]
    public bool isDebug; //true�̏ꍇ�C�f�o�b�O���[�h�Ƃ���
    public int defenseBaseDurability;
    public int stageNo;
    public List<int> clearStageNosList = new List<int>();
    public int totalClearPoint;
    public List<int> engageCharaNosList = new List<int>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        clearStageNosList.Add(0);
        totalClearPoint = 0;
        engageCharaNosList.Add(0);
    }
}
