using Conf;
using Managers;

namespace UI
{
    public class UISunCard : UICardBase
    {

        public override float CoolDown { get; protected set; } = 3.0f;
        public override PlantTypeEnum CurrentPlantType { get; protected set; }
        // [SerializeField] private PlantTypeEnum currentPlantType;
        
        protected override void Start()
        {
            base.Start();
            CurrentPlantType=PlantTypeEnum.SunFlower;
            sunAmountTMP.text = ((int)CurrentPlantType).ToString();

            NotificationCenter.Instance.RegisterObserver(this);
        }

        private void OnDestroy()
        {
            NotificationCenter.Instance.UnregisterObserver(this);
        }
 
    }
}
