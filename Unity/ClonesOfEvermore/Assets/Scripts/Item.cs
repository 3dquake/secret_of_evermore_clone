using UnityEngine;
using System.Collections;

public class Item {

    public ItemType Type
    {
        get;
        private set;
    }
    public enum ItemType
    {
        
    }

    public int Amount
    {
        get; set;
    }


}
