using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToe.Core;
using TicTacToe.Core.Enums;
using TicTacToe.Web.Data;
using TicTacToe.Web.ViewModels;

namespace TicTacToe.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext context;
        //public ApplicationDbContext Context => context ?? (context = new ApplicationDbContext());
        public ApplicationDbContext Context {
            get
            {
                if(context == null)
                    context = new ApplicationDbContext();
                return context;
            }
            private set { context = value; }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
                return View("Index");
            var game  = new Game();
            game.CreateField();
            game.Player = playerName;
            Context.Games.Add(game);
            Context.Commit();
            return View(game);
        }

        
        public JsonResult Turn(TurnClientViewModel turn)
        {
           
            var game = Context.Games.Find(turn.GameId);
            if (game == null)
                return SendJsonError("Game not found");
            if (!game.Token.Equals(turn.Token))
                return SendJsonError("Token for this game is not valid");
            var isTurnSuccess = game.Move(PlayerCode.One, turn.Turn);
            if (!isTurnSuccess)
                return SendJsonError("Turn not valid");
            var result = new TurnResultViewModel {status = RusultStatus.Success};
            if (game.IsDone() == GameStatus.Done)
            {
                result.isGameDone = true;
                result.winner = game.Winner;
            }
            else
            {
                var botMove = Bot.Move(game, PlayerCode.Two);
                if (botMove == -1) return SendJsonError("Bot turn error");
                result.opponentMove = (byte) botMove;
                if (game.IsDone() == GameStatus.Done)
                {
                    result.isGameDone = true;
                    result.winner = game.Winner;
                }
            }
            Context.Commit();
            return Json(result);
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

        private JsonResult SendJsonError(string errorText)
        {
            var result = new TurnResultViewModel
            {
                status = RusultStatus.Error,
                errorText = errorText
            };
            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            if(context != null)
                context.Dispose();
            //context?.Dispose();
            base.Dispose(disposing);
        }
    }
}