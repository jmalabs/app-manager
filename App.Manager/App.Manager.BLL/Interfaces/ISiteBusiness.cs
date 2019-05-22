using App.Manager.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Manager.BLL.Interfaces
{
    public interface ISiteBusiness
    {
        List<SiteDTO> GetSites(string domain);
        List<string> GetApplicationPools(string domain);
        bool StopSite(string siteName);
        bool StartSite(string siteName);
    }
}
