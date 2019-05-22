using App.Manager.BLL.Interfaces;
using App.Manager.DTO;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;

namespace App.Manager.BLL
{
    public class SiteBusiness : ISiteBusiness
    {
        private DirectoryEntry websiteEntry = null;
        internal const string IIsWebServer = "IIsWebServer";

        public List<SiteDTO> GetSites()
        {
            DirectoryEntry server = null;
            var services = new DirectoryEntry("IIS://localhost/W3SVC");
            var enumerator = services.Children.GetEnumerator();
            var sites = new List<SiteDTO>();

            while (enumerator.MoveNext())
            {
                server = (DirectoryEntry)enumerator.Current;
                if (server.SchemaClassName == IIsWebServer)
                {
                    sites.Add(new SiteDTO()
                    {
                        Name = server.Properties["ServerComment"][0].ToString(),
                        Identity = Convert.ToInt32(server.Name),
                        PhysicalPath = GetPath(server),
                        ServerStatus = GetSiteStatus(server)

                    });
                }
            }

            return sites;
        }

        static string GetPath(DirectoryEntry server)
        {
            foreach (DirectoryEntry IIsEntity in server.Children)
            {
                if (IIsEntity.SchemaClassName == "IIsWebVirtualDir")
                    return IIsEntity.Properties["Path"].Value.ToString();
            }
            return null;
        }

        static ServerStatus GetSiteStatus(DirectoryEntry server)
        {
            var state = Convert.ToInt32(server.Properties["ServerState"].Value);

            return ServerStatus.GetStatus(state);
        }
    }
}
