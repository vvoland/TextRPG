using System;
using System.Collections.Generic;
using TextRPG.Render;

namespace TextRPG.Game.Views
{
    public class ViewMessageInfo : ViewMessage
    {
        public ViewMessageInfo(GameSystem game, RenderSystem renderer, string text, Action callback)
            : base(game, renderer, text, BuildButtons(game, callback))
        {
        }

        private static IList<MenuOption> BuildButtons(GameSystem game, Action callback)
        {
            return new List<MenuOption>
            {
                new MenuOption
                {
                    Text = "Continue",
                    Callback = () =>
                    {
                        game.PopView();
                        callback();
                    }
                }
            };
        }

    }
}