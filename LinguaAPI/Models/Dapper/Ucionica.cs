using LinguaAPI.Repositories.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Models.Dapper
{
    public partial class Ucionica : BaseEntity
    {
        public Ucionica()
        {
        }

        public string Naziv { get; set; }
        public string Broj { get; set; }
    }
}
