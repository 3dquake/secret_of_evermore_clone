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

    float CalcPercentage(float lhs, float rhs)
    {
        if (lhs == 0 || rhs == 0)
            return 0;
        return lhs - rhs / lhs;
    }

    void UpdateValues()
    {
        attackValue.text = GameManager.Instance.Characters.Selected.Attack.ToString();
        defenceValue.text = GameManager.Instance.Characters.Selected.Defence.ToString();
        agilityValue.text = GameManager.Instance.Characters.Selected.Agility.ToString();

        nameValue.text = GameManager.Instance.Characters.Selected.Name;
        levelValue.text = GameManager.Instance.Characters.Selected.Level.ToString();

        healthBar.fillAmount = CalcPercentage(GameManager.Instance.Characters.Selected.Health, GameManager.Instance.Characters.Selected.Link.health) / 100;
        manaBar.fillAmount = CalcPercentage(GameManager.Instance.Characters.Selected.Mana, GameManager.Instance.Characters.Selected.Link.mana) / 100;



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
