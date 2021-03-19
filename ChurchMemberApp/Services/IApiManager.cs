using ChurchMemberApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChurchMemberApp.Services
{
    public interface IApiManager
    {
        Task<IEnumerable<Feeds>> GetAllChurchFeeds(string TenantId, string userId = "");
    }
}
