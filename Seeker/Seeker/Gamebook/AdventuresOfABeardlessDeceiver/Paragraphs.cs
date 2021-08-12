﻿using System.Xml;
using Seeker.Game;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
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

            if (action.Name == "Option")
                action.Option = OptionParse(xmlAction["Option"]);

            return action;
        }

        public override Option OptionParse(XmlNode xmlOption) => OptionParseWithDo(xmlOption, new Modification());

        public override Abstract.IModification ModificationParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            Modification modification = new Modification();

            foreach (string param in Constants.GetModsParams())
                SetPropertyByAttr(modification, param, xmlNode);

            return modification;
        }
    }
}
