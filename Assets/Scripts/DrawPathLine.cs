using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPathLine : MonoBehaviour
{
    [SerializeField]
    private float startLineWidth = 0.5f;

    [SerializeField]
    private float endLineWidth = 0.5f;

    public void CreatePathLine(Vector3[] drawPaths)
    {
        TryGetComponent(out LineRenderer lineRenderer);

        //���C���̑����𒲐�
        lineRenderer.startWidth = startLineWidth;
        lineRenderer.endWidth = endLineWidth;

        //�������郉�C���̒��_����ݒ�i����͎n�_�ƏI�_���P���j
        lineRenderer.positionCount = drawPaths.Length;

        //���C�����P����
        lineRenderer.SetPositions(drawPaths);

    }
}
