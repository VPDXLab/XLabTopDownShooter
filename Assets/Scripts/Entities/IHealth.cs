namespace Entities
{
    public interface IHealth
    {
        public void Heal(float heal);
        
        public void TakeDamage(float damage);
    }
}