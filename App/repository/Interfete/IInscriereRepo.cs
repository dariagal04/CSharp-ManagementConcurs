using App.domain;

namespace App.repository
{
    public interface IInscriereRepo: IRepository<Inscriere, int>
    {
        Inscriere? GetInscriereByParticipantAndProba(int idParticipant, int idProba);
        int GetNumRegistrations(int idProba);
        
        
        List<Inscriere> GetInscrieriByParticipantId(int idParticipant);
        public Dictionary<string, Dictionary<string, int>> GetCompetitionsWithParticipants();

        public Inscriere GetInscriereByParticipantAndProbaAndCategorie(int idParticipant, int idProba, int idCategorie);
    }
    
}