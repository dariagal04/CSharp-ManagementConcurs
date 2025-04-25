using System.Collections;
using App.domain;
using App.repository;

namespace App.sevice;

public class CategorieService
{
    private readonly ICategorieVarstaRepo _categorieVarstaRepo;
    public CategorieService(ICategorieVarstaRepo categorieRepo)
    {
        _categorieVarstaRepo = categorieRepo;
    }

    public CategorieVarsta GetCategorie(int id) {
        return _categorieVarstaRepo.GetOne(id);
    }
    
    public CategorieVarsta GetCategorieVarstaByAgeGroup(int minAge, int maxAge)
    {
        return _categorieVarstaRepo.GetCategorieVarstaByAgeGroup(minAge, maxAge);
    }

    public CategorieVarsta GetCategorieVarstaByAge(int varsta)
    {
        return _categorieVarstaRepo.GetOne(varsta);
    }


    public List< CategorieVarsta> FindAll()
    {
        return _categorieVarstaRepo.GetAll();
    }
}