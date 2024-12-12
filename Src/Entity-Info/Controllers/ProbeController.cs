using EntityInfoService.DAL.ElasticSearch;
using EntityInfoService.DAL.MySql;
using EntityInfoService.Models.OpusBackend;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EntityInfoService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [Route("api/v1")]
    [ApiController]
    public class ProbeController : ControllerBase
    {
        static Serilog.ILogger _logger = Log.Logger.ForContext(typeof(ProbeController));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("probe/live")]
        public IActionResult GetLiveStatus()
        {
            return Ok(new { status = "OK" });
        }

        [HttpGet("probe/ready")]
        public IActionResult GetReadyStatus()
        {
            return Ok(new { status = "OK" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("status/elasticsearch")]
        public IActionResult GetElasticsearchConnectionStatus()
        {
            var esClient = ElasticSearchHelper.ElasticsearchClient;
            try
            {
                var response = esClient.GetAsync("/").Result;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return Ok(new { message = "Issue connecting elasticsearch" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("status/cassandra")]
        public IActionResult GetCassandraConnectionStatus()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("status/mysql")]
        public IActionResult GetMySqlConnectionStatus()
        {
            var dbResult = new DbResultList<string>();
            dbResult = AuthenticationDB.GetConnectionStatus();
            return Ok(dbResult);
        }
    }
}
