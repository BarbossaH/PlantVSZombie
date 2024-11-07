using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlantCard : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IObserver
{
    //private Image cardIcon;
    private Image maskImg;

    [SerializeField]private float CDDuration = 3f;
    [SerializeField]private PlantTypeEnum currentPlantType;
    private float currentTime = 0;

    private bool isReady = false;
    public bool IsReady {  get { return isReady; }
        set
        {
            isReady = value;
            if (isReady)
            {
                maskImg.fillAmount = 0;
            }
            else
            {
                //start cd
                maskImg.fillAmount = 1;
                StartCD(CDDuration);
            }
        }
    }

    private void Start()
    {
        //cardIcon = GetComponent<Image>();
      
        maskImg=transform.Find("Mask").GetComponent<Image>();
        maskImg.fillAmount = 1;
        IsReady = false;
    }

    private void Update()
    {
  
    }

    private void OnEnable()
    {
        NotificationCenter.Instance.RegisterObserver(this);
    }
    private void OnDisable()
    {
        NotificationCenter.Instance.UnregisterObserver(this);
    }

    private void StartCD(float duration)
    {
        StartCoroutine(StartCDCoroutine( duration));
    }

    private IEnumerator StartCDCoroutine(float duration)
    {
       
        while (currentTime <= duration)
        {
            maskImg.fillAmount =1- currentTime / duration;
            yield return null;
            currentTime += Time.deltaTime;
        }
        currentTime = 0;
        IsReady = true;
    }
    //mouse enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector2(1.05f, 1.05f);
    }

    //mouse out
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector2(1.0f, 1.0f);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectPlant();
        }
    }
    public void SelectPlant()
    {
        if (!isReady || MouseManager.Instance.currentPlantType!=PlantTypeEnum.None) return;
        //change the state of mouse manager
        MouseManager.Instance.SetMouseSelected(currentPlantType);
    }

    public void OnDataChanged(Event_Type type, object eventData = null)
    {
        //todo refresh the card cd
        if (type == Event_Type.Planting_Event && (PlantTypeEnum)eventData==PlantTypeEnum.SunFlower)
        {
            IsReady = false;
        }
    }
}
