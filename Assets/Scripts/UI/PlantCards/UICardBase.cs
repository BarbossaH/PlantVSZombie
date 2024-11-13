using System;
using System.Collections;
using UnityEngine;
using Conf;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Managers;
using Interfaces;
using TMPro;

namespace UI
{
   public abstract class UICardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IDataObserver
   {
      private Image maskImg;
      private Image cardImg;
      private bool isReady;
      private float currentTime;
      protected TextMeshProUGUI sunAmountTMP;

      public abstract float CoolDown { get; protected set; }
      public abstract PlantTypeEnum CurrentPlantType { get; protected set; }
      private bool IsReady
      {
         get => isReady;
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
               StartCd(CoolDown);
            }
         }
      }
      //check the sun amount is enough or not
      private bool IsSunEnough()
      {
         //this cannot be written as a property, because property initialization in c# generally occur before Unity's lifecycle methods like Awake, Start 
         return PlayerManager.Instance.SunAmount >= (int)CurrentPlantType;
      }

      private void Awake()
      {
         cardImg=GetComponent<Image>();
         maskImg = transform.Find("Mask").GetComponent<Image>();
         sunAmountTMP=GetComponentInChildren<TextMeshProUGUI>();
      }

      protected virtual void Start()
      {
         maskImg.fillAmount = 1;
         IsReady = false;
      }

      private void Update()
      {
         if (!IsSunEnough())
         {
            cardImg.color = new Color(0.5f, 0.5f, 0.5f);
         }
         else
         {
            cardImg.color = new Color(1, 1, 1);
         }
      }

      private void StartCd(float duration)
      {
         StartCoroutine(StartCdCoroutine(duration));
      }
      
      private IEnumerator StartCdCoroutine(float duration)
      {

         while (currentTime <= duration)
         {
            maskImg.fillAmount = 1 - currentTime / duration;
            yield return null;
            currentTime += Time.deltaTime;
         }

         currentTime = 0;
         IsReady = true;
      }
      
      private void OnMouseDown()
      {
         if (Input.GetMouseButtonDown(0))
         {
            SelectPlant();
         }
      }
        
      private void SelectPlant()
      {
         //if cd is not ready, or the mouse has held a plant, or the sum amount is not enough,then return
         if (!IsReady 
             || MouseManager.Instance.currentPlantType != PlantTypeEnum.None
             || !IsSunEnough()) return;
         //change the state of mouse manager
         MouseManager.Instance.SetMouseSelected(CurrentPlantType);
      }

      public void OnDataChanged(EventTypeEnum type, object eventData = null)
      {
         //todo refresh the card cd
         if (type == EventTypeEnum.PlantingEvent && eventData!=null && (PlantTypeEnum)eventData == CurrentPlantType)
         {
            IsReady = false;
         }
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
      
   }
}