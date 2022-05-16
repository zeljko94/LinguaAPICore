using LinguaAPI.Repositories.Dapper;
using System;

namespace LinguaAPI.Models.Dapper
{
    public class Predmet : BaseEntity
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string Razina { get; set; }
    }
}
