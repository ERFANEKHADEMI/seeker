﻿using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.InvisibleFront
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#d52b1e",
            [ButtonTypes.Option] = "#d52b1e",
            [ButtonTypes.Continue] = "#e57f78",
            [ButtonTypes.Font] = "#eede49",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#ffdadb",
            [ColorTypes.StatusBar] = "#aa2218",
            [ColorTypes.StatusFont] = "#eede49",
            [ColorTypes.BookColor] = "#d52b1e",
            [ColorTypes.BookFontColor] = "#eede49",
        };

        public static List<string> GetApartments() => new List<string>
        {
            "один", "два", "три", "один", "два", "три"
        };

        public override string GetDescription() => "Вам предстоит влезть в шкуру сотрудника всесильной советской разведки и попробовать внести свою лепту в борьбу с акулами империализма.";

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
