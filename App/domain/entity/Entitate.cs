using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App.domain
{
    public class Entitate<IdType>
    {
        public IdType Id { get; set; }

        public Entitate(IdType id)
        {
            this.Id = id;
        }
        
    }
}