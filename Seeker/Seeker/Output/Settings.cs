﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Seeker.Output
{
    class Settings
    {
        private static Grid SettingGrid;
        private delegate void SettingMethod();

        public static void Add(ref StackLayout settings)
        {
            SettingGrid = new Grid
            {
                ColumnSpacing = 0,
                RowSpacing = 0,

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 20 },
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                },
            };

            SettingOption("Основной шрифт", "FontType", Constants.FONT_TYPE_SETTING);
            SettingOption("Размер шрифта", "FontSize", Constants.FONT_SIZE_SETTING);
            SettingOption("Текст по ширине", "Justyfy", Constants.ON_OFF_SETTING);
            SettingOption("Недоступные опции", "DisabledOption", Constants.OPTION_SETTING);
            SettingOption("Отображать меню", "SystemMenu", Constants.ON_OFF_SETTING);
            SettingOption("Отладка", "Debug", Constants.ON_OFF_SETTING);
            SettingOption("Сортировка", "Sort", Constants.SORT_SETTING);

            SettingCheatingBlock();

            SettingButton("Сбросить сохранённые игры", () => Game.Continue.Clean(), spacer: true);
            SettingButton("Сбросить все настройки", () => Game.Settings.Clean());

            settings.Children.Add(SettingGrid);
        }

        private static void SettingCheatingBlock()
        {
            int currentRow = AddNewRow(ref SettingGrid);

            VerticalText settingTitle = new VerticalText
            {
                Value = "Читерство",
                VerticalOptions = LayoutOptions.Center,
                LeftRotate = true,
            };

            SettingGrid.Children.Add(settingTitle, 0, currentRow);
            Grid.SetRowSpan(settingTitle, 2);

            SettingOption("Кнопка 'Назад'", "CheatingBack", Constants.ON_OFF_SETTING, row: currentRow);

            currentRow = AddNewRow(ref SettingGrid);

            SettingOption("God mode", "CheatingGodmode", Constants.ON_OFF_SETTING, row: currentRow);
        }     

        private static void SettingButton(string settingName, SettingMethod Click, bool spacer = false)
        {
            int currentRow = AddNewRow(ref SettingGrid);

            Label settingButton = new Label
            {
                Text = settingName,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Interface.Font(NamedSize.Medium),
                TextColor = Color.Black,
                TextDecorations = TextDecorations.Underline,
            };

            if (spacer)
                settingButton.Margin = new Thickness(0, 15, 0, 0);

            TapGestureRecognizer click = new TapGestureRecognizer();
            click.Tapped += (sender, e) => SettingClick(sender, Click);
            settingButton.GestureRecognizers.Add(click);

            SettingGrid.Children.Add(settingButton, 0, currentRow);
            Grid.SetColumnSpan(settingButton, 3);
        }

        private static void SettingOption(string settingName, string settingType, List<string> options, int? row = null)
        {
            int currentRow = row ?? AddNewRow(ref SettingGrid);

            Label settingTitle = new Label
            {
                Text = String.Format("{0}:", settingName),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = Interface.Font(NamedSize.Small),
            };

            if (row == null)
            {
                SettingGrid.Children.Add(settingTitle, 0, currentRow);
                Grid.SetColumnSpan(settingTitle, 2);
            }
            else
                SettingGrid.Children.Add(WithGrayLayout(settingTitle), 1, currentRow);
                
            Picker settingPicker = new Picker
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Interface.Font(NamedSize.Medium),
            };
            
            settingPicker.SelectedIndexChanged += (sender, e) => SettingChanged(sender, settingType);

            foreach (string option in options)
                settingPicker.Items.Add(option);

            settingPicker.SelectedIndex = Game.Settings.GetValue(settingType);

            if (row == null)
                SettingGrid.Children.Add(settingPicker, 2, currentRow);   
            else
                SettingGrid.Children.Add(WithGrayLayout(settingPicker), 2, currentRow);
        }

        private static View WithGrayLayout(View element)
        {
            StackLayout place = new StackLayout
            {
                BackgroundColor = Color.LightGray,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            place.Children.Add(element);

            return place;
        }

        private static int AddNewRow(ref Grid settingGrid)
        {
            settingGrid.RowDefinitions.Add(new RowDefinition());
            return settingGrid.RowDefinitions.Count - 1;
        }

        private static void SettingChanged(object sender, string setting) =>
            Game.Settings.SetValue(setting, (sender as Picker).SelectedIndex);

        private static void SettingClick(object sender, SettingMethod method)
        {
            (sender as Label).TextColor = Color.LightGray;
            method();
        }
    }
}
