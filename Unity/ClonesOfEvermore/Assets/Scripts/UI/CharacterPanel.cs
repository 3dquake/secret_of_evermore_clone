using System;
using UnityEngine.UI;
using UnityEngine;


public class CharacterPanel : EvermorePanel
{
    public Text attackValue;
    public Text defenceValue;
    public Text agilityValue;

    public Text nameValue;
    public Text levelValue;

    public Image healthBar;
    public Image manaBar;

    void UpdateValues()
    {
        attackValue.text = GameManager.Instance.Characters.Selected.Attack.ToString();
        defenceValue.text = GameManager.Instance.Characters.Selected.Defence.ToString();
        agilityValue.text = GameManager.Instance.Characters.Selected.Agility.ToString();

        nameValue.text = GameManager.Instance.Characters.Selected.Name;
        levelValue.text = GameManager.Instance.Characters.Selected.Level.ToString();

        healthBar.fillAmount = GameManager.Instance.Characters.Selected.Link.health - CalcPercentage(GameManager.Instance.Characters.Selected.Link.health, GameManager.Instance.Characters.Selected.Health);
        manaBar.fillAmount = GameManager.Instance.Characters.Selected.Link.mana - CalcPercentage(GameManager.Instance.Characters.Selected.Link.mana, GameManager.Instance.Characters.Selected.Mana);

    }

    public override void Initialize()
    {
        UpdateValues();
    }

    public override void Refresh()
    {
        UpdateValues();
    }
}
