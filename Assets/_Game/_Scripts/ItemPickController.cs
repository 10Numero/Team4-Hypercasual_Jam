using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickController : MonoBehaviour
{
    [SerializeField] private LevelConfiguration _config;
    [SerializeField] private Toggle itemPrefab;

    [SerializeField] private Transform itemContainer;
    private int _itemInInventory;
    
    public float alphaUnselectedItem = 0.4f;
    public float alphaNoSpace = 0.2f;

    private void Awake()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void Start()
    {
        for (var i = 0; i < _config.levelConfiguration.proposedItems; i++)
        {
            var index = i;
            var item = Instantiate(itemPrefab, itemContainer);
            item.transform.GetChild(0).GetComponent<Image>().sprite = _config.levelConfiguration.items[i].itemOff;
            item.onValueChanged.AddListener(isOn => ToggleItemSlot(isOn, index, item));
        }
    }

    void OnReset()
    {
        _config = FindObjectOfType<LevelConfiguration>();
    }

    void ToggleItemSlot(bool __value, int __index, Toggle __item)
    {
        //Si on veut l'ajouter dans l'inventory
        var image = __item.transform.GetChild(0).GetComponent<Image>();
        
        image.sprite = __value 
            ? _config.levelConfiguration.items[__index].itemOn 
                : _config.levelConfiguration.items[__index].itemOff;

        _itemInInventory = __value ? _itemInInventory + 1 : _itemInInventory - 1;
    }

    void UpdateAlpha()
    {
        foreach (Transform child in itemContainer)
        {
            var image = child.transform.GetChild(0).GetComponent<Image>();
            var toggle = child.transform.GetComponent<Toggle>();

            if (!toggle.isOn)
            {
                image.DOColor(new Color(1, 1, 1, _itemInInventory >= _config.levelConfiguration.maxInventorySlot
                        ? alphaNoSpace
                        : alphaUnselectedItem), 0); 
            }
            else
            {
                image.DOColor(new Color(1, 1, 1, _itemInInventory >= _config.levelConfiguration.maxInventorySlot
                    ? alphaNoSpace
                    : alphaUnselectedItem), 0); 
            }
            
            
        
            // __item.transform.GetComponent<Image>().DOColor(new Color(1, 1, 1, __value 
            //     ? 1 
            //     : _itemInInventory >= _config.levelConfiguration.maxInventorySlot
            //         ? alphaNoSpace
            //         : alphaUnselectedItem), 0);
        }
    }
}
