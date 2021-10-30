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
    private int plantsToSpawn = 0;
    private AudioSource audioData;
    public Transform testeParent;
    public GameObject parentt;
    private bool runningAnimation = false;
    private float my_time = 0f;
    private float _correctClickZone;
    private float _sliderValue;
    public Animator anim;
    private List<Image> plants = new List<Image>();
    private Animator animPlant;
    public MusicController musicController; 
    public DialogueUI dialogueUI;
    public DialogueObject dialogueObject;
    private bool movimentation = true;
    void Start()
    {

        audioData = audioData = GetComponent<AudioSource>();
        StartCoroutine(CreatePlant(1));
        _slider.onValueChanged.AddListener((v) =>{
            _sliderValue = v;
        });
        StartCoroutine(UpdateSlider());
        StartCoroutine(MyTime());
    }

    void Update()
    {
        if(Input.GetKeyDown("space") && !runningAnimation && _working)
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
        audioData.PlayDelayed(0.6f);
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.3f);
        
        print("Contage tamanho: " +plants.Count);
        foreach(var plant in plants)
        {   
            _correctClickZone = plant.rectTransform.anchoredPosition[0] / 3.625f;
            print("Clickzone: "+ _correctClickZone);
            if(clickTime >= _correctClickZone-5f && clickTime <= _correctClickZone+5f)
            {
                print($"clicou {clickTime} | certo {_correctClickZone} | GANHOU");
                totalPlantas+=1;
                pontos +=1;
                _pointText.text = pontos.ToString();
                RemovePlant(plant);
                if(movimentation) ControlNumberOfPlants();
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
        float xRandom = Random.Range(5f, 155f); //Onde o mato vai spawnar
        _correctClickZone = xRandom;
        return (xRandom  * 3.625f, _ZoneImage.rectTransform.anchoredPosition[1]);
    }

    IEnumerator CreatePlant(int quantity)
    {
        for (int i=0; i<quantity; i++)
        {
            if(_working){
                var newObj = GameObject.Instantiate(_ZoneImage);
                newObj.transform.SetParent(testeParent, false);
                var cords = PositionPlant();
                newObj.rectTransform.anchoredPosition = new Vector2(cords.Item1, cords.Item2);
                animPlant = newObj.GetComponent<Animator>();
                animPlant.SetInteger("animacao", Random.Range(1,4));
                plants.Add(newObj);
            }
            if(quantity < 8)
            {
                yield return new WaitForSeconds(Random.Range(3f, 6f));
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(0.03f, 0.7f));;
            }
            
        }
    }

    private void RemovePlant(Image image)
    {
        plants.Remove(image);
        Destroy(image.gameObject);
    }

    private void ControlNumberOfPlants() //Função responsável em criar mais plantas com o tempo, não está pronta
    {

        plantsToSpawn = 0;
        if(pontos == 1)
        {
            plantsToSpawn = 2;
        }
        else if(pontos == 3)
        {
            plantsToSpawn = 8;
        }
        else if(pontos == 11)
        {
            plantsToSpawn = 12;
        }
        else if(pontos == 23)
        {
            plantsToSpawn = 7;
        }
        else if(pontos == 30)
        {
            StartCoroutine(perdeu());
            return;
        }
        

        StartCoroutine(CreatePlant(plantsToSpawn));
    }
    IEnumerator perdeu()
    {  
        movimentation = false;
        print("ativou");
        musicController.Stop();
        StartCoroutine(CreatePlant(30));
        yield return new WaitForSeconds(14f);
        _working = false;

        yield return new WaitForSeconds(3f);
        dialogueUI.ShowDialogue(dialogueObject);
                    //?
    }

}