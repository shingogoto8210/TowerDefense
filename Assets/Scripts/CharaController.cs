using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaController : MonoBehaviour
{
    [SerializeField] private EnemyController enemy;

    [SerializeField, Header("攻撃するまでの待機時間")]
    private float intervalAttackTime = 60.0f;

    [SerializeField, Header("攻撃力")]
    private int attackPower = 1;

    [SerializeField] private bool isAttack;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //攻撃中ではない場合で、かつ、敵の情報を未取得である場合
        if (!isAttack && !enemy)
        {
            Debug.Log("敵発見");

            //敵の情報を取得する。EnemyControllerがアタッチされているゲームオブジェクトを判別しているので、ここで、今までのTagによる判定と同じ動作で判定が行える
            if (collision.gameObject.TryGetComponent(out enemy))
            {
                //情報を取得できたら、攻撃態勢にする
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

        //攻撃中の間だけループ処理を繰り返す
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
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        Debug.Log("攻撃");
        enemy.CulcDamage(attackPower);
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

}
