using LinguaAPI.Repositories.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Models.Dapper
{
    public class TipNastave : BaseEntity
    {
        public TipNastave()
        {
        }

        public string Naziv { get; set; }
        public string Opis { get; set; }
    }
}
