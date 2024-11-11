namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamaged(float damage);
        
        void Die();
        
        float MaxHealth { get; set; }
        
        float CurrentHealth { get; set; }
    }
}