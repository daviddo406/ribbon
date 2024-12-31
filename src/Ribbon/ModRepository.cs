using CurseForge.APIClient;
using CurseForge.APIClient.Models;
using CurseForge.APIClient.Models.Mods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ribbon
{
    // API_KEY = "$2a$10$96h3LX0zd4NF9fTroK0Du.06R.mear0mOhpN.ax9B.8DUH.JM6A4K"
    // MINECRAFT_ID = 432

    public class ModRepository
    {
        private ApiClient _apiClient;

        public ModRepository()
        {
            _apiClient = new ApiClient("$2a$10$96h3LX0zd4NF9fTroK0Du.06R.mear0mOhpN.ax9B.8DUH.JM6A4K");
        }
        
        public List<Mod>? SearchMods(int index)
        {
            var req = _apiClient.SearchModsAsync(432, index: index);
            req.Wait();
            return req.Result.Data;
        }

        public Mod? GetModById(int id)
        {
            var req = _apiClient.GetModAsync(id);
            req.Wait();
            return req.Result.Data;
        }

        public Mod? GetModByName(string name)
        {
            var req = _apiClient.SearchModsAsync(432, searchFilter: name);
            req.Wait();
            return req.Result.Data.FirstOrDefault();
        }

        public string GetModFileDownloadUrl(Mod mod, int fileId)
        {
            var req = _apiClient.GetModFileDownloadUrlAsync(mod.Id, fileId);
            req.Wait();
            return req.Result.Data;
        }
    }
}
