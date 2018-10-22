using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using TwistedGames.Core;
using TwistedGames.Core.Common;
using TwistedGames.Core.Games.Snake;
using TwistedGames.Core.SingleGames.Snake;

namespace TwistedGames.Pages.SingleGames
{
    public class SnakeModel : PageModel
    {
        //Todo make common games repository with auto clean
        private static ConcurrentDictionary<Guid, SnakeGameManager> Games { get; }=
            new ConcurrentDictionary<Guid, SnakeGameManager>();

        public Guid GameId { get; set; }
        public SnakeGameRepresentation GameRepresentation { get; set; }

        private static SnakeGameManager CreateSnakeGameManager() => new SnakeGameManager(new GameSize(20, 15));

        public void OnGet()
        {
            GameId = Guid.NewGuid();
            var gameManager = Games[GameId] = CreateSnakeGameManager();
            GameRepresentation = gameManager.GetActualGameState();
        }

        public JsonResult OnPostRefresh(Guid gameId)
        {
            var gameManager = Games.GetOrAdd(gameId, _ => CreateSnakeGameManager());
            return new JsonResult(gameManager.GetActualGameState());
        }

        public JsonResult OnPostAction(Guid gameId, ActionType action)
        {
            var gameManager = Games.GetOrAdd(gameId, _ => CreateSnakeGameManager());
            return new JsonResult(gameManager.DoAction(action));
        }
    }
}