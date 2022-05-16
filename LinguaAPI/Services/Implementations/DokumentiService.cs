using LinguaAPI.Models;
using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper.Interfaces;
using LinguaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Services.Implementations
{
    public class DokumentiService : IDokumentiService
    {
        private readonly IDokumentiRepository _dokumentiRepository;
        private readonly ILogger<DokumentiService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DokumentiService(IDokumentiRepository dokumentiRepository,
            ILogger<DokumentiService> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _dokumentiRepository = dokumentiRepository;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var fromDb = await _dokumentiRepository.GetById(id);
            if(fromDb != null)
            {
                var deleted = await _dokumentiRepository.Delete(id);
                if(deleted)
                {
                    var deletedFile = await DeleteFile(fromDb);
                    if(deletedFile)
                    {
                        return new OkResponse<bool>("Dokument uspješno obrisan!", true);
                    }
                    else
                    {
                        return new ErrorResponse<bool>("Greška prilikom brisanja dokumenta sa servera!", false);
                    }
                }
                else
                {
                    return new ErrorResponse<bool>("Greška prilikom brisanja dokumenta iz baze!", false);
                }
            }
            else
            {
                return new OkResponse<bool>("Dokument nije pronađen u bazi!", true);
            }
        }

        public async Task<ApiResponse<List<Dokument>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<Dokument>> GetById(int id)
        {
            var fromDb = await _dokumentiRepository.GetById(id);
            if(fromDb != null)
            {
                fromDb.Bytes = await GetFile(fromDb.Path + "\\" + fromDb.Guid + "_" + fromDb.Filename);
                return new OkResponse<Dokument>("Uspješan dohvat!", fromDb);
            }
            else
            {
                return new ErrorResponse<Dokument>("Greška prilikom dohvaćanja dokumenta iz baze!", null);
            }
        }

        public async Task<ApiResponse<int>> Insert(Dokument entity)
        {
            var inserted = await _dokumentiRepository.Insert(entity);
            if(inserted > 0)
            {
                await CreateFile(entity);
                return new OkResponse<int>("Uspješno spremljeno!", inserted);
            }
            return new ErrorResponse<int>("Greška prilikom spremanja datoteke!", 0);
        }

        public async Task<ApiResponse<bool>> Update(Dokument entity)
        {
            throw new NotImplementedException();
        }



        #region HELPERS
        private readonly string PROFILE_IMAGES_PATH = "/ProfilneSlike";
        private readonly string DOKUMENTI_PATH      = "/Dokumenti";
        private async Task<bool> CreateFile(Dokument dokument)
        {
            try
            {
                var path = GetDokumentRootPathByType(dokument);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                dokument.Guid = Guid.NewGuid().ToString();
                
                if(dokument.IsDirectory)
                {
                    await File.WriteAllBytesAsync(path + "\\" + dokument.Guid + "_" + dokument.Filename, dokument.Bytes);
                }
                else
                {
                    await File.WriteAllBytesAsync(path + "\\" + dokument.Guid + "_" + dokument.Filename, dokument.Bytes);
                }
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        private async Task<bool> DeleteFile(Dokument dokument)
        {
            try
            {
                var path = GetDokumentRootPathByType(dokument);
                if(dokument.IsDirectory)
                {
                    Directory.Delete(path + "\\" + dokument.Guid + "_" + dokument.Filename);
                }
                else
                {
                    File.Delete(path + "\\" + dokument.Guid + "_" + dokument.Filename);
                }
                return await Task.FromResult(true);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(false);
            }
        }

        private async Task<byte[]> GetFile(string path)
        {
            var exists = File.Exists(path);
            return exists ? await File.ReadAllBytesAsync(path) : null;
        }

        private string GetDokumentRootPathByType(Dokument dokument)
        {
            string path = _webHostEnvironment.WebRootPath;

            switch (dokument.DokumentType)
            {
                case DokumentTypeEnum.ProfilnaSlika:
                    return path + "\\" + PROFILE_IMAGES_PATH;

                case DokumentTypeEnum.Dokument:
                    return path + "\\" + DOKUMENTI_PATH;

                default:
                    return null;
            }
        }
        #endregion
    }
}
