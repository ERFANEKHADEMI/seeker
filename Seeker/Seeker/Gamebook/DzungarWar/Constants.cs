﻿using System.Collections.Generic;

namespace Seeker.Gamebook.DzungarWar
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public static Dictionary<string, string> StatNames() => new Dictionary<string, string>
        {
            ["Strength"] = "силы",
            ["Skill"] = "ловкости",
            ["Wisdom"] = "мудрости",
            ["Cunning"] = "хитрости",
            ["Oratory"] = "красноречия",
            ["Danger"] = "опасности",
        };

        public override Dictionary<string, string> ButtonText() => new Dictionary<string, string>
        {
            ["Test"] = "Проверить",
        };

        public override bool ShowDisabledOption() => true;

        public static Links GetLinks() => new Links
        {
            Protagonist = Character.Protagonist.Init,
            CheckOnlyIf = Actions.StaticInstance.CheckOnlyIf,
            Paragraphs = Paragraphs.StaticInstance,
            Actions = Actions.StaticInstance,
            Constants = StaticInstance,
            Save = Character.Protagonist.Save,
            Load = Character.Protagonist.Load,
            Debug = Character.Protagonist.Debug,
        };
    }
}
