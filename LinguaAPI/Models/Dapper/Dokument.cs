using LinguaAPI.Repositories.Dapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinguaAPI.Models.Dapper
{
    public enum DokumentTypeEnum
    {
        ProfilnaSlika = 1,
        Dokument      = 2
    }

    public class Dokument : BaseEntity
    {
        [NotMapped]
        public byte[] Bytes { get; set; }
        public DokumentTypeEnum DokumentType { get; set; }
        public string Guid { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public bool IsDirectory { get; set; }
        public int UserId { get; set; }

    }
}
