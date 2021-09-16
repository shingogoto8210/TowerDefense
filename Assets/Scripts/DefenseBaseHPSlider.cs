using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DefenseBaseHPSlider : MonoBehaviour
{
    private Slider slider;
    private int coreHP;
    [SerializeField]
    private GameObject defenseBase;

    void Start()
    {
        coreHP = defenseBase.GetComponent<DefenseBase>().defenseBaseDurability;
        slider = GetComponent<Slider>();
        slider.maxValue = coreHP;
        UpdateSlider(coreHP);
        //slider.value = coreHP;
    }

    public void UpdateSlider(int currentCoreHP)
    {
        slider.DOValue(currentCoreHP, 0.5f);
    }
}
