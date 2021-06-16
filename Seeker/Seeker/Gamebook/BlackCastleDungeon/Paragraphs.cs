﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;


namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
                paragraph.Options.Add(OptionParse(xmlOption));

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
                paragraph.Modification.Add(Game.Xml.ModificationParse(xmlModification, new Modification()));

            return paragraph;
        }

        private Actions ActionParse(XmlNode xmlAction)
        {
            Actions action = new Actions
            {
                ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
                Text = Game.Xml.StringParse(xmlAction["Text"]),

                RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]),
                WoundsToWin = Game.Xml.IntParse(xmlAction["WoundsToWin"]),
                Price = Game.Xml.IntParse(xmlAction["Price"]),

                Multiple = Game.Xml.BoolParse(xmlAction["Multiple"]),
                ThisIsSpell = Game.Xml.BoolParse(xmlAction["ThisIsSpell"]),
            };

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            action.Benefit = Game.Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            return action;
        }

        private Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplate(xmlOption);

            if (xmlOption.Attributes["Do"] != null)
            {
                Modification modification = new Modification
                {
                    Name = Game.Xml.StringParse(xmlOption.Attributes["Do"]),
                    Value = Game.Xml.IntParse(xmlOption.Attributes["Value"]),
                    ValueString = Game.Xml.StringParse(xmlOption.Attributes["ValueString"]),
                };

                option.Do = modification;
            }

            return option;
        }

        private static Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character
            {
                Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                MaxMastery = Game.Xml.IntParse(xmlEnemy.Attributes["Mastery"]),
                MaxEndurance = Game.Xml.IntParse(xmlEnemy.Attributes["Endurance"]),
            };

            enemy.Mastery = enemy.MaxMastery;
            enemy.Endurance = enemy.MaxEndurance;

            return enemy;
        }
    }
}
