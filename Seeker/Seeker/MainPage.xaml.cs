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

            foreach (string gamebook in Gamebook.List.GetBooks())
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

            UpdateStatus();
        }

        private void Gamebook_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Game.Data.Load(b.Text);
            Paragraph(523);
            //Paragraph(0);
        }

        public void Paragraph(int id)
        {
            Game.Router.Clean();
            Options.Children.Clear();
            Action.Children.Clear();
            Action.IsVisible = false;

            Game.Paragraph paragraph = Game.Data.Paragraphs.Get(id);

            Game.Data.CurrentParagraph = paragraph;

            this.Text.Text = (Game.Data.TextOfParagraphs.ContainsKey(id) ? Game.Data.TextOfParagraphs[id] : String.Empty);

            if (!String.IsNullOrEmpty(paragraph.OpenOption))
                Game.Data.OpenedOption.Add(paragraph.OpenOption);

            if (paragraph.Modification != null)
                Game.Data.CurrentParagraph.Modification.Do();

            if (paragraph.Action != null)
            {
                foreach (string s in Game.Data.CurrentParagraph.Action.Do("Representer"))
                {
                    Label enemy = new Label();
                    enemy.Text = s.Replace("\n", System.Environment.NewLine);
                    enemy.HorizontalTextAlignment = TextAlignment.Center;
                    Action.Children.Add(enemy);
                }

                Button button = new Button()
                {
                    Text = paragraph.Action.ButtonName,
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(Game.Buttons.NextColor())
                };

                button.Clicked += Action_Click;

                Action.Children.Add(button);
                Action.IsVisible = true;
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

            UpdateStatus();
            CheckGameOver();
        }

        private void UpdateStatus()
        {
            string status = (Game.Data.Actions == null ? String.Empty : Game.Data.Actions.Status());

            if (String.IsNullOrEmpty(status))
            {
                Status.IsVisible = false;
                return;
            }

            Status.Text = status;
            Status.IsVisible = true;
        }

        private void CheckGameOver()
        {
            bool gameOver = (Game.Data.Actions == null ? false : Game.Data.Actions.GameOver());

            if (gameOver)
            {
                Game.Router.Clean();
                Options.Children.Clear();

                Button button = new Button()
                {
                    Text = "Начать сначала",
                    TextColor = Xamarin.Forms.Color.White,
                    BackgroundColor = Color.FromHex(Game.Buttons.NextColor())
                };

                Game.Router.Add("Начать сначала", 0);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }
        }

        private void Action_Click(object sender, EventArgs e)
        {
            Action.Children.Clear();

            List<string> actionResult = Game.Data.CurrentParagraph.Action.Do();

            foreach (string actionLine in actionResult)
            {
                Label actions = new Label();

                string text = actionLine;

                if (text.Contains("BIG|"))
                    actions.FontSize = 22;
                else
                    actions.FontSize = 10;

                if (text.Contains("BAD|"))
                    actions.TextColor = Color.Red;

                if (text.Contains("GOOD|"))
                    actions.TextColor = Color.Green;

                if (text.Contains("HEAD|"))
                {
                    actions.HorizontalTextAlignment = TextAlignment.Center;
                    actions.FontAttributes = FontAttributes.Bold;
                }
                else
                    actions.HorizontalTextAlignment = TextAlignment.Start;

                foreach (string r in new List<string> { "BIG", "GOOD", "BAD", "HEAD" })
                    text = text.Replace(String.Format("{0}|", r), String.Empty);

                actions.Text = text;
                
                Action.Children.Add(actions);
            }

            UpdateStatus();
            CheckGameOver();
        }

        private void Option_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Paragraph(Game.Router.Find(b.Text));
        }
    }
}
