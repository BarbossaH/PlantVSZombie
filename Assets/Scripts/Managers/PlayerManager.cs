using Conf;
using UI;
namespace Managers
{
    public class PlayerManager : SingletonMono<PlayerManager>
    {
        private int sunAmount;

        public int SunAmount
        {
            get=>sunAmount;
            set
            {
                sunAmount = value;
                UICardGroup.Instance.UpdateSunAmount(sunAmount);
                NotificationCenter.Instance.NotifyObserver(EventTypeEnum.UpdateSumAmount, sunAmount);
            }
        }

        private void Start()
        {
            SunAmount = 0;
        }
    }
}