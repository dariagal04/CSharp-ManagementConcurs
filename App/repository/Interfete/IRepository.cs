
namespace App.repository
{
    public interface IRepository<EntityType, EntityId> where EntityType : class
    {
        List<EntityType>? GetAll();
        EntityType? GetOne(EntityId entityId);

        Boolean SaveEntity(EntityType entityType);
        Boolean DeleteEntity(EntityId entityId);

        Boolean UpdateEntity(EntityType entityType);    }
}