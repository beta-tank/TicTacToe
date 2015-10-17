using System.Linq;
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

        /// <summary>
        /// Вывод таблицы прошедших игр
        /// </summary>
        /// <param name="page">Номер страницы</param>
        /// <returns>Представление с таблицей</returns>
        public ActionResult Index(int? page)
        {
            var collection = Context.Games.OrderByDescending(g => g.StartTime);         
            var pageNumber = (page ?? 1);
            return View(collection.ToPagedList(pageNumber, PageSize));
        }

        /// <summary>
        /// Вывод таблицы с детализацией шагов игры
        /// </summary>
        /// <param name="id">Id игры</param>
        /// <returns>Частичное представление с таблицей</returns>
        [HttpGet]
        public ActionResult Details(int id)
        {
            var game = Context.Games.Find(id);
            if(game == null)
                return new HttpNotFoundResult();
            return PartialView(game);
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