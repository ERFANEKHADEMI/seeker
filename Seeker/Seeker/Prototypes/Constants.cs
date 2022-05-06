﻿using System;
using System.Collections.Generic;
using System.Linq;
using Seeker.Game;
using static Seeker.Output.Buttons;
using static Seeker.Game.Data;
using System.Reflection;

namespace Seeker.Prototypes
{
    class Constants
    {
        private Dictionary<ButtonTypes, string> ButtonsColorsList = null;

        private Dictionary<ColorTypes, string> ColorsList = null;

        private Dictionary<string, string> ButtonTextList = null;

        public List<int> WithoutStatuses { get; set; }

        public List<int> WithoutStaticsButtons { get; set; }

        private bool ShowDisabledOptionStatus = false;

        public virtual string GetColor(ButtonTypes type)
        {
            Dictionary<ButtonTypes, string> color = (Settings.IsEnabled("WithoutStyles") ?
                Output.Constants.DEFAULT_BUTTONS : ButtonsColorsList);

            return (color.ContainsKey(type) ? color[type] : String.Empty);
        }

        public virtual string GetColor(Data.ColorTypes type)
        {
            Dictionary<ColorTypes, string> color = (Settings.IsEnabled("WithoutStyles") ?
                Output.Constants.DEFAULT_COLORS : ColorsList);

            return (color.ContainsKey(type) ? color[type] : String.Empty);
        }

        public void Clean()
        {
            ButtonsColorsList = new Dictionary<ButtonTypes, string>();
            ColorsList = new Dictionary<ColorTypes, string>();
            WithoutStatuses = new List<int> { 0 };
            WithoutStaticsButtons = new List<int> { 0 };
            ButtonTextList = new Dictionary<string, string>();
            ShowDisabledOptionStatus = false;
        }

        public virtual void LoadColor(string type, string color)
        {
            if (Enum.TryParse(type, out ColorTypes colorTypes))
                ColorsList.Add(colorTypes, color);

            else if (Enum.TryParse(type, out ButtonTypes buttonTypes))
                ButtonsColorsList.Add(buttonTypes, color);
        }

        public virtual List<int> GetParagraphsWithoutStatuses() => WithoutStatuses;

        public virtual List<int> GetParagraphsWithoutStaticsButtons() => WithoutStaticsButtons;

        public virtual void LoadList(string name, List<string> list)
        {
            PropertyInfo param = this.GetType().GetProperty(name);

            if (param.PropertyType == typeof(List<int>))
                this.GetType().GetProperty(name).SetValue(this, list.Select(x => int.Parse(x)).ToList());
            else
                this.GetType().GetProperty(name).SetValue(this, list);

        }

        public static string DefaultColor(Data.ColorTypes type) => Output.Constants.DEFAULT_COLORS[type];

        public virtual string GetFont() => String.Empty;

        public virtual Output.Interface.TextFontSize GetFontSize() => Output.Interface.TextFontSize.normal;

        public virtual int? GetParagraphsStatusesLimit() => null;

        public void LoadEnabledDisabledOption(string option) => ShowDisabledOptionStatus = (option == "Show");

        public virtual bool ShowDisabledOption() => ShowDisabledOptionStatus;

        public virtual Dictionary<string, string> ButtonText() => ButtonTextList;

        public void LoadButtonText(string button, string text) => ButtonTextList.Add(button, text);
    }
}
