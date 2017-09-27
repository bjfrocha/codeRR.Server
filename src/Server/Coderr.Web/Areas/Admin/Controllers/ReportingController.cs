﻿using System.Web.Mvc;
using codeRR.Server.App.Core.Reports.Config;
using codeRR.Server.Infrastructure.Configuration;
using codeRR.Server.Web.Areas.Admin.Models;
using log4net;

namespace codeRR.Server.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ReportingController : Controller
    {
        private ILog _logger = LogManager.GetLogger(typeof(ReportingController));

        [HttpGet]
        public ActionResult Index()
        {
            var model = new ReportingViewModel();
            var settings = ConfigurationStore.Instance.Load<ReportConfig>();
            if (settings == null || settings.MaxReportsPerIncident == 0)
                return View(model);

            _logger.Debug("Display acess: " + settings.MaxReportsPerIncident + " from " + ConfigurationStore.Instance.GetHashCode());
            model.MaxReportsPerIncident = settings.MaxReportsPerIncident;
            model.RetentionDays= settings.RetentionDays;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ReportingViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var settings = new ReportConfig
            {
                MaxReportsPerIncident = model.MaxReportsPerIncident,
                RetentionDays = model.RetentionDays,
            };
            _logger.Debug("Storing: " + settings.MaxReportsPerIncident);
            ConfigurationStore.Instance.Store(settings);
            _logger.Debug("Stored: " + settings.MaxReportsPerIncident + " to " + ConfigurationStore.Instance.GetHashCode());

            return RedirectToAction("Index", "Home");
        }
    }
}