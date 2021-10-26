using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderTextClick;
    [SerializeField] private TextMeshProUGUI _sliderText;
    public RectTransform ZoneImage;

    public bool _working = true;
    private float zoneHitBox;
    private float _correctClickZone;
    private float _sliderValue;
    
    void Start()
    {

        PositionImage();
        

        _slider.onValueChanged.AddListener((v) =>{
            _sliderText.text = v.ToString("0.00");
            _sliderValue = v;
        });
        StartCoroutine(UpdateSlider());
    }

    void Update()
    {
        if(Input.GetKeyDown("space") && _working)
            {
                _sliderTextClick.text = _sliderValue.ToString("0.00");
                //_working = false;
                ClickedOnTime(_sliderValue);
            }
    }

    IEnumerator UpdateSlider()
    {
        while(_working){
            //Da esquerda até a direita
            for (float index = 1.0f; index < 160.0f; index+=0.5f)
            {
                _slider.value = index;
                yield return new WaitForSeconds(0.007f);
            }
            //Da direita até a esquerda
            for (float index = 160.0f; index > 1.0f; index-=0.5f)
            {
                _slider.value = index;
                yield return new WaitForSeconds(0.007f);
            }
        }
    }

    private void ClickedOnTime(float clickTime){
        print($"clicou {clickTime} | certo {_correctClickZone}");
        zoneHitBox = ZoneImage.anchoredPosition[0] / 2;
        if(clickTime >= _correctClickZone-5f && clickTime <= _correctClickZone+5f){
            _sliderTextClick.text = "Ganhou " + clickTime;
        }
        else
        {
            _sliderTextClick.text = "Perdeu " + clickTime;
        }
    }

    private void PositionImage(){
        float xRandom = Random.Range(20f, 140f);
        _correctClickZone = xRandom;
        ZoneImage.anchoredPosition = new Vector2(xRandom,0);

    }
}