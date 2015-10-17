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

        /// <summary>
        /// Создаёт новый экземпляр игры
        /// </summary>
        /// <param name="playerName">Имя игрока</param>
        /// <returns>Представление игры или перенаправление на ввод имени</returns>
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

        /// <summary>
        /// Выполняет ход в заданной игре
        /// </summary>
        /// <param name="turn">Параметры хода</param>
        /// <returns>Состояние игры после совершения хода</returns>
        public JsonResult Turn(TurnClientViewModel turn)
        {
           // Ищем игру и делаем проверки
            var game = Context.Games.Find(turn.GameId);
            if (game == null)
                return SendJsonError("Game not found");
            if (!game.Token.Equals(turn.Token))
                return SendJsonError("Token for this game is not valid");
            // Пытаемся выполнить ход
            var isTurnSuccess = game.Move(PlayerCode.One, turn.Turn);
            if (!isTurnSuccess)
                return SendJsonError("Turn not valid");
            var result = new TurnResultViewModel {status = RusultStatus.Success};
            // Если игра не закончена, то ходит бот
            if(!IsGameDone(game, result))
            {
                // Ход бота
                var botMove = Bot.Move(game, PlayerCode.Two);
                // Если произошла ошибка и бот не смог походить
                if (botMove == -1) return SendJsonError("Bot turn error");
                result.opponentMove = (byte) botMove;
                // Проверяем завершение игры ещё раз
                IsGameDone(game, result);
            }
            Context.Commit();
            return Json(result);
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";
            ViewBag.Message = "Tic Tac toe game.";
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

        private bool IsGameDone(Game game, TurnResultViewModel result)
        {
            if (game.IsDone() != GameStatus.Done) return false;
            result.isGameDone = true;
            result.winner = game.Winner;
            return true;
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