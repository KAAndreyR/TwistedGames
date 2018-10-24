using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwistedGames.Core.Common;
using TwistedGames.Core.Games;
using TwistedGames.Core.Storage;

namespace TwistedGames.Pages.Shared
{
    public abstract class GameModel : PageModel
    {
        public IGameStorage<IGameManager> GameStorage { get; }
        public Guid GameId { get; set; }
        public GameRepresentation GameRepresentation { get; set; }

        protected GameModel(IGameStorage<IGameManager> gameStorage)
        {
            GameStorage = gameStorage;
        }

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