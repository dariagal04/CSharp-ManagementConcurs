using App.domain;

namespace App.repository
{
    public interface ICategorieVarstaRepo: IRepository<CategorieVarsta, int>
    {
        CategorieVarsta? GetCategorieVarstaByAgeGroup(int minAge, int maxAge);
    }
}