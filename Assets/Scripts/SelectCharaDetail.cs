using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectCharaDetail : MonoBehaviour
{
    [SerializeField]
    private Button btnSelectCharaDetail;
    [SerializeField]
    private Image imgChara;
    private PlacementCharaSelectPopUp placementCharaSelectPopUp;
    private CharaData charaData;

    /// <summary>
    /// SelectCharaDetail�̐ݒ�
    /// </summary>
    /// <param name="placementCharaSelectPopUp"></param>
    /// <param name="charaData"></param>
    public void SetUpSelectCharaDetail(PlacementCharaSelectPopUp placementCharaSelectPopUp, CharaData charaData)
    {
        this.placementCharaSelectPopUp = placementCharaSelectPopUp;
        this.charaData = charaData;

        //TODO �{�^���������Ȃ���Ԃɐ؂�ւ���

        imgChara.sprite = this.charaData.charaSprite;

        //TODO�J�����V�[�̒l���X�V�����x�ɃR�X�g���x�����邩�m�F����

        //�{�^���Ƀ��\�b�h��o�^
        btnSelectCharaDetail.onClick.AddListener(OnClickSelectCharaDetail);

        //TODO�@�R�X�g�ɉ����ă{�^���������邩�ǂ�����؂�ւ���
    }

    private void OnClickSelectCharaDetail()
    {
        //TODO�@�A�j�����o

        //�^�b�v����SelectCharaDetail�̏����|�b�v�A�b�v�ɑ���
        placementCharaSelectPopUp.SetSelectCharaDetail(charaData);
    }
}
