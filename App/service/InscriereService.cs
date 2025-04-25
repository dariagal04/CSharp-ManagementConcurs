using App.domain;
using App.repository;

namespace App.sevice;

public class InscriereService
{
    private readonly IInscriereRepo _inscriereRepo;
    public InscriereService(IInscriereRepo inscriereRepo)
    {
        _inscriereRepo = inscriereRepo;
    }
    

    public bool SaveEntity(Inscriere inscriere) {
        return _inscriereRepo.SaveEntity(inscriere);
    }

    public int CountByProbaAndCategorie(object id, object o)
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, Dictionary<string, int>> GetCompetitionsWithParticipants()
    {
        return _inscriereRepo.GetCompetitionsWithParticipants();
    }

    public Inscriere GetInscriereByParticipantAndProbaAndCategorie(int idParticipant, int idProba, int idCategorie)
    {
        return _inscriereRepo.GetInscriereByParticipantAndProbaAndCategorie(idParticipant, idProba, idCategorie);
    }

}