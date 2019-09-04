using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InfraAsApi_Rest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InfraAsApi_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfrastructureController : ControllerBase
    {
        // GET: api/Infrastructure
        [HttpGet]
        public IActionResult Get()
        {
            Process[] processList = Process.GetProcesses();
            List<ProcessPerformanceCounter> processes = new List<ProcessPerformanceCounter>();
            foreach (Process process in processList)
            {
                if (process.ProcessName != "Idle")
                {
                    processes.Add(new ProcessPerformanceCounter(process.ProcessName));
                }
            }
            processes.Sort();
            return Ok(processes.Take(3));
        }

        // GET: api/Infrastructure/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Infrastructure
        [HttpPost]
        public void Post([FromBody] ProcessDefinition process)
        {
            Process[] processList = Process.GetProcessesByName(process.ProcessName);
            foreach(Process p in processList)
            {
                p.Kill();
            }   
        }

        // PUT: api/Infrastructure/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProcessDefinition process)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}