﻿using System.Collections.Generic;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;

namespace Seeker.Gamebook.SwampFever
{
    class Constants : Prototypes.Constants, Abstract.IConstants
    {
        public static Constants StaticInstance = new Constants();

        public override Dictionary<ButtonTypes, string> ButtonsColors() => new Dictionary<ButtonTypes, string>
        {
            [ButtonTypes.Main] = "#5c6649",
            [ButtonTypes.Action] = "#5a6546",
            [ButtonTypes.Option] = "#5c6649",
            [ButtonTypes.Continue] = "#8c937f",
            [ButtonTypes.System] = "#717963",
        };

        public override Dictionary<ColorTypes, string> Colors() => new Dictionary<ColorTypes, string>
        {
            [ColorTypes.Background] = "#485432",
            [ColorTypes.Font] = "#eaece8",
            [ColorTypes.ActionBox] = "#707a60",
            [ColorTypes.StatusBar] = "#34411c",
            [ColorTypes.BookColor] = "#ff557c48",
        };

        public static Dictionary<int, string> GetRangeTypes() => new Dictionary<int, string>
        {
            [6] = "ДАЛЬНЯЯ ДИСТАНЦИЯ",
            [5] = "СРЕДНЯЯ ДИСТАНЦИЯ",
            [4] = "БЛИЖНЯЯ ДИСТАНЦИЯ",
        };

        public static Dictionary<int, string> GetUpgrates() => new Dictionary<int, string>
        {
            [1] = "SecondEngine|Второй двигатель",
            [2] = "Stealth|Стелс-покрытие",
            [3] = "Radar|Радар",
            [4] = "CircularSaw|Циркулярная пила",
            [5] = "Flamethrower|Реактивный огнемёт",
            [6] = "PlasmaCannon|Спаренная плазмопушка",
        };

        public static Dictionary<string, int> GetPurchases() => new Dictionary<string, int>
        {
            ["Месячное проживание на Земле"] = 5,
            ["Круиз по галактике"] = 10,
            ["Робот-помощник"] = 50,
            ["Жена/муж-андроид"] = 100,
            ["Звёздный ялик"] = 150,
            ["Кибермодификация тела"] = 200,
            ["Биомодификация тела"] = 300,
            ["Чудо-омоложение"] = 400,
            ["Особняк на Эдеме"] = 500,
            ["Остров на Гидронее"] = 750,
            ["Космический фрегат"] = 1000,
            ["Колонизация нового планетоида"] = 1500,
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
            Debug = Character.Protagonist.Debug,
        };
    }
}
