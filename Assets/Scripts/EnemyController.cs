using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("移動経路の情報")]
    private PathData pathData;

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    [SerializeField, Header("最大HP")]
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

        //xにtransformを代入して，positionに変換して配列に直す。
        paths = pathData.pathTranArray.Select(x => x.position).ToArray();
        //OnWaypointChange(x => ChangeAnimeDirection(x))
        tween = transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);
    }

    //void Update()
    //{
      //  ChangeAnimeDirection();
    //}

    /// <summary>
    /// 敵の進行方向を取得して、移動アニメと同期
    /// </summary>
    private void ChangeAnimeDirection(int index)
    {
        //Debug.Log(index);

        //次の移動先の地点がない場合には、ここで処理を終了する
        if(index >= paths.Length)
        {
            return;
        }
        
        //目標の位置と現在の位置との距離と方向を取得し、正規化処理を行い、単位ベクトルとする(方向の情報は持ちつつ、距離による速度差をなくして一定値にする）
        Vector3 direction = (paths[index]- transform.position).normalized;
        //Debug.Log(direction);

        //アニメーションのPalameterの値を更新し、移動アニメのBlendTreeを制御して移動の方向と移動アニメを同期
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);

        //if(transform.position.x > paths[index].x)
        //{
        //    anim.SetFloat("Y", 0f);
        //    anim.SetFloat("X", -1.0f);
        //    Debug.Log("左方向");

        //}else if(transform.position.y < paths[index].y)
        //{
        //    anim.SetFloat("X", 0f);
        //    anim.SetFloat("Y", 1.0f);
        //    Debug.Log("上方向");

        //}else if(transform.position.y > paths[index].y)
        //{
        //    anim.SetFloat("X",0f);
        //    anim.SetFloat("Y", -1.0f);
        //    Debug.Log("下方向");

        //}else{
        //    anim.SetFloat("Y",0f);
        //    anim.SetFloat("X",1.0f);
        //    Debug.Log("右方向");
        //}

    //currentPos = transform.position;
    }

    /// <summary>
    /// ダメージ計算
    /// </summary>
    /// <param name="amount"></param>
    public void CulcDamage(int amount)
    {
        hp = Mathf.Clamp(hp -= amount, 0, maxHp);
        Debug.Log("残りHP：" + hp);

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
