﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.CaptainSheltonsSecret
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _mastery;
        public int MaxMastery { get; set; }
        public int Mastery
        {
            get => _mastery;
            set => _mastery = Game.Param.Setter(value, max: MaxMastery);
        }

        private int _endurance;
        public int MaxEndurance { get; set; }
        public int Endurance
        {
            get => _endurance;
            set => _endurance = Game.Param.Setter(value, max: MaxEndurance);
        }

        public int Gold { get; set; }
        public int ExtendedDamage { get; set; }
        public int MasteryDamage { get; set; }
        public bool SeaArmour { get; set; }
        public List<bool> Luck { get; set; }

        private static Dictionary<string, int> EnduranceLoss = new Dictionary<string, int>();

        public override void Init()
        {
            int dice = Game.Dice.Roll(dices: 2);

            Name = "Главный герой";
            MaxMastery = Constants.Masterys()[dice];
            Mastery = MaxMastery;
            MaxEndurance = Constants.Endurances()[dice];
            Endurance = MaxEndurance;
            Gold = 15;
            Luck = new List<bool> { false, true, true, true, true, true, true };

            for (int i = 0; i < 2; i++)
                Luck[Game.Dice.Roll()] = false;

            Game.Healing.Add(name: "Поесть", healing: 4, portions: 3);

            EnduranceLoss.Clear();
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            MaxMastery = this.MaxMastery,
            Mastery = this.Mastery,
            MaxEndurance = this.MaxEndurance,
            Endurance = this.Endurance,
            Gold = this.Gold,
            ExtendedDamage = this.ExtendedDamage,
            MasteryDamage = this.MasteryDamage,
            SeaArmour = this.SeaArmour,
        };

        public Character SetEndurance()
        {
            if (EnduranceLoss.ContainsKey(this.Name))
                this.Endurance = EnduranceLoss[this.Name];

            return this;
        }

        public void SaveEndurance() => EnduranceLoss[this.Name] = this.Endurance;

        public int GetEndurance() => (EnduranceLoss.ContainsKey(this.Name) ? EnduranceLoss[this.Name] : this.Endurance);

        public override string Save() => String.Join("|",
            MaxMastery, Mastery, MaxEndurance, Endurance, Gold, ExtendedDamage, MasteryDamage,
            (SeaArmour ? 1 : 0), String.Join(",", Luck.Select(x => x ? "1" : "0"))
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxMastery = int.Parse(save[0]);
            Mastery = int.Parse(save[1]);
            MaxEndurance = int.Parse(save[2]);
            Endurance = int.Parse(save[3]);
            Gold = int.Parse(save[4]);
            ExtendedDamage = int.Parse(save[5]);
            MasteryDamage = int.Parse(save[6]);
            SeaArmour = (save[7] == "1");

            Luck = save[8].Split(',').Select(x => x == "1").ToList();
        }
    }
}
