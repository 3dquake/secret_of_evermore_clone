using UnityEngine;
using System.Collections.Generic;

public class CharacterManager {

    public Character Selected
    {
        get;
        set;
    }

    List<Character> m_characters;

    public void FindAllCharacters()
    {
        VisualCharacter[] characters = Object.FindObjectsOfType<VisualCharacter>();
        m_characters = new List<Character>(characters.Length);
        
    }

    public void Add(Character character)
    {
        m_characters.Add(character);
    }
}
