using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToe.Core;
using TicTacToe.Web.Data;

namespace TicTacToe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var game = new Game {Player = "123"};
            var field = new Field() {Game = game};
            game.Field = field;
            context.Games.Add(game);
            context.Commit();
            context.Dispose();
            return View();
        }

        public ActionResult Game()
        {
            return View();
        }

        public ActionResult About()
        {
            var context = new ApplicationDbContext();
            var games = context.Games.ToArray();
            context.Dispose();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}