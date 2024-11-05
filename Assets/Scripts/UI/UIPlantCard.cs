using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlantCard : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private Image maskImg;

    [SerializeField]private float CDDuration = 3f;
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
        maskImg=transform.Find("Mask").GetComponent<Image>();
        maskImg.fillAmount = 1;
        IsReady = false;
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
        //if use this method need to add a boxcollider to this card
        if (!isReady) return;
        Debug.Log(11);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(222);
    }
}
