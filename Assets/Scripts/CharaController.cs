using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaController : MonoBehaviour
{
    [SerializeField] private EnemyController enemy;
    [SerializeField, Header("�U������܂ł̑ҋ@����")]
    private float intervalAttackTime = 60.0f;
    [SerializeField, Header("�U����")]
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
        //�U�����ł͂Ȃ��ꍇ�ŁA���A�G�̏��𖢎擾�ł���ꍇ
        if (!isAttack && !enemy)
        {
            //Debug.Log("�G����");
            //�G�̏����擾����BEnemyController���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�𔻕ʂ��Ă���̂ŁA�����ŁA���܂ł�Tag�ɂ�锻��Ɠ�������Ŕ��肪�s����
            if (collision.gameObject.TryGetComponent(out enemy))
            {
                isAttack = true;
                //�U���̏����ɓ���
                StartCoroutine(PrepareteAttack());
            }
        }
    }

    /// <summary>
    /// �U������
    /// </summary>
    /// <returns></returns>
    public IEnumerator PrepareteAttack()
    {
        Debug.Log("�U�������J�n");
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
    /// �U��
    /// </summary>
    private void Attack()
    {
        if (enemy != null)
        {
            Debug.Log("�U��");
            enemy.CulcDamage(attackPower);  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("�G�Ȃ�");
            isAttack = false;
            enemy = null;
        }
    }

    /// <summary>
    /// �c��U���񐔂̕\���X�V
    /// </summary>
    private void UpdateDisplayAttackCount()
    {
        attackCountText.text = attackCount.ToString();
    }

    /// <summary>
    /// �L�����̐ݒ�
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
