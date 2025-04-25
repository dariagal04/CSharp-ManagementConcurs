
namespace App.domain
{
    public class NumeProba : Entitate<int>
    {
        public string numeProba { get; }
        public NumeProba(int id, String numeProba) : base(id)
        {
            this.numeProba = numeProba;
        }
        public string ToString()
        {
            return numeProba;
        }
    }
}