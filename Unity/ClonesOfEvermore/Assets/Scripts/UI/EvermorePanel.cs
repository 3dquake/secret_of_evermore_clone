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

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
