using CurseForge.APIClient;
using CurseForge.APIClient.Models;
using CurseForge.APIClient.Models.Mods;
using System;
using System.Collections.Generic;
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

        public ModRepository(string token)
        {
            _apiClient = new ApiClient(token);
        }

        public async Task<GenericListResponse<Mod>> SearchMods()
        {
            return await _apiClient.SearchModsAsync(432);
        }
    }
}
