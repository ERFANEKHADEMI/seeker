﻿using System.Collections.Generic;
using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.OrcsDay
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph)
        {
            Game.Paragraph paragraph = ParagraphTemplate(xmlParagraph);

            foreach (XmlNode xmlOption in xmlParagraph.SelectNodes("Options/*"))
            {
                Option option = OptionsTemplateWithoutLink(xmlOption);

                if (ThisIsGameover(xmlOption))
                {
                    option.Link = GetLink(xmlOption);
                }
                else if (xmlOption.Attributes["Link"].Value == "Back")
                {
                    option.Link = Character.Protagonist.WayBack;
                }
                else
                {
                    option.Link = Xml.IntParse(xmlOption.Attributes["Link"]);
                }

                XmlNode optionMod = xmlOption.SelectSingleNode("Modification");

                if (optionMod != null)
                    option.Do = Xml.ModificationParse(optionMod, new Modification());

                paragraph.Options.Add(option);
            }

            if (Game.Option.IsTriggered("Имя") && (Character.Protagonist.Orcishness <= 0) && (id != 33))
            {
                Character.Protagonist.WayBack = id;
                paragraph.Options.Insert(0, GetOption(link: 33, text: "Получить имя"));
            }
                
            foreach (XmlNode xmlAction in xmlParagraph.SelectNodes("Actions/*"))
                paragraph.Actions.Add(ActionParse(xmlAction));

            foreach (XmlNode xmlModification in xmlParagraph.SelectNodes("Modifications/*"))
                paragraph.Modification.Add(ModificationParse(xmlModification));

            return paragraph;
        }

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);

            if (xmlAction["Enemies"] != null)
            {
                action.Enemies = new List<Character>();

                foreach (XmlNode xmlEnemy in xmlAction.SelectNodes("Enemies/Enemy"))
                    action.Enemies.Add(EnemyParse(xmlEnemy));
            }

            if (action.Type == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            action.Benefit = Xml.ModificationParse(xmlAction["Benefit"], new Modification());

            return action;
        }

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
           (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());

        private Character EnemyParse(XmlNode xmlEnemy)
        {
            Character enemy = new Character();

            foreach (string param in GetProperties(enemy))
                SetPropertyByAttr(enemy, param, xmlEnemy, maxPrefix: true);

            return enemy;
        }

        private static Option GetOption(int link, string text) => new Option
        {
            Link = link,
            Text = text,
        };
    }
}
