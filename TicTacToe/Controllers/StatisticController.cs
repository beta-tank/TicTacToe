using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using TicTacToe.Web.Data;

namespace TicTacToe.Web.Controllers
{
    public class StatisticController : Controller
    {
        const int PageSize = 10;
        private ApplicationDbContext context;
        public ApplicationDbContext Context
        {
            get
            {
                if (context == null)
                    context = new ApplicationDbContext();
                return context;
            }
            private set { context = value; }
        }
        // GET: Statistic
        public ActionResult Index(int? page)
        {
            var collection = Context.Games.OrderByDescending(g => g.StartTime);
           
            var pageNumber = (page ?? 1);
            return View(collection.ToPagedList(pageNumber, PageSize));
        }

        protected override void Dispose(bool disposing)
        {
            if (context != null)
                context.Dispose();
            //context?.Dispose();
            base.Dispose(disposing);
        }
    }
}