using Conf;
using Interfaces;

namespace Managers
{

    using TMPro;
    using UnityEngine;

    public class UICardGroup : MonoBehaviour,IDataObserver
    {
        public static UICardGroup Instance;

        private TextMeshProUGUI sunAmountTMP;

        private void Awake()
        {
            Instance = this;
            sunAmountTMP = transform.Find("MainPanel/SunAmount").GetComponent<TextMeshProUGUI>();
        }


        public void UpdateSunAmount(int amount)
        {
            sunAmountTMP.text = amount.ToString();
        }

        public Vector3 GetSunTMPScreenPos()
        {
            Vector3 screenPos;
            // Get the world coordinates of the sunAmountTMP
            Vector3 worldPos = sunAmountTMP.transform.position;


            //Convert the world coordinates to screen coordinates
            screenPos = Camera.main.WorldToScreenPoint(worldPos);
            //Debug.Log(screenPos);
            screenPos.y += 90f;
            return screenPos;
        }

        public void OnDataChanged(EventTypeEnum type, object eventData = null)
        {
            if (type is EventTypeEnum.UpdateSumAmount && eventData is int sumAmount)
            {
                UpdateSunAmount(sumAmount);
            }
        }
    }
}