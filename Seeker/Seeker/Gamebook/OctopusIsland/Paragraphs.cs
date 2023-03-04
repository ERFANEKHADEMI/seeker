﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.OctopusIsland
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) =>
            base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            action.WoundsToWin = Xml.IntParse(xmlAction["WoundsToWin"]);
            action.DinnerHitpointsBonus = Xml.IntParse(xmlAction["Dinner"]);
            action.ReturnedStuffs = Xml.BoolParse(xmlAction["ReturnedStuffs"]);

            if (xmlAction["Enemy"] != null)
                action.Enemies = new List<Character> { EnemyParse(xmlAction["Enemy"]) };

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption)
        {
            Option option = OptionsTemplateWithoutDestination(xmlOption);

            if (ThisIsGameover(xmlOption))
            {
                option.Destination = GetDestination(xmlOption);
            }
            else if (int.TryParse(xmlOption.Attributes["Destination"].Value, out int _))
            {
                option.Destination = Xml.IntParse(xmlOption.Attributes["Destination"]);
            }
            else
            {
                List<string> link = xmlOption.Attributes["Destination"].Value.Split(',').ToList<string>();
                option.Destination = int.Parse(link[random.Next(link.Count())]);
            }

            return option;
        }

        private static Character EnemyParse(XmlNode xmlEnemy) => new Character
        {
            Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
            Skill = Xml.IntParse(xmlEnemy.Attributes["Skill"]),
            Hitpoint = Xml.IntParse(xmlEnemy.Attributes["Hitpoint"]),
        };

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
            Xml.ModificationParse(xmlModification, new Modification());
    }
}
