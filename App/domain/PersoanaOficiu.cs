
namespace App.domain{
    public class PersoanaOficiu : Entitate<int>
    {
        public string username { get; }        

        public string password { get; }

		public string locatie_oficiu { get; }

        public PersoanaOficiu(int id, string username, string password, string locatie_oficiu) : base(id)
        {
            this.username = username;
            this.password = password;
			this.locatie_oficiu = locatie_oficiu;
        }

        public override string ToString()
        {
            return username + " "+ locatie_oficiu;
        }
    }
}