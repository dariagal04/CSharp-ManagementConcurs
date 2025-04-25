using App.domain;

namespace App.repository

{
    public interface INumeProbaRepo : IRepository<NumeProba, int>
    {
        NumeProba? GetNumeProbaByName(String name);
    }
}