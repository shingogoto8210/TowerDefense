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
    public int hp;
    public int attackPower;
    private Tween tween;
    private Vector3[] paths;
    private Animator anim;
    private GameManager gameManager;
    public EnemyDataSO.EnemyData enemyData;

    /// <summary>
    /// 敵の設定
    /// </summary>
    /// <param name="pathssData"></param>
    public void SetUpEnemyController(Vector3[] pathsData,GameManager gameManager,EnemyDataSO.EnemyData enemyData)
    {
        this.enemyData = enemyData;
        moveSpeed = this.enemyData.moveSpeed;
        attackPower = this.enemyData.attackPower;
        maxHp = this.enemyData.hp;
        this.gameManager = gameManager;
        hp = maxHp;
        if(TryGetComponent(out anim))
        {
            SetUpAnimation();
        }
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
        CreateDamageEffect();
        Debug.Log("残りHP：" + hp);
        if(hp <= 0)
        {
            CreateDestroyEffect();
            DestroyEnemy();
        }
        StartCoroutine(WaitMove());
    }

    /// <summary>
    /// ヒットムーブ演出
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitMove()
    {
        tween.timeScale = 0.05f;
        yield return new WaitForSeconds(0.5f);
        tween.timeScale = 1.0f;
    }
    /// <summary>
    /// 敵破壊処理
    /// </summary>
    public void DestroyEnemy()
    {
        tween.Kill();
        Destroy(gameObject);
        gameManager.CountUpDestroyEnemyCount(this);
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

    private void SetUpAnimation()
    {
        if(enemyData.enemyOverrideController != null)
        {
            anim.runtimeAnimatorController = enemyData.enemyOverrideController;
        }
    }

    private void CreateDamageEffect()
    {
        GameObject effect = Instantiate(BattleEffectManager.instance.GetEffect(EffectType.Hit_Enemy), transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);

    }

    private void CreateDestroyEffect()
    {
        GameObject effect = Instantiate(BattleEffectManager.instance.GetEffect(EffectType.Destroy_Enemy), transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);

    }
}
