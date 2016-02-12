using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for UI panels
/// </summary>
public abstract class EvermorePanel : MonoBehaviour {
    
    public bool hideOnStart;

    public bool Active
    {
        get
        {
            return gameObject.activeSelf;
        }
    }

    public abstract void Initialize();
    public abstract void Refresh();

    public virtual void OnHide() { }
    public virtual void OnShow() { }

    public void SetActive(bool active)
    {
        if (active && !Active)
            OnShow();
        else if (!active && Active)
            OnHide();

        gameObject.SetActive(active);
    }

    public void Hide()
    {
        SetActive(false);
    }

    public void Show()
    {
        SetActive(true);
    }

    public void Toggle()
    {
        SetActive(!Active);
    }

    public float CalcPercentage(float lhs, float rhs)
    {
        if (lhs == 0 || rhs == 0)
            return 0;
        return (lhs - rhs) / lhs;
    }
}
