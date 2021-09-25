﻿using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#b80f0a",
            [ButtonTypes.Action] = "#a92605",
            [ButtonTypes.Option] = "#878787",
            [ButtonTypes.Continue] = "#db8784",
            [ButtonTypes.System] = "#f2f2f2",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.StatusBar] = "#b42806",
            [ColorTypes.StatusFont] = "#ffffff",
            [ColorTypes.BookColor] = "#b80f0a",
        };

        public override Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.little;

        public static Dictionary<string, int> GetStartValues() => new Dictionary<string, int>
        {
            ["Attack"] = 8,
            ["Defence"] = 15,
            ["Endurance"] = 14,
            ["Initiative"] = 10,
        };

        public override List<int> GetParagraphsWithoutStatuses() => new List<int> { 0, 904, 905, 906 };

        public static Dictionary<Character.SpecialTechniques, string> TechniquesNames() => new Dictionary<Character.SpecialTechniques, string>
        {
            [Character.SpecialTechniques.TwoBlades] = "Бой двумя клинками",
            [Character.SpecialTechniques.TotalProtection] = "Веерная защита",
            [Character.SpecialTechniques.FirstStrike] = "Первый удар",
            [Character.SpecialTechniques.PowerfulStrike] = "Мощный выпад",
            [Character.SpecialTechniques.Reaction] = "Реакция",
            [Character.SpecialTechniques.IgnoreReaction] = "Игнорирует прием Реакции",
            [Character.SpecialTechniques.IgnoreFirstStrike] = "Игнорирует прием Первый удар",
            [Character.SpecialTechniques.IgnorePowerfulStrike] = "Игнорирует прием Мощный выпад",
            [Character.SpecialTechniques.ExtendedDamage] = "Каждый удар отнимает 3 Жизни",
            [Character.SpecialTechniques.PoisonBlade] = "Отравленный клинок",
        };

        public static Dictionary<Character.FightStyles, string> FightStyles() => new Dictionary<Character.FightStyles, string>
        {
            [Character.FightStyles.Aggressive] = "агрессивный",
            [Character.FightStyles.Counterattacking] = "контратакующий",
            [Character.FightStyles.Defensive] = "оборонительный",
            [Character.FightStyles.Fullback] = "глухую защиту",
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
