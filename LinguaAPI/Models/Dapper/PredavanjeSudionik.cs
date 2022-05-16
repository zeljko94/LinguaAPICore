using LinguaAPI.Repositories.Dapper;

namespace LinguaAPI.Models.Dapper
{
    public class PredavanjeSudionik : BaseEntity
    {
        public int? UserId { get; set; }
        public int? PredavanjeId { get; set; }
        public short? IsPredavac { get; set; }
    }
}
