﻿using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.Genesis
{
    class Constants : Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#4c687c",
            [ButtonTypes.Continue] = "#849fb3",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#293342",
            [ColorTypes.Font] = "#b6cbd8",
            [ColorTypes.StatusBar] = "#3c5363",
        };

        public string GetButtonsColor(ButtonTypes type)
        {
            return (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);
        }

        public string GetColor(Game.Data.ColorTypes type)
        {
            return (Colors.ContainsKey(type) ? Colors[type] : String.Empty);
        }

        public string GetFont() => String.Empty;

        public Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;

        public double? GetLineHeight() => null;

        public List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 201, 202, 203, 204, 205, 206, 207 };
    }
}
