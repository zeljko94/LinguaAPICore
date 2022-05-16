using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Models.DTOs
{
    public class PredavanjeSudionikDTO : BaseEntity
    {

        public int? UserId { get; set; }
        public int? PredavanjeId { get; set; }
        public short? IsPredavac { get; set; }
        public User User { get; set; }
        public Predavanje Predavanje { get; set; }
    }
}
