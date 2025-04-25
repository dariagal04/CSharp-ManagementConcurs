
namespace App.domain
{
    public class InscriereProba: Entitate<int>
    {
        public int nrParticipanti { get; }
        public string numeProba { get; }
        public string categorieVarsta { get; }

        public InscriereProba(int id, int nrParticipanti, string numeProba, string categorieVarsta) : base(id)
        {
            this.nrParticipanti = nrParticipanti;
            this.numeProba = numeProba;
            this.categorieVarsta = categorieVarsta;
        }

        public String toString()
        {
            return "InscrieriProba{" +
                   "nrParticipanti=" + nrParticipanti +
                   ", numeProba='" + numeProba + '\'' +
                   ", categorieVarsta='" + categorieVarsta + '\'' +
                   '}';
        }

    }
}