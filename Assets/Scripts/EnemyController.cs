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
    public int hp;
    public int attackPower;
    private Tween tween;
    private Vector3[] paths;
    private Animator anim;
    private GameManager gameManager;
    public EnemyDataSO.EnemyData enemyData;

    /// <summary>
    /// �G�̐ݒ�
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
    /// �G�̐i�s�������擾���āA�ړ��A�j���Ɠ���
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
    /// �_���[�W�v�Z
    /// </summary>
    /// <param name="amount"></param>
    public void CulcDamage(int amount)
    {
        hp = Mathf.Clamp(hp -= amount, 0, maxHp);
        CreateDamageEffect();
        Debug.Log("�c��HP�F" + hp);
        if(hp <= 0)
        {
            CreateDestroyEffect();
            DestroyEnemy();
        }
        StartCoroutine(WaitMove());
    }

    /// <summary>
    /// �q�b�g���[�u���o
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitMove()
    {
        tween.timeScale = 0.05f;
        yield return new WaitForSeconds(0.5f);
        tween.timeScale = 1.0f;
    }
    /// <summary>
    /// �G�j�󏈗�
    /// </summary>
    public void DestroyEnemy()
    {
        tween.Kill();
        Destroy(gameObject);
        gameManager.CountUpDestroyEnemyCount(this);
    }

    /// <summary>
    /// �ړ����ꎞ��~
    /// </summary>
    public void PauseMove()
    {
        tween.Pause();
    }

    /// <summary>
    /// �ړ����J�n
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
