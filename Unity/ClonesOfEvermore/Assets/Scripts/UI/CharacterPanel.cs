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

    public Image weapon;
    public Image armor;

    void UpdateValues()
    {
        attackValue.text = GameManager.Instance.Characters.Selected.Weapon != null ? GameManager.Instance.Characters.Selected.Attack.ToString() : "0" ;
        defenceValue.text = GameManager.Instance.Characters.Selected.Armor != null ? GameManager.Instance.Characters.Selected.Defence.ToString() : "0";
        agilityValue.text = GameManager.Instance.Characters.Selected.Agility.ToString();

        nameValue.text = GameManager.Instance.Characters.Selected.Name;
        levelValue.text = GameManager.Instance.Characters.Selected.Level.ToString();

        healthBar.fillAmount = GameManager.Instance.Characters.Selected.Link.health - Percentage(GameManager.Instance.Characters.Selected.Link.health, GameManager.Instance.Characters.Selected.Health);
        manaBar.fillAmount = GameManager.Instance.Characters.Selected.Link.mana - Percentage(GameManager.Instance.Characters.Selected.Link.mana, GameManager.Instance.Characters.Selected.Mana);

        weapon.sprite = GameManager.Instance.Characters.Selected.Weapon != null ? GameManager.Instance.Characters.Selected.Weapon.Link.icon : GameManager.Instance.emptyIcon;
        armor.sprite = GameManager.Instance.Characters.Selected.Armor != null ? GameManager.Instance.Characters.Selected.Armor.Link.icon : GameManager.Instance.emptyIcon;

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
