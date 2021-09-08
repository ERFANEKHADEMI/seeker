﻿using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.StainlessSteelRat
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#738595",
            [ButtonTypes.Continue] = "#8f9daa",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#b9c2ca",
            [ColorTypes.BookColor] = "#738595",
        };

        public override string GetDescription() => "Читатель в роли новобранца Специал" +
            "ьного Корпуса должен отправиться на планету Скралдеспенд и найти и захват" +
            "ить там профессора Гейстескранка. Читатель сам выбирает линию своего пове" +
            "дения, которая приведёт его к цели.";

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
