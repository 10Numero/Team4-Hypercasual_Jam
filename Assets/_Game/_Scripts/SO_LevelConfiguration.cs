using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Configuration", fileName = "Level Configuration 0")]
public class SO_LevelConfiguration : ScriptableObject
{
    [Title("Level Configuration", TitleAlignment = TitleAlignments.Centered, Bold = true)]
    public enum Configuration { Settings, Items};
    [EnumToggleButtons, HideLabel]
    public Configuration config;
    
    #region settings
    [ShowIf("config", Configuration.Settings)] 
    public string levelSceneName = "Level 01";
    
    [ShowIf("config", Configuration.Settings)] 
    public string levelName = "Level 01";
    
    [ShowIf("config", Configuration.Settings), Tooltip("Durée maximum du niveau en seconde.")] 
    public float levelDuration = 60;
    #endregion
    
    
    #region items
    [ShowIf("config", Configuration.Items), Range(0, 5), OnValueChanged("UpdateMaxItemSlot")] 
    public int maxInventorySlot;
    
    [ShowIf("config", Configuration.Items), Range(0, 5), OnValueChanged("UpdateProposedItems")] 
    public int proposedItems;

    [ShowIf("config", Configuration.Items)/*, ShowIf("@this.proposedItems != 0")*/, SerializeField]
    public List<Item> items = new List<Item>();

    [System.Serializable]
    public class Item
    {
        public Sprite itemOn;
        public Sprite itemOff;
        public GameObject abilityPrefab;
    }
    #endregion
    
    void UpdateProposedItems()
    {
        if (proposedItems < maxInventorySlot)
        {
            maxInventorySlot = proposedItems;
            //items = new Item[proposedItems];
            
            // UpdateItemLength();
        }
    }

    // void UpdateItemLength()
    // {
    //     if (proposedItems > items.Count)
    //     {
    //         while (proposedItems < items.Count)
    //         {
    //             items.Add(new Item());
    //         }
    //     }
    //     else
    //     {
    //         while (proposedItems > items.Count)
    //         {
    //             items.RemoveAt(items.Count - 1);
    //         }
    //     }
    // }

    void UpdateMaxItemSlot()
    {
        //UpdateItemLength();
        //items = new Item[proposedItems];
        
        if (proposedItems < maxInventorySlot)
            maxInventorySlot = proposedItems;
    }
}
