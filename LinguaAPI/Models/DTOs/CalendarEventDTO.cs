using LinguaAPI.Repositories.Dapper;
using System;

namespace LinguaAPI.Models.DTOs
{
    public class CalendarEventDTO : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int PredavanjeId { get; set; }
        public DateTime? DatumPocetka { get; set; }
        public DateTime? DatumZavrsetka { get; set; }

        public PredavanjeDTO Predavanje { get; set; }
    }
}
