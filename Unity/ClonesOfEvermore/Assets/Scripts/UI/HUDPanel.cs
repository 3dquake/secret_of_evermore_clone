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
        dogHealthBar.fillAmount = m_dog.Link.health - CalcPercentage(m_dog.Link.health, m_dog.Health);
        dogManaBar.fillAmount = m_dog.Link.health - CalcPercentage(m_dog.Link.mana, m_dog.Mana);

        humanName.text = m_human.Name;
        humanHealthBar.fillAmount = m_human.Link.health - CalcPercentage(m_human.Link.health, m_human.Health);
        humanManaBar.fillAmount = m_human.Link.health - CalcPercentage(m_human.Link.mana, m_human.Mana);


    }

    public override void Refresh()
    {
        UpdateValues();
    }

}
