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

    private void Start()
    {
         attackCountText.text = attackCount.ToString();
    }
    private void Update()
    {
        //Debug.Log(isAttack);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //�U�����ł͂Ȃ��ꍇ�ŁA���A�G�̏��𖢎擾�ł���ꍇ
        if (!isAttack && !enemy)
        {
            Debug.Log("�G����");
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
            timer++;
            if(timer > intervalAttackTime)
            {
                timer = 0;

                Attack();
                attackCount--;
                UpdateDisplayCount();
                if (attackCount <= 0)
                {
                    Destroy(gameObject);
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
    private void UpdateDisplayCount()
    {
        attackCountText.text = attackCount.ToString();
    }

}
