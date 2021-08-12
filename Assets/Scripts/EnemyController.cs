using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("�ړ��o�H�̏��")]
    private PathData pathData;

    [SerializeField, Header("�ړ����x")]
    private float moveSpeed;

    [SerializeField, Header("�ő�HP")]
    private int maxHp;

    [SerializeField] private int hp;

    private Tween tween;

    private Vector3[] paths;

    private Animator anim;

    //private Vector3 currentPos;
    void Start()
    {
        hp = maxHp;

        TryGetComponent(out anim);

        //paths = new Vector3[pathData.pathTranArray.Length];

        //for (int i = 0; i < pathData.pathTranArray.Length; i++)
        //{
        //    paths[i] = pathData.pathTranArray[i].position;
        //}

        //x��transform�������āCposition�ɕϊ����Ĕz��ɒ����B
        paths = pathData.pathTranArray.Select(x => x.position).ToArray();
        //OnWaypointChange(x => ChangeAnimeDirection(x))
        tween = transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);
    }

    //void Update()
    //{
      //  ChangeAnimeDirection();
    //}

    /// <summary>
    /// �G�̐i�s�������擾���āA�ړ��A�j���Ɠ���
    /// </summary>
    private void ChangeAnimeDirection(int index)
    {
        //Debug.Log(index);

        //���̈ړ���̒n�_���Ȃ��ꍇ�ɂ́A�����ŏ������I������
        if(index >= paths.Length)
        {
            return;
        }
        
        //�ڕW�̈ʒu�ƌ��݂̈ʒu�Ƃ̋����ƕ������擾���A���K���������s���A�P�ʃx�N�g���Ƃ���(�����̏��͎����A�����ɂ�鑬�x�����Ȃ����Ĉ��l�ɂ���j
        Vector3 direction = (paths[index]- transform.position).normalized;
        //Debug.Log(direction);

        //�A�j���[�V������Palameter�̒l���X�V���A�ړ��A�j����BlendTree�𐧌䂵�Ĉړ��̕����ƈړ��A�j���𓯊�
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);

        //if(transform.position.x > paths[index].x)
        //{
        //    anim.SetFloat("Y", 0f);
        //    anim.SetFloat("X", -1.0f);
        //    Debug.Log("������");

        //}else if(transform.position.y < paths[index].y)
        //{
        //    anim.SetFloat("X", 0f);
        //    anim.SetFloat("Y", 1.0f);
        //    Debug.Log("�����");

        //}else if(transform.position.y > paths[index].y)
        //{
        //    anim.SetFloat("X",0f);
        //    anim.SetFloat("Y", -1.0f);
        //    Debug.Log("������");

        //}else{
        //    anim.SetFloat("Y",0f);
        //    anim.SetFloat("X",1.0f);
        //    Debug.Log("�E����");
        //}

    //currentPos = transform.position;
    }

    /// <summary>
    /// �_���[�W�v�Z
    /// </summary>
    /// <param name="amount"></param>
    public void CulcDamage(int amount)
    {
        hp = Mathf.Clamp(hp -= amount, 0, maxHp);
        Debug.Log("�c��HP�F" + hp);

        if(hp <= 0)
        {
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        tween.Kill();
        Destroy(gameObject);
    }
}
