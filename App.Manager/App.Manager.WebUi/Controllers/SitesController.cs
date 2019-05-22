using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Manager.BLL.Interfaces;
using App.Manager.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Manager.WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {

        private readonly ISiteBusiness _siteBusiness;

        public SitesController(ISiteBusiness siteBusiness)
        {
            _siteBusiness = siteBusiness;
        }


        [HttpGet]
        public ActionResult<List<string>> GetServers()
        {
            List<SiteDTO> sites;
            try
            {
                sites = _siteBusiness.GetSites();

                if (sites == null)
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