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
        public int Attack;
        public int Defense;
        public int Magic;
        public int Difficulty;

        public ChampionModel(string name, string title, string blurb, int attack, int defense, int magic, int difficulty)
        {
            Name = name;
            Title = title;
            Blurb = blurb;
            Attack = attack;
            Defense = defense;
            Magic = magic;
            Defense = defense;
        }
    }
}
