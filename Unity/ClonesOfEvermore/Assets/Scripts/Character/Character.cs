using UnityEngine;
using System.Collections;

//contains all infor related to chars (name, hp, mp, weap, stats, level, att, def,...)
public class Character {

    public Character(string name, int health, int mana)
    {
        Stats = new CharacterStats(1,1,1);
        Health = health;
        Mana = mana;
        Level = 1;
    }

    /// <summary>
    /// Link to the visual character and data
    /// </summary>
    public VisualCharacter Link;

    public struct CharacterStats
    {
        public CharacterStats(int att, int def, int agi)
        {
            Attack = att;
            Defence = def;
            Agility = agi;
        }

        public int Attack { get; set; }  //Affects damage output
        public int Defence { get; set; } //Affects damage input
        public int Agility { get; set; } //Affects moving speed
    }

    public string Name { get; set; }
    public int Health { get; set; }
    public int Mana { get; set; }
    //Currently equipped weapon
    public Weapon Weapon { get; set; }
    public int Level { get; set; }

    public CharacterStats Stats;

}
