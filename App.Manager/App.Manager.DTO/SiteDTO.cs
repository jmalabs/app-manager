using System;
using System.Collections.Generic;
using System.Text;

namespace App.Manager.DTO
{
    public class SiteDTO
    {
        public SiteDTO()
        {
            Applications = new List<ApplicationSiteDTO>();
        }
        public long Identity { get; set; }

        public string Name { get; set; }

        public string PhysicalPath { get; set; }
        public string AppPoolName { get; set; }

        public ServerStatus ServerStatus { get; set; }

        public List<ApplicationSiteDTO> Applications { get; set; }
    }
}
