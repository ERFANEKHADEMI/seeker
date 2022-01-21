﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.StringOfWorlds
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _skill;
        public int MaxSkill { get; set; }
        public int Skill
        {
            get => _skill;
            set => _skill = Game.Param.Setter(value, max: MaxSkill);
        }

        private int _strength;
        public int MaxStrength { get; set; }
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, max: MaxStrength);
        }

        private int _charm;
        public int Charm
        {
            get => _charm;
            set
            {
                if (value < 2)
                    _charm = 2;
                else
                    _charm = value;
            }
        }

        public int Blaster { get; set; }
        public int GateCode { get; set; }
        public string Equipment { get; set; }
        public List<bool> Luck { get; set; }
        public int Coins { get; set; }

        public override void Init()
        {
            int dice = Game.Dice.Roll(dices: 2);

            MaxSkill = Constants.Skills()[dice];
            Skill = MaxSkill;
            MaxStrength = Constants.Strengths()[dice];
            Strength = MaxStrength;
            Charm = Constants.Charms()[dice];

            Blaster = 1;
            GateCode = 0;
            Equipment = String.Empty;
            Coins = 0;

            Luck = new List<bool> { false, true, true, true, true, true, true };

            for (int i = 0; i < 2; i++)
                Luck[Game.Dice.Roll()] = false;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            MaxSkill = this.MaxSkill,
            Skill = this.Skill,
            MaxStrength = this.MaxStrength,
            Strength = this.Strength,
            Charm = this.Charm,
            Blaster = this.Blaster,
            GateCode = this.GateCode,
            Equipment = this.Equipment,
            Coins = this.Coins,
        };

        public override string Save() => String.Join("|",
            MaxSkill, Skill, MaxStrength, Strength, Charm, Blaster, GateCode, Equipment, Coins,
            String.Join(",", Luck.Select(x => x ? "1" : "0"))
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            MaxSkill = int.Parse(save[0]);
            Skill = int.Parse(save[1]);
            MaxStrength = int.Parse(save[2]);
            Strength = int.Parse(save[3]);
            Charm = int.Parse(save[4]);
            Blaster = int.Parse(save[5]);
            GateCode = int.Parse(save[6]);
            Equipment = save[7];
            Coins = int.Parse(save[8]);

            Luck = save[9].Split(',').Select(x => x == "1").ToList();
        }
    }
}
