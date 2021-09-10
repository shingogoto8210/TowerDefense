using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoreHPSlider : MonoBehaviour
{
    private Slider slider;
    private int coreHP;

    void Start()
    {
        coreHP = transform.root.gameObject.GetComponent<Core>().defenseBaseDurability;
        slider = GetComponent<Slider>();
        slider.maxValue = coreHP;
        UpdateSlider(coreHP);
        //slider.value = coreHP;
    }

    //void Update()
    //{
    //    coreHP = transform.root.gameObject.GetComponent<Core>().currentCoreHP;
    //    slider.value = coreHP;
    //}

    public void UpdateSlider(int currentCoreHP)
    {
        slider.DOValue(currentCoreHP, 0.5f);
    }
}
