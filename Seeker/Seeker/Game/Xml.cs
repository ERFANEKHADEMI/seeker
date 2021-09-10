﻿using System;
using System.Collections.Generic;
using System.Xml;
using Seeker.Gamebook;
using Seeker.Output;
using Xamarin.Forms;

namespace Seeker.Game
{
    class Xml
    {
        private static Dictionary<string, XmlNode> Descriptions { get; set; }

        public static int IntParse(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return 0;

            bool success = int.TryParse(xmlNode.InnerText, out int value);

            return (success ? value : 0);
        }

        public static string StringParse(XmlNode xmlNode) => (xmlNode == null ? String.Empty : xmlNode.InnerText);

        public static Dictionary<string, string> ImagesParse(XmlNode xmlNode)
        {
            Dictionary<string, string> images = new Dictionary<string, string>();

            if (xmlNode == null)
                return images;

            foreach (XmlNode xmlImage in xmlNode.SelectNodes("Image"))
                images.Add(StringParse(xmlImage.Attributes["Image"]), StringParse(xmlImage.Attributes["Aftertext"]));

            return images;
        }

        public static bool BoolParse(XmlNode xmlNode) => xmlNode != null;

        public static Abstract.IModification ModificationParse(XmlNode xmlNode, Abstract.IModification modification, string name = "Name")
        {
            if (xmlNode == null)
                return null;

            modification.Name = StringParse(xmlNode.Attributes[name]);
            modification.Value = IntParse(xmlNode.Attributes["Value"]);
            modification.ValueString = StringParse(xmlNode.Attributes["ValueString"]);

            return modification;
        }

        public static string TextParse(int id, string optionName)
        {
            string textByParagraph = String.Empty;

            if (Game.Data.XmlParagraphs[id]["Text"] != null)
                textByParagraph = Game.Data.XmlParagraphs[id]["Text"].InnerText;

            string textByOption = Game.Data.Actions.TextByOptions(optionName);

            return (String.IsNullOrEmpty(textByOption) ? textByParagraph : textByOption);
        }

        public static void AllTextParse(ref StackLayout textPlace, int id, string optionName)
        {
            string text = Game.Xml.TextParse(id, optionName);

            if (!String.IsNullOrEmpty(text))
                textPlace.Children.Add(Output.Interface.Text(text));

            foreach (Output.Text texts in Game.Xml.TextsParse(Game.Data.XmlParagraphs[id]))
                textPlace.Children.Add(Output.Interface.Text(texts));
        }

        public static List<Text> TextsParse(XmlNode xmlNode, bool aftertext = false)
        {
            List<Text> texts = new List<Text>();

            foreach (XmlNode text in xmlNode.SelectNodes(aftertext ? "Aftertexts/Aftertext" : "Texts/Text"))
            {
                string font = StringParse(text.Attributes["Font"]);

                Text outputText = new Text
                {
                    Content = text.InnerText,
                    Bold = (font == "Bold"),
                    Italic = (font == "Italic"),
                    Alignment = StringParse(text.Attributes["Alignment"])
                };

                if (text.Attributes["Size"] != null)
                {
                    Enum.TryParse(StringParse(text.Attributes["Size"]), out Interface.TextFontSize size);
                    outputText.Size = size;
                }
                else
                    outputText.Size = Interface.TextFontSize.nope;

                texts.Add(outputText);
            }

            return texts;
        }

        public static void GameLoad(string name)
        {
            Game.Data.XmlParagraphs.Clear();
            Game.Data.Triggers.Clear();
            Healing.Clear();

            if (String.IsNullOrEmpty(name))
                return;

            Gamebook.Description gamebook = Gamebook.List.GetDescription(name);

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(DependencyService.Get<Abstract.IAssets>().GetFromAssets(gamebook.XmlBook));

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Gamebook/Paragraphs/Paragraph"))
                Game.Data.XmlParagraphs.Add(Game.Xml.IntParse(xmlNode["ID"]), xmlNode);

            Game.Data.Paragraphs = gamebook.Links.Paragraphs;
            Game.Data.Actions = gamebook.Links.Actions;
            Game.Data.Constants = gamebook.Links.Constants;
            Game.Data.Protagonist = gamebook.Links.Protagonist;
            Game.Data.Save = gamebook.Links.Save;
            Game.Data.Load = gamebook.Links.Load;
            Game.Data.CheckOnlyIf = gamebook.Links.CheckOnlyIf;
        }

        public static void GetXmlDescriptionData(ref Description description)
        {
            if (Descriptions == null)
                DescriptionLoad();

            if (!Descriptions.ContainsKey(description.Book))
                return;

            XmlNode data = Descriptions[description.Book];

            description.Title = StringParse(data["Title"]);
            description.Author = StringParse(data["Author"]);
            description.Authors = StringParse(data["Authors"]);
            description.Translator = StringParse(data["Translator"]);
            description.Translators = StringParse(data["Translators"]);
            description.Year = IntParse(data["Year"]);
            description.Text = StringParse(data["Text"]);

            List.SaveBookTitle(description.Book, description.Title);
        }

        private static void DescriptionLoad()
        {
            Descriptions = new Dictionary<string, XmlNode>();

            XmlDocument xmlFile = new XmlDocument();
            xmlFile.LoadXml(DependencyService.Get<Abstract.IAssets>().GetFromAssets(Game.Data.DescriptionXml));

            foreach (XmlNode xmlNode in xmlFile.SelectNodes("Gamebooks/Description"))
                Descriptions.Add(xmlNode["Name"].InnerText, xmlNode);
        }
    }
}
