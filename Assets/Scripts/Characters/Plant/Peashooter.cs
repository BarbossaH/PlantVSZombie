using Conf;

namespace Characters.Plant
{
   public class Peashooter : PlantBase
    {
        public override float MaxHealth { get; set; }
        public override float CurrentHealth { get; set; }
        public override float CdDuration { get; set; }

        protected override void Awake()
        {
            base.Awake();
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
            CurrentHealth = 0.5f;
        }

        public override void ActivatePlantFunction()
        {
            Shoot();
        }
        private void Shoot(){}

    }
}