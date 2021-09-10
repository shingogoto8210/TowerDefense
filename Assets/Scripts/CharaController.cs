using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaController : MonoBehaviour
{
    [SerializeField] private EnemyController enemy;
    [SerializeField, Header("攻撃するまでの待機時間")]
    private float intervalAttackTime = 60.0f;
    [SerializeField, Header("攻撃力")]
    private int attackPower = 1;
    [SerializeField] private bool isAttack;
    [SerializeField] private int attackCount;
    [SerializeField] private Text attackCountText;
    [SerializeField]
    private BoxCollider2D attackRangeArea;
    [SerializeField]
    private CharaData charaData;
    private GameManager gameManager;
    //private SpriteRenderer spriteRenderer;
    private Animator anim;
    private string overrideClipName = "Chara0_front";
    private AnimatorOverrideController overrideController;
    private void Start()
    {
         attackCountText.text = attackCount.ToString();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //攻撃中ではない場合で、かつ、敵の情報を未取得である場合
        if (!isAttack && !enemy)
        {
            //Debug.Log("敵発見");
            //敵の情報を取得する。EnemyControllerがアタッチされているゲームオブジェクトを判別しているので、ここで、今までのTagによる判定と同じ動作で判定が行える
            if (collision.gameObject.TryGetComponent(out enemy))
            {
                isAttack = true;
                //攻撃の準備に入る
                StartCoroutine(PrepareteAttack());
            }
        }
    }

    /// <summary>
    /// 攻撃準備
    /// </summary>
    /// <returns></returns>
    public IEnumerator PrepareteAttack()
    {
        Debug.Log("攻撃準備開始");
        int timer = 0;
        while(isAttack)
        {
            if (gameManager.currentGameState == GameManager.GameState.Play)
            {
                timer++;
                if (timer > intervalAttackTime)
                {
                    timer = 0;

                    Attack();
                    attackCount--;
                    UpdateDisplayAttackCount();
                    if (attackCount <= 0)
                    {
                        Destroy(gameObject);
                        gameManager.RemoveCharaList(this);
                    }
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        if (enemy != null)
        {
            Debug.Log("攻撃");
            enemy.CulcDamage(attackPower);  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("敵なし");
            isAttack = false;
            enemy = null;
        }
    }

    /// <summary>
    /// 残り攻撃回数の表示更新
    /// </summary>
    private void UpdateDisplayAttackCount()
    {
        attackCountText.text = attackCount.ToString();
    }

    /// <summary>
    /// キャラの設定
    /// </summary>
    /// <param name="charaData"></param>
    /// <param name="gameManager"></param>
    public void SetUpChara(CharaData charaData, GameManager gameManager)
    {
        this.charaData = charaData;
        this.gameManager = gameManager;
        attackPower = this.charaData.attackPower;
        intervalAttackTime = this.charaData.intervalAttackTime;
        attackRangeArea.size = DataBaseManager.instance.GetAttackRangeSize(this.charaData.attackRange);
        attackCount = this.charaData.maxAttackCount;
        UpdateDisplayAttackCount();
        //if(TryGetComponent(out spriteRenderer))
        //{
        //spriteRenderer.sprite = this.charaData.charaSprite;
        //}
        SetUpAnimation();
    }

    private void SetUpAnimation()
    {
        if(TryGetComponent(out anim))
        {
            overrideController = new AnimatorOverrideController();
            overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;
            anim.runtimeAnimatorController = overrideController;
            AnimatorStateInfo[] layerInfo = new AnimatorStateInfo[anim.layerCount];
            for (int i = 0; i < anim.layerCount; i++)
            {
                layerInfo[i] = anim.GetCurrentAnimatorStateInfo(i);
            }
                overrideController[overrideClipName] = this.charaData.charaAnim;
                anim.runtimeAnimatorController = overrideController;
                anim.Update(0.0f);
                for(int i = 0; i < anim.layerCount; i++)
                {
                    anim.Play(layerInfo[i].fullPathHash, i, layerInfo[i].normalizedTime);
                }
        }
    }

    public void OnClickChara()
    {
        gameManager.PrepareteCreateCharaPopUp(this);
    }
}
