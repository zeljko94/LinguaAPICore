using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper;
using System;
using System.Collections.Generic;

namespace LinguaAPI.Models.DTOs
{
    public class PredavanjeDTO : BaseEntity
    {
        public string Opis { get; set; }
        public int? PredmetId { get; set; }
        public int? UcionicaId { get; set; }
        public int? ProfesorId { get; set; }
        public int? TipNastaveId { get; set; }
        public DateTime? DatumPocetka { get; set; }
        public DateTime? DatumZavrsetka { get; set; }


        public Predmet Predmet { get; set; }
        public Ucionica Ucionica { get; set; }
        public User Profesor { get; set; }
        public TipNastave TipNastave { get; set; }
        public List<PredavanjeSudionikDTO> PredavanjeSudionici { get; set; }
        public List<CalendarEventDTO> CalendarEvents { get; set; }

        public PredavanjeDTO()
        {
            Predmet = default(Predmet);
            Ucionica = default(Ucionica);
            Profesor = default(User);
            TipNastave = default(TipNastave);
            PredavanjeSudionici = new List<PredavanjeSudionikDTO>();
        }
    }
}
