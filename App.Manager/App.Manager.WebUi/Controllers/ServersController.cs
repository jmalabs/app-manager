using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Manager.WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private DirectoryEntry websiteEntry = null;
        internal const string IIsWebServer = "IIsWebServer";

        [HttpGet]
        public List<string> GetServers()
        {

            DirectoryEntry Services = new DirectoryEntry("IIS://localhost/W3SVC");
            IEnumerator ie = Services.Children.GetEnumerator();
            DirectoryEntry Server = null;
            List<string> sites = new List<string>();

            // find iis website
            while (ie.MoveNext())
            {
                Server = (DirectoryEntry)ie.Current;
                if (Server.SchemaClassName == IIsWebServer)
                {
                    //// "ServerComment" means name
                    //if (Server.Properties["ServerComment"][0].ToString() == name)
                    //{
                    sites.Add(Server.Properties["ServerComment"][0].ToString());
                    //}
                }
            }

            return sites;
        }
    }
}