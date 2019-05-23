using App.Manager.BLL.Interfaces;
using App.Manager.DTO;
using Microsoft.Web.Administration;
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

        //public List<SiteDTO> GetSites(string domain = "localhost")
        //{
        //    DirectoryEntry directoryEntry = null;
        //    var directoryEntries = this.GetDirectoryEntry($"IIS://{domain}/W3SVC");
        //    var enumerator = directoryEntries.Children.GetEnumerator();
        //    var sites = new List<SiteDTO>();

        //    while (enumerator.MoveNext())
        //    {
        //        directoryEntry = (DirectoryEntry)enumerator.Current;
        //        if (directoryEntry.SchemaClassName == IIsWebServer)
        //        {
        //            sites.Add(new SiteDTO()
        //            {
        //                Name = directoryEntry.Properties["ServerComment"][0].ToString(),
        //                Identity = Convert.ToInt32(directoryEntry.Name),
        //                PhysicalPath = GetPath(directoryEntry),
        //                ServerStatus = GetSiteStatus(directoryEntry)

        //            });
        //        }
        //    }

        //    return sites;
        //}

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

        /// <summary>
        /// Retrieves an Adsi Node by its path. Abstracted for error handling
        /// </summary>
        /// <param name="Path">the ADSI path to retrieve: IIS://localhost/w3svc/root</param>
        /// <returns>node or null</returns>
        private DirectoryEntry GetDirectoryEntry(string Path)
        {

            DirectoryEntry root = null;
            try
            {
                root = new DirectoryEntry(Path);
            }
            catch
            {
                //this.SetError("Couldn't access node");
                return null;
            }
            if (root == null)
            {
                //this.SetError("Couldn't access node");
                return null;
            }
            return root;
        }

        public List<string> GetApplicationPools(string domain)
        {
            var applicationPools = new List<string>();

            ServerManager manager = new ServerManager();
            foreach (Site site in manager.Sites)
            {
                foreach (Application app in site.Applications)
                {
                    applicationPools.Add(app.ApplicationPoolName);
                }
            }
            return applicationPools;
        }


        public bool StopSite(string siteName)
        {
            var applicationPools = new List<string>();

            ServerManager manager = new ServerManager();
            foreach (Site site in manager.Sites)
            {
                if (site.Name == siteName)
                {
                    site.Stop();
                }
            }
            return true;
        }

        public bool StartSite(string siteName)
        {
            var applicationPools = new List<string>();

            ServerManager manager = new ServerManager();
            foreach (Site site in manager.Sites)
            {
                if (site.Name == siteName)
                {
                    site.Start();
                }
            }
            return true;
        }

        public List<SiteDTO> GetSites(string domain = "localhost")
        {
            var sites = new List<SiteDTO>();
            var manager = new ServerManager();
            foreach (var site in manager.Sites)
            {
                var webSite = new SiteDTO()
                {
                    Name = site.Name,
                    Identity = Convert.ToInt32(site.Id),
                    PhysicalPath = site.Applications["/"].VirtualDirectories["/"].PhysicalPath,
                    ServerStatus = ServerStatus.GetStatus((int)site.State),
                    AppPoolName = site.Applications["/"].ApplicationPoolName
                };

                foreach (var app in site.Applications)
                {
                    if(app.Path == "/")
                    {
                        continue;
                    }

                    var appSite = new ApplicationSiteDTO()
                    {
                        AppPoolName = app.ApplicationPoolName,
                        Name = app.Path,
                        PhysicalPath = app.VirtualDirectories["/"].PhysicalPath,
                    };

                    webSite.Applications.Add(appSite);
                }

                sites.Add(webSite);
            }

            return sites;
        }
    }
}
