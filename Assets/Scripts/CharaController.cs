using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField] private EnemyController enemy;

    [SerializeField, Header("�U������܂ł̑ҋ@����")]
    private float intervalAttackTime = 60.0f;

    [SerializeField, Header("�U����")]
    private int attackPower = 1;

    [SerializeField] private bool isAttack;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //�U�����ł͂Ȃ��ꍇ�ŁA���A�G�̏��𖢎擾�ł���ꍇ
        if (!isAttack && !enemy)
        {
            Debug.Log("�G����");

            //�G�̏����擾����BEnemyController���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�𔻕ʂ��Ă���̂ŁA�����ŁA���܂ł�Tag�ɂ�锻��Ɠ�������Ŕ��肪�s����
            if (collision.gameObject.TryGetComponent(out enemy))
            {
                //�����擾�ł�����A�U���Ԑ��ɂ���
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

        //�U�����̊Ԃ������[�v�������J��Ԃ�
        while(isAttack)
        {
            timer++;

            if(timer > intervalAttackTime)
            {
                timer = 0;

                Attack();
            }

            yield return null;
        }
    }

    /// <summary>
    /// �U��
    /// </summary>
    private void Attack()
    {
        Debug.Log("�U��");
        enemy.CulcDamage(attackPower);
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

}
