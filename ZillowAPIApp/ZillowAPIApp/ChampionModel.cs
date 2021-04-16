using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZillowAPIApp
{
    public class ChampionModel
    {
        public string Name;
        public string Title;
        public string Blurb;
        public string Attack;
        public string Defense;
        public string Magic;
        public string Difficulty;

        public ChampionModel(string name, string title, string blurb, string attack, string defense, string magic, string difficulty)
        {
            Name = name;
            Title = title;
            Blurb = blurb;
            Attack = attack;
            Defense = defense;
            Magic = magic;
            Defense = defense;
            Difficulty = difficulty;
        }
    }
}
