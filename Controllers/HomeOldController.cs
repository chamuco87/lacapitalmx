using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaCapitalMX.Models;

namespace LaCapitalMX.Controllers
{

    public class HomeController : Controller
    {
        DB_A57E75_chamucolol87Entities context = new DB_A57E75_chamucolol87Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActivityStep GetStep()
        {
            var activityHistory = context.ActivityHistories.FirstOrDefault(m => m.StatusId == context.Status.FirstOrDefault(n => n.StatusName == "Iniciado").StatusId);

            var step = new ActivityStep();
            step.ActivityStepName = "none";

            if (activityHistory != null)
            {
                var stepHistory = context.StepHistories.FirstOrDefault(m => m.ActivityId == activityHistory.ActivityId && m.Status.StatusName == "Iniciado");
                step = context.ActivitySteps.FirstOrDefault(m => m.ActivityStepId == stepHistory.StepActivityId);
            }
            return step;
        }


        public ActionResult Open()
        {
            var step = GetStep();

            ViewBag.Message = "Open La Capital-MX";

            return View(step);
        }

        public ActionResult GetStatuses()
        {
            var statuses = context.Status.Where(n => n.StatusName == "Posponer" || n.StatusName == "Completado").Select(m => m.StatusName).ToList();
            return View(statuses);
        }

        [HttpGet]
        public ActionResult StartExistingOpening()
        {
            var dbStep = GetStep();
            return View("StepElement", dbStep);
        }

        [HttpGet]
        public ActionResult StartNewOpening()
        {
            var activity = context.Activities.FirstOrDefault(m => m.ActivityName == "Abrir");



            var activityHistory = new ActivityHistory();
            activityHistory.ActivityDatetime = DateTime.Now;
            activityHistory.ActivityId = activity.ActivityId;
            activityHistory.StatusId = context.Status.FirstOrDefault(m => m.StatusName == "Iniciado").StatusId;

            context.ActivityHistories.Add(activityHistory);

            var datetime = DateTime.Now;
            foreach (var step in activity.ActivitySteps)
            {
                var stepHistory = new StepHistory();
                if (step.ActivityStepOrder == 1)
                {
                    stepHistory.StatusId = context.Status.FirstOrDefault(m => m.StatusName == "Iniciado").StatusId;
                }
                else
                {
                    stepHistory.StatusId = context.Status.FirstOrDefault(m => m.StatusName == "No Iniciado").StatusId;
                }

                stepHistory.StepActivityId = step.ActivityStepId;
                stepHistory.StepDatetime = datetime;
                stepHistory.ActivityId = step.ActivityId;
                context.StepHistories.Add(stepHistory);
            }

            context.SaveChanges();

            var dbStep = activity.ActivitySteps.FirstOrDefault(m => m.ActivityStepOrder == 1);
            return View("StepElement", dbStep);

        }
    }
}