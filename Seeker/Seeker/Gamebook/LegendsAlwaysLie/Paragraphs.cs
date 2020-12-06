﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Seeker.Game;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Paragraphs : Abstract.IParagraphs
    {
        public Game.Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = new Game.Paragraph();

            paragraph.Options = new List<Option>();
            paragraph.Actions = new List<Abstract.IActions>();
            paragraph.Modification = new List<Abstract.IModification>();

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/Option"))
            {
                Option option = new Option
                {
                    Destination = Game.Xml.IntParse(xmlOption.Attributes["Destination"]),
                    Text = Game.Xml.StringParse(xmlOption.Attributes["Text"]),
                    OnlyIf = Game.Xml.StringParse(xmlOption.Attributes["OnlyIf"]),
                };

                paragraph.Options.Add(option);
            }

            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/Action"))
            {
                Actions action = new Actions
                {
                    ActionName = Game.Xml.StringParse(xmlAction["ActionName"]),
                    ButtonName = Game.Xml.StringParse(xmlAction["ButtonName"]),
                    Aftertext = Game.Xml.StringParse(xmlAction["Aftertext"]),
                    Trigger = Game.Xml.StringParse(xmlAction["Trigger"]),
                    Text = Game.Xml.StringParse(xmlAction["Text"]),
                    ConneryAttacks = Game.Xml.StringParse(xmlAction["ConneryAttacks"]),
                    ReactionWounds = Game.Xml.StringParse(xmlAction["ReactionWounds"]),
                    ReactionRound = Game.Xml.StringParse(xmlAction["ReactionRound"]),
                    ReactionHit = Game.Xml.StringParse(xmlAction["ReactionHit"]),

                    Dices = Game.Xml.IntParse(xmlAction["Dices"]),
                    DiceBonus = Game.Xml.IntParse(xmlAction["DiceBonus"]),
                    Price = Game.Xml.IntParse(xmlAction["Price"]),
                    OnlyRounds = Game.Xml.IntParse(xmlAction["OnlyRounds"]),
                    RoundsToWin = Game.Xml.IntParse(xmlAction["RoundsToWin"]),
                    AttackWounds = Game.Xml.IntParse(xmlAction["AttackWounds"]),

                    Disabled = Game.Xml.BoolParse(xmlAction["Disabled"]),
                    IncrementWounds = Game.Xml.BoolParse(xmlAction["IncrementWounds"]),
                    GolemFight = Game.Xml.BoolParse(xmlAction["GolemFight"]),
                    ZombieFight = Game.Xml.BoolParse(xmlAction["ZombieFight"]),
                };

                if (xmlAction["FoodSharing"] != null)
                    action.FoodSharing = FoodSharingParse(xmlAction["FoodSharing"]);

                if (xmlAction["Specialization"] != null)
                    action.Specialization = SpecializationParse(xmlAction["Specialization"]);

                if (xmlAction["Enemies"] != null)
                {
                    action.Enemies = new List<Character>();

                    foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    {
                        Character enemy = new Character
                        {
                            Name = Game.Xml.StringParse(xmlEnemy.Attributes["Name"]),
                            Strength = Game.Xml.IntParse(xmlEnemy.Attributes["Strength"]),
                            Hitpoints = Game.Xml.IntParse(xmlEnemy.Attributes["Hitpoints"]),
                        };

                        action.Enemies.Add(enemy);
                    }
                }


                paragraph.Actions.Add(action);
            }

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/Modification"))
            {
                Modification modification = new Modification
                {
                    Name = Game.Xml.StringParse(xmlModification.Attributes["Name"]),
                    Value = Game.Xml.IntParse(xmlModification.Attributes["Value"]),
                };

                if (xmlModification.Attributes["Empty"] != null)
                    modification.Empty = true;

                if (xmlModification.Attributes["Init"] != null)
                    modification.Init = true;
                
                if (xmlModification.Attributes["WizardWoundsPenalty"] != null)
                    modification.WizardWoundsPenalty = Game.Xml.IntParse(xmlModification.Attributes["WizardWoundsPenalty"]);
                
                if (xmlModification.Attributes["ThrowerWoundsPenalty"] != null)
                    modification.ThrowerWoundsPenalty = Game.Xml.IntParse(xmlModification.Attributes["ThrowerWoundsPenalty"]);

                paragraph.Modification.Add(modification);
            }

            paragraph.Trigger = Game.Xml.StringParse(xmlParagraph["Triggers"]);
            paragraph.LateTrigger = Game.Xml.StringParse(xmlParagraph["LateTriggers"]);
            paragraph.RemoveTrigger = Game.Xml.StringParse(xmlParagraph["RemoveTriggers"]);
            paragraph.Image = Game.Xml.StringParse(xmlParagraph["Image"]);

            return paragraph;
        }

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
    }
}
