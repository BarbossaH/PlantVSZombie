using Conf;
using Managers;

namespace UI
{
    public class UISunCard : UICardBase
    {

        public override float CoolDown { get; protected set; } = 3.0f;
        public override PoolTypeEnum CurrentPlantType { get; protected set; }
        // [SerializeField] private PlantTypeEnum currentPlantType;
        
        protected override void Start()
        {
            base.Start();
            CurrentPlantType=PoolTypeEnum.Sunflower;
            sunAmountTMP.text = ((int)CurrentPlantType).ToString();

        }

  
 
    }
}
