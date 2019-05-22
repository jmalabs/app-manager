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
        public ActionResult<List<SiteDTO>> GetSites()
        {
            List<SiteDTO> sites;
            try
            {
                sites = _siteBusiness.GetSites("localhost");

                if (sites == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(sites);
        }

        [HttpGet("application-pools")]
        public ActionResult<List<string>> GetApplicationPools()
        {
            List<string> applicationPools;
            try
            {
                applicationPools = _siteBusiness.GetApplicationPools("localhost");

                if (applicationPools == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(applicationPools);
        }

        [HttpGet("{siteName}/stop")]
        public ActionResult<bool> StopSite(string siteName)
        {
            try
            {
                _siteBusiness.StopSite(siteName);

                return Ok(true);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{siteName}/start")]
        public ActionResult<bool> StartSite(string siteName)
        {
            try
            {
                _siteBusiness.StartSite(siteName);

                return Ok(true);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}