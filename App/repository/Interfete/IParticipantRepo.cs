using App.domain;

namespace App.repository
{
    public interface IParticipantRepo : IRepository<Participant, int>
    {
        Participant GetParticipantByName(String name);
        List<Participant> GetParticipantsByAgeCategory(int ageMin, int ageMax);
        List<Participant> GetParticipantsByProba(int idProba);

        Participant GetParticipantByCNP(string cnp);

    }
}