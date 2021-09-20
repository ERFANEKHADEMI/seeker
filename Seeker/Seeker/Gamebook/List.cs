﻿using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook
{
    class List
    {
        private static Dictionary<string, string> BookTitles = new Dictionary<string, string>();

        private static Dictionary<string, Links> Books = new Dictionary<string, Links>
        {
            ["BlackCastleDungeon"] = BlackCastleDungeon.Constants.GetLinks(),
            ["CaptainSheltonsSecret"] = CaptainSheltonsSecret.Constants.GetLinks(),
            ["FaithfulSwordOfTheKing"] = FaithfulSwordOfTheKing.Constants.GetLinks(),
            ["AdventuresOfABeardlessDeceiver"] = AdventuresOfABeardlessDeceiver.Constants.GetLinks(),
            ["DzungarWar"] = DzungarWar.Constants.GetLinks(),
            ["RockOfTerror"] = RockOfTerror.Constants.GetLinks(),
            ["RendezVous"] = RendezVous.Constants.GetLinks(),
            ["SwampFever"] = SwampFever.Constants.GetLinks(),
            ["MentorsAlwaysRight"] = MentorsAlwaysRight.Constants.GetLinks(),
            ["LegendsAlwaysLie"] = LegendsAlwaysLie.Constants.GetLinks(),
            ["StringOfWorlds"] = StringOfWorlds.Constants.GetLinks(),
            ["ThreePaths"] = ThreePaths.Constants.GetLinks(),
            ["InvisibleFront"] = InvisibleFront.Constants.GetLinks(),
            ["SilentSchool"] = SilentSchool.Constants.GetLinks(),
            ["ThoseWhoAreAboutToDie"] = ThoseWhoAreAboutToDie.Constants.GetLinks(),
            ["OctopusIsland"] = OctopusIsland.Constants.GetLinks(),
            ["CreatureOfHavoc"] = CreatureOfHavoc.Constants.GetLinks(),
            ["BloodfeudOfAltheus"] = BloodfeudOfAltheus.Constants.GetLinks(),
            ["PensionerSimulator"] = PensionerSimulator.Constants.GetLinks(),
            ["LordOfTheSteppes"] = LordOfTheSteppes.Constants.GetLinks(),
            ["HowlOfTheWerewolf"] = HowlOfTheWerewolf.Constants.GetLinks(),
            ["StainlessSteelRat"] = StainlessSteelRat.Constants.GetLinks(),
            ["LastHokku"] = LastHokku.Constants.GetLinks(),
            ["Genesis"] = Genesis.Constants.GetLinks(),
            ["Catharsis"] = Catharsis.Constants.GetLinks(),           
            ["PrairieLaw"] = PrairieLaw.Constants.GetLinks(),
            ["HeartOfIce"] = HeartOfIce.Constants.GetLinks(),
            ["VWeapons"] = VWeapons.Constants.GetLinks(),
            ["LandOfUnwaryBears"] = LandOfUnwaryBears.Constants.GetLinks(),
            ["YounglingTournament"] = YounglingTournament.Constants.GetLinks(),
            ["GoingToLaughter"] = GoingToLaughter.Constants.GetLinks(),
        };

        public static List<string> GetBooks() => Books.Keys.ToList();

        public static Description GetDescription(string name)
        {
            Description book = new Description();
                
            book.Links = Books[name];
            book.Book = name;
            book.Illustration = string.Format("{0}.jpg", name);
            book.XmlBook = string.Format("Gamebooks/{0}.xml", name);

            Game.Xml.GetXmlDescriptionData(ref book);

            Abstract.IConstants data = book.Links.Constants;

            book.BookColor = data.GetColor(Game.Data.ColorTypes.BookColor);
            book.FontColor = data.GetColor(Game.Data.ColorTypes.BookFontColor);
            book.BorderColor = data.GetColor(Game.Data.ColorTypes.BookBorderColor);

            return book;
        }

        public static string GetBooksNameByTitle(string title) => BookTitles[title];

        public static void SaveBookTitle(string name, string title) => BookTitles[title] = name;
    }
}
