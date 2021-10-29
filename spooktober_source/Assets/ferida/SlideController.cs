using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    public bool _working = true;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _pointText;
    [SerializeField] private Image _ZoneImage;
    [HideInInspector] public int pontos = 0;
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
    private Animator animPlant;
    void Start()
    {
        CreatePlant(1);
        _slider.onValueChanged.AddListener((v) =>{
            _sliderValue = v;
        });
        StartCoroutine(UpdateSlider());
        StartCoroutine(MyTime());
    }

    void Update()
    {
        if((Input.GetKeyDown("space") || Input.GetKeyDown("e")) && !runningAnimation)
            {
                StartCoroutine(ClickedOnTime(_sliderValue));
            }
    }

    IEnumerator MyTime(){
        while(_working)
        {
            if(runningAnimation)
            {
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
        yield return new WaitForSeconds(1.3f);
        
        print("Contage tamanho: " +plants.Count);
        foreach(var plant in plants)
        {   
            _correctClickZone = plant.rectTransform.anchoredPosition[0] / 3.625f;
            print("Clickzone: "+ _correctClickZone);
            if(clickTime >= _correctClickZone-10f && clickTime <= _correctClickZone+10f)
            {
                print($"clicou {clickTime} | certo {_correctClickZone} | GANHOU");
                totalPlantas+=1;
                pontos +=1;
                _pointText.text = pontos.ToString();
                RemovePlant(plant);
                ControlNumberOfPlants();
            }
            else
            {
                print($"clicou {clickTime} | certo {_correctClickZone} | ERROU");
            }
        }

        runningAnimation = false;
    }

    private (float,float) PositionPlant()
    {
        float xRandom = Random.Range(40f, 150f); //Onde o mato vai spawnar
        _correctClickZone = xRandom;
        return (xRandom  * 3.625f, _ZoneImage.rectTransform.anchoredPosition[1]);
    }

    private void CreatePlant(int quantity)
    {
        for (int i=0; i<quantity; i++)
        {
            var newObj = GameObject.Instantiate(_ZoneImage);
            newObj.transform.SetParent(testeParent, false);
            var cords = PositionPlant();
            newObj.rectTransform.anchoredPosition = new Vector2(cords.Item1, cords.Item2);
            animPlant = newObj.GetComponent<Animator>();
            animPlant.SetInteger("animacao", Random.Range(1,4));
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
        if(pontos < 3)
        {
            CreatePlant(2);
        }
        else if(pontos < 7)
        {
            CreatePlant(3);
        }
        else if(pontos < 11)
        {
            CreatePlant(5);
        } 
    }

}
