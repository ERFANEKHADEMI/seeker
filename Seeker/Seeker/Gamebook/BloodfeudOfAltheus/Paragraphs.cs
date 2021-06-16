﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
                paragraph.Options.Add(OptionParse(xmlOption));

            if (Game.Xml.BoolParse(xmlParagraph["IntuitiveSolution"]))
                paragraph.Options.Add(GetOption(destination: id + 20, text: "Интуитивное решение", onlyIf: "selectOnly"));

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = new Actions
            {
                ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
                FightToDeath = Game.Xml.BoolParse(xmlAction["FightToDeath"]),
                LastWound = Game.Xml.BoolParse(xmlAction["LastWound"]),
                YourRacing = Game.Xml.BoolParse(xmlAction["YourRacing"]),
                Ichor = Game.Xml.BoolParse(xmlAction["Ichor"]),
            };

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = new Option
            {
                Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]),
                Text = Game.Xml.StringParse(xmlOption.Attributes["Text"]),
                OnlyIf = Game.Xml.StringParse(xmlOption.Attributes["OnlyIf"]),
            };

            if (xmlOption.Attributes["Do"] != null)
                option.Do = Game.Xml.ModificationParse(xmlOption, new Modification(), name: "Do");

            return option;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) => new Modification
        {
            Name = Game.Xml.StringParse(xmlModification.Attributes["Name"]),
            Value = Game.Xml.IntParse(xmlModification.Attributes["Value"]),
            ValueString = Game.Xml.StringParse(xmlModification.Attributes["ValueString"]),
            IntuitiveSolution = Game.Xml.BoolParse(xmlModification.Attributes["IntuitiveSolution"]),
        };

        private static Character EnemyParse(XmlNode xmlEnemy) => new Character
        {
            Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
            Strength = Game.Xml.IntParse(xmlEnemy.Attributes["Strength"]),
            Defence = Game.Xml.IntParse(xmlEnemy.Attributes["Defence"]),
        };

        private static Option GetOption(int destination, string text, string onlyIf) => new Option
        {
            Destination = destination,
            Text = text,
            OnlyIf = onlyIf,
        };
    }
}
