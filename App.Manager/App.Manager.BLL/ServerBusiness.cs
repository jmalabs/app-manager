using App.Manager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;

namespace App.Manager.BLL
{
    public class ServerBusiness : IServerBusiness
    {
        private DirectoryEntry websiteEntry = null;
        internal const string IIsWebServer = "IIsWebServer";

        public List<string> GetSites()
        {
            DirectoryEntry server = null;
            var services = new DirectoryEntry("IIS://localhost/W3SVC");
            var enumerator = services.Children.GetEnumerator();
            var sites = new List<string>();

            while (enumerator.MoveNext())
            {
                server = (DirectoryEntry)enumerator.Current;
                if (server.SchemaClassName == IIsWebServer)
                {
                    sites.Add(server.Properties["ServerComment"][0].ToString());
                }
            }

            return sites;
        }
    }
}
