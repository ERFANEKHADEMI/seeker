﻿using System;
using System.Collections.Generic;
using System.Text;

using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.PrairieLaw
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        static Dictionary<ButtonTypes, string> ButtonsColors = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#b66247",
        };

        static Dictionary<ColorTypes, string> Colors = new Dictionary<ColorTypes, string>();

        public string GetButtonsColor(ButtonTypes type) => (ButtonsColors.ContainsKey(type) ? ButtonsColors[type] : String.Empty);

        public string GetColor(Game.Data.ColorTypes type) => (Colors.ContainsKey(type) ? Colors[type] : String.Empty);
    }
}
