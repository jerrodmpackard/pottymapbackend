using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pottymapbackend.Models;
using pottymapbackend.Services.Context;

namespace pottymapbackend.Services
{
    public class ReportService
    {
        private readonly DataContext _context;
        public ReportService(DataContext context)
        {
            _context = context;
        }

        public bool AddNewReport(ReportModel newReport)
        {
            _context.Add(newReport);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<ReportModel> GetAllReports()
        {
            return _context.ReportInfo;
        }

        public IEnumerable<ReportModel> GetReportsByUserId(int userId)
        {
            return _context.ReportInfo.Where(item => item.UserId == userId);
        }

        public IEnumerable<ReportModel> GetUnresolvedReports()
        {
            return _context.ReportInfo.Where(item => item.IsResolved == false);
        }

        public ReportModel GetReportsById(int id)
        {
            return _context.ReportInfo.SingleOrDefault(item => item.ID == id);
        }

        public bool UpdateReport(ReportModel reportToUpdate)
        {
            _context.Update<ReportModel>(reportToUpdate);
            return _context.SaveChanges() != 0;
        }


        public bool ResolveReport(ReportModel reportToResolve)
        {
            reportToResolve.IsResolved = true;
            _context.Update<ReportModel>(reportToResolve);
            return _context.SaveChanges() != 0;
        }
    }
}