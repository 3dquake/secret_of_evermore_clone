using UnityEngine;
using System.Collections.Generic;

public class CharacterManager {

    public CharacterManager(bool auto = true)
    {
        if (auto)
        {
            FindAllCharacters();
        }
    }

    /// <summary>
    /// Currently selected player character
    /// </summary>
    public Character Selected
    {
        get;
        set;
    }

    /// <summary>
    /// Enemies in this scene
    /// </summary>
    public List<Character> Enemies
    {
        get
        {
            if (m_enemies == null)
            {
                foreach (Character character in m_characters)
                {
                    m_enemies = m_characters.FindAll(x => x.Link.tag == "Enemy");
                }
            }
            return m_enemies;
        }
    }
    List<Character> m_enemies;
    List<Character> m_characters;
    
    /// <summary>
    /// Searches all loaded characters
    /// </summary>
    public void FindAllCharacters()
    {
        VisualCharacter[] characters = Object.FindObjectsOfType<VisualCharacter>();
        m_characters = new List<Character>(characters.Length);

        for (int i = 0; i < characters.Length; i++)
        {
            //Set up starting values
            m_characters.Add(new Character( /*characters[i].charName, characters[i].health, characters[i].mana,characters[i].attack,characters[i].defence,characters[i].agility, characters[i].level*/ ));
            SetupCharacter(m_characters[i], characters[i]);
            //m_characters[i].

            ////Link the visual character
            //m_characters[i].Link = characters[i];
        }
    }

    void SetupCharacter(Character character, VisualCharacter vcharacter)
    {
        // Setup function for characters

        // Setup stats
        //character.Attack = vcharacter.attack;
        //character.Defence = vcharacter.defence;
        character.Agility = vcharacter.agility;

        character.Level = vcharacter.level;
        character.Health = vcharacter.health;
        character.Mana = vcharacter.mana;

        // Equip system?
        //character.Armor = an armor;
        //character.Weapon = a weapon;

        character.Name = vcharacter.charName == "" ? vcharacter.name : vcharacter.charName;
        character.Link = vcharacter;


    }

    public Character FindCharacter(System.Predicate<Character> match)
    {
        Character result = null;
        for (int i = 0; i < m_characters.Count; i++)
        {
            if (match(m_characters[i]))
                result = m_characters[i];
        }
        return result;
    }

    //Character GetNextCharacter()
    //{
    //    m_characters.IndexOf(Selected);
    //    for (int i = 0; i < m_characters.Count-1; i++)
    //    {

    //    }
    //}
    
}
