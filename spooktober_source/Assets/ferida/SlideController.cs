using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    public bool _working = true;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderTextClick;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private Image _ZoneImage;
    [HideInInspector] public int cicloAtual = 0;
    private int totalPlantas = 1;
    public int speed;
    public Transform testeParent;
    public GameObject parentt;
    private bool runningAnimation = false;
    private float my_time = 0f;
    private float _correctClickZone;
    private float _sliderValue;
    public Animator anim;
    private List<Image> plants = new List<Image>();

    void Start()
    {
        CreatePlant();
        _slider.onValueChanged.AddListener((v) =>{
            _sliderText.text = v.ToString("0.00");
            _sliderValue = v;
        });
        StartCoroutine(UpdateSlider());
        StartCoroutine(MyTime());
    }

    void Update()
    {
        if((Input.GetKeyDown("space") || Input.GetKeyDown("e")) && !runningAnimation)
            {
                _sliderTextClick.text = _sliderValue.ToString("0.00");
                StartCoroutine(ClickedOnTime(_sliderValue));
            }
    }

    IEnumerator MyTime(){
        while(_working){
            if(runningAnimation){
                yield return new WaitForSeconds(1.3f);
                runningAnimation = false;
            }
            my_time += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator UpdateSlider()
    {
        while(_working)
        {
            _slider.value = Mathf.PingPong(my_time*speed,160f);
            yield return new WaitForSeconds(0.0003f);
        }
    }

    IEnumerator ClickedOnTime(float clickTime)
    {
        runningAnimation = true;
        anim.Play("cavando");
        // anim.SetBool("cavando",true);
        yield return new WaitForSeconds(1.3f);
        print("Contage tamanho: " +plants.Count);
        foreach(Image plant in plants)
        {   
            _correctClickZone = plant.rectTransform.anchoredPosition[0] / 3.625f;
            print("Clickzone: "+ _correctClickZone);
            // print("Clicktime: "+ clickTime);
            if(clickTime >= _correctClickZone-10f && clickTime <= _correctClickZone+10f)
            {
                _sliderTextClick.text = "Ganhou " + clickTime;
                print($"clicou {clickTime} | certo {_correctClickZone} | GANHOU");
                totalPlantas+=1;
                RemovePlant(plant);
                CreatePlant();
            }
        }
        // anim.SetBool("cavando",false);
        runningAnimation = false;
    }

    private (float,float) PositionPlant()
    {
        float xRandom = Random.Range(40f, 150f); //Onde o mato vai spawnar
        _correctClickZone = xRandom;
        return (xRandom  * 3.625f, _ZoneImage.rectTransform.anchoredPosition[1]);
    }

    private void CreatePlant()
    {
        for (int i=0; i<2; i++)
        {
            var newObj = GameObject.Instantiate(_ZoneImage);
            newObj.transform.SetParent(testeParent, false);
            var cords = PositionPlant();
            newObj.rectTransform.anchoredPosition = new Vector2(cords.Item1, cords.Item2);
            plants.Add(newObj);
            
        }
    }

    private void RemovePlant(Image image)
    {
        plants.Remove(image);
        Destroy(image.gameObject);
    }

    private void ControlNumberOfPlants() //Função responsável em criar mais plantas com o tempo, não está pronta
    {

    }

}
