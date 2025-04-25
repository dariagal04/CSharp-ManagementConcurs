using App.domain;
using App.repository;

namespace App.sevice;

public class ParticipantiService
{
    private readonly IParticipantRepo _participantRepo;
    public ParticipantiService(IParticipantRepo participantRepo)
    {
        _participantRepo = participantRepo;
    }
    
    public bool SaveEntity(Participant participant) {
        return _participantRepo.SaveEntity(participant);
    }

    public Participant GetParticipantByNume(String nume) {
        return _participantRepo.GetParticipantByName(nume);
    }

    public List<Participant> GetParticipantByProba(int proba_id)
    {
        return _participantRepo.GetParticipantsByProba(proba_id);
    }

    public Participant GetParticipantByCNP(string cnp)
    {
        return _participantRepo.GetParticipantByCNP(cnp);
    }
}