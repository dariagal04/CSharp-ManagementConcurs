//using concurs_csharp.domain.entity;

namespace App.domain
{
    public class CategorieVarsta : Entitate<int>
    {
        public int VarstaMin { get; }
        public int VarstaMax { get; }

        public CategorieVarsta(int id, int varstaMin, int varstaMax) : base(id)
        {
            this.VarstaMin = varstaMin;
            this.VarstaMax = varstaMax;
        }
        public string ToString()
        {
            return string.Format("{0:d}-{1:d}",VarstaMin,VarstaMax);
        }
    }
}