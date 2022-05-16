using LinguaAPI.Repositories.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Models.Dapper
{
    public class CalendarEvent : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int PredavanjeId { get; set; }
        public DateTime? DatumPocetka { get; set; }
        public DateTime? DatumZavrsetka { get; set; }
    }
}
