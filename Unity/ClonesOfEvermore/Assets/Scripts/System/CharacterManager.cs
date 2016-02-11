using UnityEngine;
using System.Collections.Generic;

public class CharacterManager {

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
            m_characters.Add(new Character(characters[i].name, characters[i].startingHealth, characters[i].startingMana));

            //Link the visual character
            m_characters[i].Link = characters[i];
        }
    }

    public Character FindCharacter(System.Predicate<Character> match)
    {
        Character result = null;
        for (int i = 0; i < m_characters.Count-1; i++)
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
