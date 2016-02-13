using UnityEngine;
using System.Collections;

/// <summary>
/// Character class holds all the data related to characters
/// </summary>
public class Character {

    public Character()
    {

    }

    /// <summary>
    /// Link to the visual character.
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
    
    /// <summary>
    /// Name of this character
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Health points of the character
    /// </summary>
    public int Health { get; set; }
    /// <summary>
    /// Mana points of this character
    /// </summary>
    public int Mana { get; set; }
    
    /// <summary>
    /// Currently equipped weapon
    /// </summary>
    public Weapon Weapon
    {
        get
        {
            return m_weapon;
        }
        set
        {
            if (m_weapon != null)
                m_weapon.OnDequip();
            m_weapon = value;
            m_weapon.Owner = this;
            m_weapon.OnEquip();
        }
    }
    Weapon m_weapon;


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
    public int Attack { get { return Weapon != null ? Weapon.Damage : 0; } }

    /// <summary>
    /// Affects damage input
    /// </summary>
    public int Defence { get { return Armor != null ? Armor.Defence : 0; } }

    /// <summary>
    /// Affects moving speed
    /// </summary>
    public int Agility { get; set; }

    //public CharacterStats Stats = new CharacterStats();

}
