using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;
using App.Manager.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Manager.WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly IServerBusiness _serverBusiness;

        public ServersController(IServerBusiness serverBusiness)
        {
            _serverBusiness = serverBusiness;
        }

        [HttpGet]
        public ActionResult<List<string>> GetServers()
        {
            List<string> sites;
            try
            {
                sites = _serverBusiness.GetSites();

                if(sites == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(sites);
        }
    }
}