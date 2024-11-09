namespace UI
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using Managers;
    public class UIPlantCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IObserver
    {
        //private Image cardIcon;
        private Image maskImg;

        [SerializeField] private float cdDuration = 3f;
        [SerializeField] private PlantTypeEnum currentPlantType;
        private float currentTime;

        private bool isReady;

        public bool IsReady
        {
            get=> isReady;
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
                    StartCd(cdDuration);
                }
            }
        }

        private void Start()
        {
            //cardIcon = GetComponent<Image>();

            maskImg = transform.Find("Mask").GetComponent<Image>();
            maskImg.fillAmount = 1;
            IsReady = false;

            //cannot put it in onEnable because OnEnable could be executed before awake
            NotificationCenter.Instance.RegisterObserver(this);

        }

        private void OnDestroy()
        {
            NotificationCenter.Instance.UnregisterObserver(this);
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

        private void SelectPlant()
        {
            if (!isReady || MouseManager.Instance.currentPlantType != PlantTypeEnum.None) return;
            //change the state of mouse manager
            MouseManager.Instance.SetMouseSelected(currentPlantType);
        }

        public void OnDataChanged(Event_Type type, object eventData = null)
        {
            //todo refresh the card cd
            if (type == Event_Type.Planting_Event && eventData!=null && (PlantTypeEnum)eventData == currentPlantType)
            {
                IsReady = false;
            }
        }
    }
}
