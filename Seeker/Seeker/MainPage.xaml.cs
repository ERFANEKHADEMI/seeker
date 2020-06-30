﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Android.Content.Res;

namespace Seeker
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Gamebooks();
        }

        public void Gamebooks()
        {
            this.Text.Text = "Выберите книгу:";

            Game.Router.Clean();
            Options.Children.Clear();

            string buttonsColor = Game.Buttons.NextColor();

            foreach (string gamebook in Gamebook.List.Get())
            {
                Button button = new Button()
                {
                    Text = gamebook,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(buttonsColor)
                };

                button.Clicked += Gamebook_Click;

                Options.Children.Add(button);
            }
        }

        private void Gamebook_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Game.Data.Load(Gamebook.List.Find(b.Text));
            Paragraph(0);
        }

        public void Paragraph(int id)
        {
            if (!Gamebook.BlackCastleDungeon.Paragraphs.ContainsKey(id))
                return;

            Game.Router.Clean();
            Options.Children.Clear();
            GoodLuckOptions.IsVisible = false;
            FightPlace.IsVisible = false;

            Game.Paragraph paragraph = Gamebook.BlackCastleDungeon.Paragraphs[id];

            Game.Data.CurrentParagraph = paragraph;

            this.Text.Text = (Game.Data.Paragraphs.ContainsKey(id) ? Game.Data.Paragraphs[id] : String.Empty);

            if (!String.IsNullOrEmpty(paragraph.OpenOption))
                Game.Data.OpenedOption.Add(paragraph.OpenOption);

            if (paragraph.GoodLuckCheck)
            {
                GoodLuckOptions.Children.Clear();

                Button button = new Button()
                {
                    Text = "Проверить удачу",
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(Game.Buttons.NextColor())
                };

                button.Clicked += GoodLuck_Click;

                GoodLuckOptions.Children.Add(button);
                GoodLuckOptions.IsVisible = true;
            }

            if ((paragraph.Enemies != null) && (paragraph.Enemies.Count > 0))
            {
                FightPlace.Children.Clear();

                foreach (Game.Character enemy in paragraph.Enemies)
                {
                    Label enemyName = new Label();
                    enemyName.Text = enemy.Name;
                    enemyName.HorizontalTextAlignment = TextAlignment.Center;
                    FightPlace.Children.Add(enemyName);

                    Label enemyParams = new Label();
                    enemyParams.Text = String.Format("мастерство {0}  выносливость {1}", enemy.Mastery, enemy.Endurance);
                    enemyParams.HorizontalTextAlignment = TextAlignment.Center;
                    FightPlace.Children.Add(enemyParams);
                }

                Button button = new Button()
                {
                    Text = "Сражаться",
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(Game.Buttons.NextColor())
                };

                button.Clicked += Fight_Click;

                FightPlace.Children.Add(button);
                FightPlace.IsVisible = true;
            }

            string buttonsColor = Game.Buttons.NextColor();

            foreach (Game.Option option in paragraph.Options)
            {
                if (!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.OpenedOption.Contains(option.OnlyIf))
                    continue;

                Button button = new Button()
                {
                    Text = option.Text,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(buttonsColor)
                };

                Game.Router.Add(option.Text, option.Destination);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }
        }

        private void GoodLuck_Click(object sender, EventArgs e)
        {
            GoodLuckOptions.Children.Clear();

            Label luck = new Label();

            if (Gamebook.BlackCastleDungeon.GoodLuckCheck())
            {
                luck.Text = "УСПЕХ :)";
                luck.TextColor = Color.Green;
                
            }
            else
            {
                luck.Text = "НЕУДАЧА :(";
                luck.TextColor = Color.Red;
            };

            luck.FontAttributes = FontAttributes.Bold;
            luck.FontSize = 22;
            luck.HorizontalTextAlignment = TextAlignment.Center;

            GoodLuckOptions.Children.Add(luck);
        }

        private void Fight_Click(object sender, EventArgs e)
        {
            FightPlace.Children.Clear();

            List<string> fightResult = Gamebook.BlackCastleDungeon.Fight();

            foreach (string s in fightResult)
            {
                Label fight = new Label();

                if (s.Contains("BAD|"))
                    fight.TextColor = Color.Red;

                if (s.Contains("GOOD|"))
                    fight.TextColor = Color.Green;

                fight.Text = s.Replace("GOOD|", String.Empty).Replace("BAD|", String.Empty);
                fight.FontSize = 10;
                fight.HorizontalTextAlignment = TextAlignment.Start;

                FightPlace.Children.Add(fight);
            }
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Paragraph(Game.Router.Find(b.Text));
        }
    }
}
