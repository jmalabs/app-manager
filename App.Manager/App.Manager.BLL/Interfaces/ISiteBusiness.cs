using App.Manager.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Manager.BLL.Interfaces
{
    public interface ISiteBusiness
    {
        List<SiteDTO> GetSites();
    }
}
