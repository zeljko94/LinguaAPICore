using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinguaAPI.Repositories.Dapper
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? DatumKreiranja { get; set; }
        public DateTime? DatumAzuriranja { get; set; }
        public DateTime? DatumBrisanja { get; set; }
    }
}