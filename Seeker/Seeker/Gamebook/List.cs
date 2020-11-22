﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class List
    {
        private static Dictionary<string, Description> Books = new Dictionary<string, Description>
        {
            ["Подземелья чёрного замка"] = new Description
            {
                XmlBook = "Gamebooks/BlackCastleDungeon.xml",
                Protagonist = BlackCastleDungeon.Character.Protagonist.Init,
                CheckOnlyIf = BlackCastleDungeon.Actions.CheckOnlyIf,
                Paragraphs = new BlackCastleDungeon.Paragraphs(),
                Actions = new BlackCastleDungeon.Actions(),
                Constants = new BlackCastleDungeon.Constants(),
                Save = BlackCastleDungeon.Character.Protagonist.Save,
                Load = BlackCastleDungeon.Character.Protagonist.Load,
                Disclaimer = "Браславский Дмитрий, 1991",
                BookColor = "#151515",
                Illustration = "BlackCastleDungeon.jpg",
            },

            ["Тайна капитана Шелтона"] = new Description
            {
                XmlBook = "Gamebooks/CaptainSheltonsSecret.xml",
                Protagonist = CaptainSheltonsSecret.Character.Protagonist.Init,
                CheckOnlyIf = CaptainSheltonsSecret.Actions.CheckOnlyIf,
                Paragraphs = new CaptainSheltonsSecret.Paragraphs(),
                Actions = new CaptainSheltonsSecret.Actions(),
                Constants = new CaptainSheltonsSecret.Constants(),
                Save = CaptainSheltonsSecret.Character.Protagonist.Save,
                Load = CaptainSheltonsSecret.Character.Protagonist.Load,
                Disclaimer = "Браславский Дмитрий, 1992",
                BookColor = "#4682B4",
                Illustration = "CaptainSheltonsSecret.jpg",
            },

            ["Верная шпага короля"] = new Description
            {
                XmlBook = "Gamebooks/FaithfulSwordOfTheKing.xml",
                Protagonist = FaithfulSwordOfTheKing.Character.Protagonist.Init,
                CheckOnlyIf = FaithfulSwordOfTheKing.Actions.CheckOnlyIf,
                Paragraphs = new FaithfulSwordOfTheKing.Paragraphs(),
                Actions = new FaithfulSwordOfTheKing.Actions(),
                Constants = new FaithfulSwordOfTheKing.Constants(),
                Save = FaithfulSwordOfTheKing.Character.Protagonist.Save,
                Load = FaithfulSwordOfTheKing.Character.Protagonist.Load,
                Disclaimer = "Браславский Дмитрий, 1995",
                BookColor = "#911",
                Illustration = "FaithfulSwordOfTheKing.jpg",
            },
	    
	        ["Приключения безбородого обманщика"] = new Description
            {
                XmlBook = "Gamebooks/AdventuresOfABeardlessDeceiver.xml",
                Protagonist = AdventuresOfABeardlessDeceiver.Character.Protagonist.Init,
                CheckOnlyIf = AdventuresOfABeardlessDeceiver.Actions.CheckOnlyIf,
                Paragraphs = new AdventuresOfABeardlessDeceiver.Paragraphs(),
                Actions = new AdventuresOfABeardlessDeceiver.Actions(),
                Constants = new AdventuresOfABeardlessDeceiver.Constants(),
                Save = AdventuresOfABeardlessDeceiver.Character.Protagonist.Save,
                Load = AdventuresOfABeardlessDeceiver.Character.Protagonist.Load,
                Disclaimer = "Сизиков Владимир, 2015",
                BookColor = "#5da130",
                Illustration = "AdventuresOfABeardlessDeceiver.jpg",
            },

            ["Джунгарское нашествие"] = new Description
            {
                XmlBook = "Gamebooks/DzungarWar.xml",
                Protagonist = DzungarWar.Character.Protagonist.Init,
                CheckOnlyIf = DzungarWar.Actions.CheckOnlyIf,
                Paragraphs = new DzungarWar.Paragraphs(),
                Actions = new DzungarWar.Actions(),
                Constants = new DzungarWar.Constants(),
                Save = DzungarWar.Character.Protagonist.Save,
                Load = DzungarWar.Character.Protagonist.Load,
                Disclaimer = "Сизиков Владимир, 2016",
                BookColor = "#4a8026",
                Illustration = "DzungarWar.jpg",
                ShowDisabledOption = true,
            },

            ["Скала ужаса"] = new Description
            {
                XmlBook = "Gamebooks/RockOfTerror.xml",
                Protagonist = RockOfTerror.Character.Protagonist.Init,
                CheckOnlyIf = RockOfTerror.Actions.CheckOnlyIf,
                Paragraphs = new RockOfTerror.Paragraphs(),
                Actions = new RockOfTerror.Actions(),
                Constants = new RockOfTerror.Constants(),
                Save = RockOfTerror.Character.Protagonist.Save,
                Load = RockOfTerror.Character.Protagonist.Load,
                Disclaimer = "Тышевич Дмитрий, 2009",
                BookColor = "#000000",
                Illustration = "RockOfTerror.jpg",
            },

            ["Рандеву"] = new Description
            {
                XmlBook = "Gamebooks/RendezVous.xml",
                Protagonist = RendezVous.Character.Protagonist.Init,
                CheckOnlyIf = RendezVous.Actions.CheckOnlyIf,
                Paragraphs = new RendezVous.Paragraphs(),
                Actions = new RendezVous.Actions(),
                Constants = new RendezVous.Constants(),
                Save = RendezVous.Character.Protagonist.Save,
                Load = RendezVous.Character.Protagonist.Load,
                Disclaimer = "Ал Торо, 2020",
                BookColor = "#ffffff",
                FontColor = "#000000",
                BorderColor = "#000000",
                Illustration = "RendezVous.jpg",
            },

            ["Болотная лихорадка"] = new Description
            {
                XmlBook = "Gamebooks/SwampFever.xml",
                Protagonist = SwampFever.Character.Protagonist.Init,
                CheckOnlyIf = SwampFever.Actions.CheckOnlyIf,
                Paragraphs = new SwampFever.Paragraphs(),
                Actions = new SwampFever.Actions(),
                Constants = new SwampFever.Constants(),
                Save = SwampFever.Character.Protagonist.Save,
                Load = SwampFever.Character.Protagonist.Load,
                Disclaimer = "Прокошев Пётр, 2017",
                BookColor = "#ff557c48",
                Illustration = "SwampFever.jpg",
            },

            ["Легенды всегда врут"] = new Description
            {
                XmlBook = "Gamebooks/LegendsAlwaysLie.xml",
                Protagonist = LegendsAlwaysLie.Character.Protagonist.Init,
                CheckOnlyIf = LegendsAlwaysLie.Actions.CheckOnlyIf,
                Paragraphs = new LegendsAlwaysLie.Paragraphs(),
                Actions = new LegendsAlwaysLie.Actions(),
                Constants = new LegendsAlwaysLie.Constants(),
                Save = LegendsAlwaysLie.Character.Protagonist.Save,
                Load = LegendsAlwaysLie.Character.Protagonist.Load,
                Disclaimer = "Островерхов Роман, 2012",
                BookColor = "#4c0000",
                Illustration = "LegendsAlwaysLie.jpg",
            },
        };

        public static List<string> GetBooks()
        {
            return new List<string>(Books.Keys);
        }

        public static Description GetDescription(string name)
        {
            return Books[name];
        }
    }
}
