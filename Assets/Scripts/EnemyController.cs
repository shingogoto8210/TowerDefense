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

    /// <summary>
    /// 敵の設定
    /// </summary>
    /// <param name="pathssData"></param>
    public void SetUpEnemyController(Vector3[] pathsData)
    {
        hp = maxHp;
        TryGetComponent(out anim);
        paths = pathsData;
        //OnWaypointChange(x => ChangeAnimeDirection(x))
        tween = transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);
        PauseMove();
    }

    /// <summary>
    /// 敵の進行方向を取得して、移動アニメと同期
    /// </summary>
    private void ChangeAnimeDirection(int index)
    {
        //Debug.Log(index);
        if(index >= paths.Length)
        {
            return;
        }
        Vector3 direction = (transform.position - paths[index]).normalized;
        //Debug.Log(direction);
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);
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

    /// <summary>
    /// 敵破壊処理
    /// </summary>
    public void DestroyEnemy()
    {
        tween.Kill();
        Destroy(gameObject);
    }

    /// <summary>
    /// 移動を一時停止
    /// </summary>
    public void PauseMove()
    {
        tween.Pause();
    }

    /// <summary>
    /// 移動を開始
    /// </summary>
    public void ResumeMove()
    {
        tween.Play();
    }
}
