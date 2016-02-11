using UnityEngine;
using System.Collections;

//contains all infor related to chars (name, hp, mp, weap, stats, level, att, def,...)
public class Character {

    public Character(/*string name, int health, int mana, int att, int def, int agi, int level*/)
    {
        //Stats = new CharacterStats(att,def,agi);
        //Health = health;
        //Mana = mana;
        //Level = level;
    }

    /// <summary>
    /// Link to the visual character and data.
    /// </summary>
    public VisualCharacter Link
    {
        get
        {
            return m_link;
        }
        set
        {
            if (!m_link)
            {
                m_link = value;
                m_link.Link = this;
            }
        }
    }
    VisualCharacter m_link;

    //public struct CharacterStats
    //{
    //    public CharacterStats(int att, int def, int agi)
    //    {
    //        Attack = att;
    //        Defence = def;
    //        Agility = agi;
    //    }

    //    public int Attack { get; set; }  //Affects damage output
    //    public int Defence { get; set; } //Affects damage input
    //    public int Agility { get; set; } //Affects moving speed
    //}

    /// <summary>
    /// Name of the character
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Health points of the character
    /// </summary>
    public int Health { get; set; }
    /// <summary>
    /// Mana points
    /// </summary>
    public int Mana { get; set; }
    
    /// <summary>
    /// Currently equipped weapon
    /// </summary>
    public Weapon Weapon { get; set; }

    /// <summary>
    /// Currently equipped armor
    /// </summary>
    public Armor Armor { get; set; }

    /// <summary>
    /// Character level
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Affects damage output
    /// </summary>
    public int Attack
    {
        get
        {
            return Weapon == null ? 0 : Weapon.Damage;
        }
    }

    /// <summary>
    /// Affects damage input
    /// </summary>
    public int Defence
    {
        get
        {
            return Armor == null ? 0 : Armor.Defence;
        }
    } //

    /// <summary>
    /// Affects moving speed
    /// </summary>
    public int Agility { get; set; }

    //public CharacterStats Stats = new CharacterStats();

}
