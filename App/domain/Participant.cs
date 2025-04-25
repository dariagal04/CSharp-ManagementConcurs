
namespace App.domain
{
    public class Participant : Entitate<int>
    {
        public string Nume { get; }
        public int Varsta { get; }
		public string Cnp { get; }
		public int idPersoanaOficiu { get; }

        public Participant(int id, string nume, int varsta, string cnp, int idPersoanaOficiu) : base(id)
        {
            this.Nume = nume;
            this.Varsta = varsta;
			this.Cnp = cnp;
			this.idPersoanaOficiu = idPersoanaOficiu;
        }

        public override string ToString()
        {
            return Nume + "-" + Varsta + "-" + Cnp;
        }
    }
}