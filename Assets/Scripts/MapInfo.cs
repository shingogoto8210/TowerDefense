using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapInfo : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemaps;�@//Walk����Tilemap���w�肷��
    [SerializeField]
    private Grid grid;  //Base����Grid���w�肷��
    [SerializeField]
    private Transform defenseBaseTran;�@�@//DefenseBase�𐶐�����ʒu

    /// <summary>
    /// �o������G�l�~�[1�̕��̏��p�N���X
    /// </summary>
    [System.Serializable]
    public class AppearEnemyInfo
    {
        [Header("x = �G�̔ԍ��B�|1�Ȃ烉���_��")]
        public int enemyNo;

        [Header("�G�̏o���n�_�̃����_�����Btrue�Ȃ烉���_��")]
        public bool isRandomPos;

        public PathData enemyPathData;  //�ړ��p�o�H�̏��
    }

    public AppearEnemyInfo[] appearEnemyInfos;  //�����̏o������G�l�~�[�̏����Ǘ����邽�߂̔z��


    /// <summary>
    /// �}�b�v�̏����擾
    /// </summary>
    /// <returns></returns>
    public (Tilemap, Grid) GetMapInfoandGrid()
    {
        return (this.tilemaps, this.grid);
    }

    /// <summary>
    /// �h�q���_�̏����擾
    /// </summary>
    /// <returns></returns>
    public Transform GetDefenseBaseTran()
    {
        return defenseBaseTran;
    }
}
