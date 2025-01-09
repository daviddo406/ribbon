using CurseForge.APIClient;
using CurseForge.APIClient.Models.Mods;
using Ribbon.State;
using File = CurseForge.APIClient.Models.Files.File;

namespace Ribbon
{
    // API_KEY = "$2a$10$96h3LX0zd4NF9fTroK0Du.06R.mear0mOhpN.ax9B.8DUH.JM6A4K"
    // MINECRAFT_ID = 432

    public class ModRepository
    {
        private ApiClient _apiClient;
        
        private readonly StateProvider _stateProvider;

        public ModRepository(StateProvider stateProvider)
        {
            _apiClient = new ApiClient("$2a$10$96h3LX0zd4NF9fTroK0Du.06R.mear0mOhpN.ax9B.8DUH.JM6A4K");
            _stateProvider = stateProvider;
        }
        
        public List<Mod>? SearchMods(int index)
        {
            var req = _apiClient.SearchModsAsync(432, gameVersion: _stateProvider.Options.GameVersion, index: index);
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
            var req = _apiClient.SearchModsAsync(432, searchFilter: name, gameVersion: _stateProvider.Options.GameVersion);
            req.Wait();
            return req.Result.Data.FirstOrDefault();
        }

        public List<File> GetModFiles(int id, int index)
        {
            var req = _apiClient.GetModFilesAsync(id, index: index, modLoaderType: _stateProvider.Options.ModLoaderType, gameVersion: _stateProvider.Options.GameVersion);
            req.Wait();
            return req.Result.Data;
        }
        
        public string GetModFileDownloadUrl(Mod mod, int fileId)
        {
            var req = _apiClient.GetModFileDownloadUrlAsync(mod.Id, fileId);
            req.Wait();
            return req.Result.Data;
        }
    }
}
