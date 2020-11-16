﻿using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Game.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.RockOfTerror
{
    class Constants : Interfaces.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#191919",
            [ButtonTypes.Action] = "#2a2a2a",
            [ButtonTypes.Option] = "#494949",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#000000",
            [ColorTypes.Font] = "#FFFFFF",
            [ColorTypes.StatusBar] = "#151515",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);
        }

        public string GetColor(Game.Data.ColorTypes type)
        {
            return (Colors.ContainsKey(type) ? Colors[type] : String.Empty);
        }

        public string GetFont()
        {
            return String.Empty;
        }

        public double GetLineHeight()
        {
            return 0;
        }
    }
}
