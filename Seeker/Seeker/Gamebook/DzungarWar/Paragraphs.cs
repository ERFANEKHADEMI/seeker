﻿using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.DzungarWar
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();

        public override Paragraph Get(int id, XmlNode xmlParagraph) => base.Get(xmlParagraph);

        public override Abstract.IActions ActionParse(XmlNode xmlAction)
        {
            Actions action = (Actions)ActionTemplate(xmlAction, new Actions());

            foreach (string param in GetProperties(action))
                SetProperty(action, param, xmlAction);
            
            action.Benefit = ModificationParse(xmlAction["Benefit"]);

            bool bargain = Xml.BoolParse(xmlAction["Bargain"]);

            if (bargain && Game.Data.Triggers.Contains("Bargain"))
                action.Price /= 2;

            if (action.Name == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlModification) =>
           (Abstract.IModification)base.ModificationParse(xmlModification, new Modification());
    }
}
