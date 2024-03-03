using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private float _fillTime = 5.0f;
    [SerializeField] public Slider _topSlider;
    [SerializeField] public Slider _bottomSlider;

    private void OnEnable()
    {
        _topSlider.value = 1;
        _bottomSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_topSlider.value > 0) { _topSlider.value -= Time.deltaTime * 1 / _fillTime; }
        if (_bottomSlider.value > 0) { _bottomSlider.value -= Time.deltaTime * 1 / _fillTime; }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
