﻿using System.Collections.Generic;
using Xamarin.Forms;
using static Seeker.Output.Interface;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Output
{
    class Constants
    {
        public static string BACK_LINK = "НАЗАД";
        public static string DISCLAIMER_LINK = "➝ подробнее";

        public static Color BACKGROUND = Color.FromHex("#f7f7f7");
        public static Color LINK_COLOR_DEFAULT = Color.Black;
        public static Color SPLITTER_COLOR_DEFAULT = Color.FromHex("#bdbdbd");

        public static double BIG_FONT = 25;
        public static double STATUSBAR_FONT = 12;
        public static double LINE_HEIGHT = 1.20;
        public static double ACTIONPLACE_SPACING = 5;
        public static double ACTIONPLACE_PADDING = 20;
        public static double TEXT_LABEL_MARGIN = 5;
        public static double BORDER_WIDTH = 1;
        public static double REPRESENT_PADDING = -10;
        public static double SPLITTER_HIGHT = 25;
        public static double DISCLAIMER_BORDER = 8;
        public static double SYS_MENU_SPACING = 4;
        public static double SYS_MENU_HIGHT = 25;

        public static int SORT_BY_TITLE = 1;
        public static int SORT_BY_AUTHORS = 2;
        public static int SORT_BY_SETTINGS = 6;
        public static int SORT_BY_PLAYTHROUGH_TIME = 7;

        public static Dictionary<TextFontSize, double> FontSize = new Dictionary<TextFontSize, double>
        {
            [TextFontSize.micro] = Font(NamedSize.Micro),
            [TextFontSize.small] = Font(NamedSize.Small),
            [TextFontSize.little] = Font(NamedSize.Medium),
            [TextFontSize.normal] = Font(NamedSize.Large),
            [TextFontSize.nope] = Font(NamedSize.Large),
            [TextFontSize.big] = BIG_FONT,
        };

        public static Dictionary<TextFontSize, double> FontSizeItalic = new Dictionary<TextFontSize, double>
        {
            [TextFontSize.micro] = Font(NamedSize.Micro),
            [TextFontSize.small] = Font(NamedSize.Micro),
            [TextFontSize.little] = Font(NamedSize.Small),
            [TextFontSize.normal] = Font(NamedSize.Small),
            [TextFontSize.nope] = Font(NamedSize.Small),
            [TextFontSize.big] = Font(NamedSize.Large),
        };

        public static Dictionary<int, string> FONT_TYPE_VALUES = new Dictionary<int, string>
        {
            [0] = "YanoneFont",
            [1] = "RobotoFont",
        };

        public static Dictionary<int, double> FONT_SIZE_VALUES = new Dictionary<int, double>
        {
            [1] = Interface.Font(NamedSize.Small),
            [2] = Interface.Font(NamedSize.Medium),
            [3] = Interface.Font(NamedSize.Large),
            [4] = Constants.BIG_FONT,
        };

        public static List<string> PLAYTHROUGH_TIME = new List<string>
        {
            "На пять минут",
            "На часок-другой",
            "На весь вечер",
        };

        public static Dictionary<string, int> PLAYTHROUGH_TIME_NODE = new Dictionary<string, int>
        {
            ["ShortPlaythrough"] = 0,
            ["MediumPlaythrough"] = 1,
            ["LongPlaythrough"] = 2,
        };

        public static Dictionary<ButtonTypes, string> DEFAULT_BUTTONS = new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#dcdcdc",
            [ButtonTypes.Action] = "#9d9d9d",
            [ButtonTypes.Option] = "#f1f1f1",
            [ButtonTypes.ButtonFont] = "#000000",
            [ButtonTypes.Continue] = "#f1f1f1",
            [ButtonTypes.System] = "#f1f1f1",
        };

        public static Dictionary<ColorTypes, string> DEFAULT_COLORS = new Dictionary<ColorTypes, string>
        {
            [ColorTypes.ActionBox] = "#d7d7d7",
            [ColorTypes.StatusBar] = "#5e5e5e",
            [ColorTypes.StatusFont] = "#ffffff",
            [ColorTypes.Font] = "#000000",
            [ColorTypes.BookColor] = "#ffffff",
            [ColorTypes.BookFontColor] = "#000000",
            [ColorTypes.BookBorderColor] = "#000000",
        };
    }
}
