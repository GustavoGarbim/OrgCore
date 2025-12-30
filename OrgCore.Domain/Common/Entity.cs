namespace OrgCore.Domain.Common
{
    public class Entity
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; } = DateTime.UtcNow.ToLocalTime();

        protected Entity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow.ToLocalTime();
        }
    }
}
