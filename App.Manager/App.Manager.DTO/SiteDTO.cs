using System;
using System.Collections.Generic;
using System.Text;

namespace App.Manager.DTO
{
    public class SiteDTO
    {
        public long Identity { get; set; }

        public string Name { get; set; }

        public string PhysicalPath { get; set; }

        public ServerStatus ServerStatus { get; set; }
    }
}
