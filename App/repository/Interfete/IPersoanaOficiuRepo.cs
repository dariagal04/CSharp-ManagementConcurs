using App.domain;

namespace App.repository
{
    public interface IPersoanaOficiuRepo : IRepository<PersoanaOficiu, int>
    {
        PersoanaOficiu? GetOneByUsername(string username);
    }
}