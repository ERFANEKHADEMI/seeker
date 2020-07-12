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

            foreach (string gamebook in Gamebook.List.GetBooks())
            {
                Button button = Game.Interface.GamebookButton(gamebook);
                button.Clicked += Gamebook_Click;
                Options.Children.Add(button);

                Label disclaimer = Game.Interface.GamebookDisclaimer(gamebook);
                Options.Children.Add(disclaimer);
            }

            UpdateStatus();
        }

        private void Gamebook_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Game.Data.Load(b.Text);
            Paragraph(0);
        }

        public void Paragraph(int id)
        {
            Game.Router.Clean();
            ActionsPlaces.Clear();
            Options.Children.Clear();
            Action.Children.Clear();

            if (id == 0)
                Game.Data.Protagonist();

            Game.Paragraph paragraph = Game.Data.Paragraphs.Get(id);

            Game.Data.CurrentParagraph = paragraph;

            this.Text.Text = (Game.Data.TextOfParagraphs.ContainsKey(id) ? Game.Data.TextOfParagraphs[id] : String.Empty);

            if (!String.IsNullOrEmpty(paragraph.OpenOption))
                Game.Data.OpenedOption.Add(paragraph.OpenOption);

            if (paragraph.Modification != null)
                Game.Data.CurrentParagraph.Modification.Do();

            if ((paragraph.Actions != null) && (paragraph.Actions.Count > 0))
            {
                int index = 0;

                foreach (Interfaces.IActions action in paragraph.Actions)
                {
                    StackLayout actionPlace = new StackLayout()
                    {
                        Orientation = StackOrientation.Vertical,
                        Spacing = 5,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = 20,
                        BackgroundColor = Color.LightGray
                    };

                    foreach (Label enemy in Game.Interface.Represent(action.Do("Representer")))
                        actionPlace.Children.Add(enemy);

                    Button button = Game.Interface.ActionButton(action.ButtonName);
                    button.Clicked += Action_Click;
                    actionPlace.Children.Add(button);

                    Action.Children.Add(actionPlace);

                    Destinations.Add(action.ButtonName, index);
                    ActionsPlaces.Add(index, actionPlace);

                    index += 1;
                }
            }

            foreach (Game.Option option in paragraph.Options)
            {
                string color = Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Main);

                if (!String.IsNullOrEmpty(option.OnlyIf) && !Game.Data.OpenedOption.Contains(option.OnlyIf))
                    continue;
                else if (!String.IsNullOrEmpty(option.OnlyIf))
                    color = Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Option);

                Button button = Game.Interface.OptionButton(option);

                if (button == null)
                    continue;

                Game.Router.Add(option.Text, option.Destination);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }

            UpdateStatus();
            CheckGameOver();
        }

        private void UpdateStatus()
        {
            List<string> statuses = (Game.Data.Actions == null ? null : Game.Data.Actions.Status());

            Status.Children.Clear();

            if (statuses == null)
                Status.IsVisible = false;
            else
            {
                foreach (Label status in Game.Interface.StatusBar(statuses))
                    Status.Children.Add(status);

                Status.IsVisible = true;
            }           
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
                    BackgroundColor = Color.FromHex(Game.Data.Constants.GetButtonsColor(Game.Buttons.ButtonTypes.Option))
                };

                Game.Router.Add("Начать сначала", 0);

                button.Clicked += Option_Click;

                Options.Children.Add(button);
            }
        }

        private static Dictionary<string, int> Destinations = new Dictionary<string, int>();
        private static Dictionary<int, StackLayout> ActionsPlaces = new Dictionary<int, StackLayout>();

        private void Action_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int actionIndex = Destinations[b.Text];

            ActionsPlaces[actionIndex].Children.Clear();

            List<string> actionResult = Game.Data.CurrentParagraph.Actions[actionIndex].Do();

            foreach (Label action in Game.Interface.Actions(actionResult))
                ActionsPlaces[actionIndex].Children.Add(action);

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
