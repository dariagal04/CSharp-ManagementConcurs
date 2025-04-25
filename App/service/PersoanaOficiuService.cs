using App.domain;
using App.repository;

namespace App.sevice;

public class PersoanaOficiuService
{
    private readonly IPersoanaOficiuRepo _persoanaOficiuRepo;
    public PersoanaOficiuService(IPersoanaOficiuRepo persoanaOficiuRepo)
    {
        _persoanaOficiuRepo = persoanaOficiuRepo;
    }
    
    public PersoanaOficiu authenticate(String username, String password) {

        PersoanaOficiu persoanaOficiu = _persoanaOficiuRepo.GetOneByUsername(username);
        if (persoanaOficiu != null && persoanaOficiu.password.Equals(password)) {
            return persoanaOficiu;
        }
        return null;
    }
}