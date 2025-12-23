namespace Entities
{
    public interface IHealth
    {
        public float value { get; }
        
        public void Heal(float heal);
        
        public void TakeDamage(float damage);
    }
}