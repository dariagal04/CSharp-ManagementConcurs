

namespace App.domain
{
    public class Inscriere : Entitate<string>
    {
        public int IdParticipant { get; }
        public int IdProba { get; }
        
        public int idCategorie { get; }
        public Inscriere(int idParticipant, int idProba, int idCategorie):base(idParticipant.ToString() + "-" + idProba.ToString())
        {

            this.IdParticipant = idParticipant;
            this.IdProba = idProba;
            this.idCategorie = idCategorie;
        }

        public override string ToString()
        {
            return IdParticipant.ToString() + "-" + IdProba.ToString() + "-" + idCategorie.ToString();
        }
    }
}