using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TwistedGames.Core;
using TwistedGames.Core.Common;
using TwistedGames.Core.Games;
using TwistedGames.Core.Games.Snake;
using TwistedGames.Core.SingleGames.Snake;
using TwistedGames.Core.Storage;

namespace TwistedGames.Pages.SingleGames
{
    public class SnakeModel : PageModel
    {
        public SnakeGameStorage GameStorage { get; }
        public SnakeModel(SnakeGameStorage gameStorage)
        {
            GameStorage = gameStorage;
        }

        public Guid GameId { get; set; }
        public GameRepresentation GameRepresentation { get; set; }

        public void OnGet()
        {
            GameId = Guid.NewGuid();
            var gameManager = GameStorage.GetOrCreateGame(GameId);
            GameRepresentation = gameManager.GetActualGameState();
        }

        public JsonResult OnPostRefresh(Guid gameId)
        {
            var gameManager = GameStorage.GetOrCreateGame(gameId);
            return new JsonResult(gameManager.GetActualGameState());
        }

        public JsonResult OnPostAction(Guid gameId, ActionType action)
        {
            var gameManager = GameStorage.GetOrCreateGame(gameId);
            return new JsonResult(gameManager.DoAction(action));
        }
    }
}