using System.Collections;
using App.domain;
using App.repository;

namespace App.sevice;

public class NumeProbaService
{
    private readonly INumeProbaRepo _numeProbaRepo;
    public NumeProbaService(INumeProbaRepo numeProbaRepo)
    {
        _numeProbaRepo = numeProbaRepo;
    }
    
    public NumeProba GetNumeProba(String nume){
        return _numeProbaRepo.GetNumeProbaByName(nume);
    }

    public List<NumeProba> FindAll()
    {
        return _numeProbaRepo.GetAll();
    }
}