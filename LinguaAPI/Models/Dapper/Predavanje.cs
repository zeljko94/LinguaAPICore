using LinguaAPI.Repositories.Dapper;
using System;

namespace LinguaAPI.Models.Dapper
{
    public class Predavanje : BaseEntity
    {
        public string Opis { get; set; }
        public int? PredmetId { get; set; }
        public int? UcionicaId { get; set; }
        public int? ProfesorId { get; set; }
        public int? TipNastaveId { get; set; }
        public DateTime? DatumPocetka { get; set; }
        public DateTime? DatumZavrsetka { get; set; }
    }
}
