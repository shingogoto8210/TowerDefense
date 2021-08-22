using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreHPSlider : MonoBehaviour
{
    private Slider slider;
    private int coreHP;

    void Start()
    {
        coreHP = transform.root.gameObject.GetComponent<Core>().currentCoreHP;
        slider = GetComponent<Slider>();
        slider.value = coreHP;
        slider.maxValue = coreHP;
    }

    void Update()
    {
        coreHP = transform.root.gameObject.GetComponent<Core>().currentCoreHP;
        slider.value = coreHP;
    }
}
