using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HUDPanel : EvermorePanel {
    [Header("Human settings")]
    public Text humanName;
    public Image humanHealthBar;
    public Image humanManaBar;

    Character m_human;

    [Header("Dog settings")]
    public Text dogName;
    public Image dogHealthBar;
    public Image dogManaBar;

    Character m_dog;

    public override void Initialize()
    {
        m_human = GameManager.Instance.Human;
        m_dog = GameManager.Instance.Dog;
    }

    void UpdateValues()
    {
        dogName.text = m_dog.Name;
        if (m_dog.Link.isDead)
        {
            dogHealthBar.fillAmount = 0;
            dogManaBar.fillAmount = 0;
        }
        else
        {
            dogHealthBar.fillAmount = 1 * (1 - Percentage(m_dog.Link.health, m_dog.Health));
            dogManaBar.fillAmount = 1 * (1 - Percentage(m_dog.Link.mana, m_dog.Mana));
        }

        humanName.text = m_human.Name;
        if (m_human.Link.isDead)
        {
            humanHealthBar.fillAmount = 0;
            humanManaBar.fillAmount = 0;
        }
        else
        {
            humanHealthBar.fillAmount = 1*(1-Percentage(m_human.Link.health, m_human.Health));
            humanManaBar.fillAmount = 1*(1-Percentage(m_human.Link.mana, m_human.Mana));
        }

    }

    public override void Refresh()
    {
        UpdateValues();
    }

}
