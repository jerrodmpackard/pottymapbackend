using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pottymapbackend.Models;
using pottymapbackend.Services;

namespace pottymapbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _data;
        public ReportController(ReportService data)
        {
            _data = data;
        }


        // Add New Report
        [HttpPost]
        [Route("AddNewReport")]
        public bool AddNewReport(ReportModel newReport)
        {
            return _data.AddNewReport(newReport);
        }


        // Get all Reports
        [HttpGet]
        [Route("GetAllReports")]
        public IEnumerable<ReportModel> GetAllReports()
        {
            return _data.GetAllReports();
        }


        // Get all reports by a single user
        [HttpGet]
        [Route("GetReportsByUserId/{userId}")]
        public IEnumerable<ReportModel> GetReportsByUserId(int userId)
        {
            return _data.GetReportsByUserId(userId);
        }


        // Get unresolved reports
        [HttpGet]
        [Route("GetUnresolvedReports")]
        public IEnumerable<ReportModel> GetUnresolvedReports()
        {
            return _data.GetUnresolvedReports();
        }


        // Get report by its own ID
        [HttpGet]
        [Route("GetReportsById/{id}")]
        public ReportModel GetReportsById(int id)
        {
            return _data.GetReportsById(id);
        }


        // Update Report
        [HttpPut]
        [Route("UpdateReport")]
        public bool UpdateReport(ReportModel reportToUpdate)
        {
            return _data.UpdateReport(reportToUpdate);
        }


        // Resolve Report
        [HttpDelete]
        [Route("ResolveReport")]
        public bool ResolveReport(ReportModel reportToResolve)
        {
            return _data.ResolveReport(reportToResolve);
        }
    }
}