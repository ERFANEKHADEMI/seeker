﻿using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#911",
            [ButtonTypes.Action] = "#ba2020",
            [ButtonTypes.Option] = "#dbabab",
            [ButtonTypes.Continue] = "#dbabab",
            [ButtonTypes.System] = "#f0dddd",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#870808",
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 660 };

        public static List<string> GetActionParams() => new List<string> {
            "RoundsToWin", "WoundsToWin", "SkillPenalty", "WithoutShooting"
        };

        public static Dictionary<int, int> Skills = new Dictionary<int, int>
        {
            [1] = 12,
            [2] = 8,
            [3] = 10,
            [4] = 7,
            [5] = 9,
            [6] = 11
        };

        public static Dictionary<int, int> Strengths = new Dictionary<int, int>
        {
            [1] = 22,
            [2] = 18,
            [3] = 14,
            [4] = 24,
            [5] = 16,
            [6] = 20
        };

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            CheckOnlyIf = Actions.StaticInstance.CheckOnlyIf,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
        };
    }
}
