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
    public string levelName = "Level 01";
    
    [ShowIf("config", Configuration.Settings), Tooltip("Durée maximum du niveau en seconde.")] 
    public float levelDuration = 60;
    #endregion
    
    
    #region items
    [ShowIf("config", Configuration.Items), Range(0, 5), OnValueChanged("UpdateMaxItemSlot")] 
    public int maxInventorySlot;
    
    [ShowIf("config", Configuration.Items), Range(0, 5), OnValueChanged("UpdateProposedItems")] 
    public int proposedItems;

    [ShowIf("config", Configuration.Items), ShowIf("@this.proposedItems != 0"), SerializeField]
    public Sprite[] items;
    #endregion
    
    void UpdateProposedItems()
    {
        items = new Sprite[proposedItems];

        if (proposedItems > maxInventorySlot)
            proposedItems = maxInventorySlot;
    }

    void UpdateMaxItemSlot()
    {
        items = new Sprite[proposedItems];
        
        if (proposedItems > maxInventorySlot)
            maxInventorySlot = proposedItems;
    }
}
