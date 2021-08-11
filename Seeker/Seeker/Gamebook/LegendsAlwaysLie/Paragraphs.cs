﻿using System;
using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in Constants.GetActionParams())
                SetProperty(action, param, xmlAction);

            action.Benefit = ModificationParse(xmlAction["Benefit"]);
            action.Damage = ModificationParse(xmlAction["Damage"]);

            if (xmlAction["FoodSharing"] != null)
                action.FoodSharing = FoodSharingParse(xmlAction["FoodSharing"]);

            if (xmlAction["Specialization"] != null)
                action.Specialization = SpecializationParse(xmlAction["Specialization"]);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            if (action.Name == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        private static Character.SpecializationType SpecializationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Character.SpecializationType.Nope;

            bool success = Enum.TryParse(xmlNode.InnerText, out Character.SpecializationType value);

            return (success ? value : Character.SpecializationType.Nope);
        }

        private static Actions.FoodSharingType FoodSharingParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return Actions.FoodSharingType.KeepMyself;

            bool success = Enum.TryParse(xmlNode.InnerText, out Actions.FoodSharingType value);

            return (success ? value : Actions.FoodSharingType.KeepMyself);
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification
            {
                Name = Xml.StringParse(xmlNode.Attributes["Name"]),
                Value = Xml.IntParse(xmlNode.Attributes["Value"]),
                WizardWoundsPenalty = Xml.IntParse(xmlNode.Attributes["WizardWoundsPenalty"]),
                ThrowerWoundsPenalty = Xml.IntParse(xmlNode.Attributes["ThrowerWoundsPenalty"]),
                Empty = Xml.BoolParse(xmlNode.Attributes["Empty"]),
                Init = Xml.BoolParse(xmlNode.Attributes["Init"]),
            };

            return modification;
        }

        private static Character EnemyParse(XmlNode xmlEnemy) => new Character
        {
            Name = Xml.StringParse(xmlEnemy.Attributes["Name"]),
            Strength = Xml.IntParse(xmlEnemy.Attributes["Strength"]),
            Hitpoints = Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
        };
    }
}
