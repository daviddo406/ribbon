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

        public ModRepository(string token)
        {
            _apiClient = new ApiClient(token);
        }
        
        public async Task<DataTable> SearchMods(int index)
        {
            GenericListResponse<Mod> res = await _apiClient.SearchModsAsync(432, index: index);

            DataTable table = new DataTable();
            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("Desc");
            foreach (Mod mod in res.Data)
            {
                table.Rows.Add(mod.Id, mod.Name, mod.Summary);
            }

            return table;
        }
    }
}
