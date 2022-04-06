using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] private LevelConfiguration _config;
    [SerializeField] private Toggle itemPrefab;
    [SerializeField] private Image barDuration;
    [SerializeField] private Transform itemContainer;
    private int _itemInInventory;

    [SerializeField] private Button readyContainer;
    
    public float alphaUnselectedItem = 0.4f;
    public float alphaNoSpace = 0.2f;

    private bool hasBeenUpdated;

    private bool isPlaying;
    public SceneReferences sceneRef;

    private Dictionary<int, bool> used = new Dictionary<int, bool>();

    private void Awake()
    {
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        hasBeenUpdated = true;
        
        readyContainer.gameObject.SetActive(_itemInInventory >= _config.levelConfiguration.maxInventorySlot);
        
        barDuration.gameObject.SetActive(false);
    }

    private void Start()
    {
        readyContainer.onClick.AddListener(Ready);
        
        for (var i = 0; i < _config.levelConfiguration.proposedItems; i++)
        {
            var index = i;
            var item = Instantiate(itemPrefab, itemContainer);
            item.transform.GetChild(0).GetComponent<Image>().sprite = _config.levelConfiguration.items[i].itemOff;
            item.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(1, 1, 1, alphaUnselectedItem), 0);
            used[index] = false;
            item.onValueChanged.AddListener(isOn => ToggleItemSlot(isOn, index, item));
        }
    }

    void OnReset()
    {
        _config = FindObjectOfType<LevelConfiguration>();
    }

    void ToggleItemSlot(bool __value, int __index, Toggle __item)
    {
        if (!isPlaying)
        {
            UpdateItemSelection(__value, __index, __item);
        }
        else
        {
            ItemUtilisation(__index, __item);
        }

    }

    void ItemUtilisation(int __index, Toggle __item)
    {
        if (used[__index])
            return;

        used[__index] = true;
        
        Instantiate(_config.levelConfiguration.items[__index].abilityPrefab,
            sceneRef.Instance.player.transform.GetChild(0)).GetComponent<AAbility>().Init(__item.gameObject, barDuration);
    }

    void UpdateItemSelection(bool __value, int __index, Toggle __item)
    {
        if (_itemInInventory >= _config.levelConfiguration.maxInventorySlot && __value)
            return;
        
        //Si on veut l'ajouter dans l'inventory
        var image = __item.transform.GetChild(0).GetComponent<Image>();
        
        image.sprite = __value 
            ? _config.levelConfiguration.items[__index].itemOn 
            : _config.levelConfiguration.items[__index].itemOff;

        _itemInInventory = __value ? _itemInInventory + 1 : _itemInInventory - 1;
        
        image.DOColor(new Color(1, 1, 1, __value ? 1 : alphaUnselectedItem >= _config.levelConfiguration.maxInventorySlot
            ? alphaNoSpace
            : alphaUnselectedItem), 0);


        __item.transform.GetComponent<Image>().DOColor(new Color(1, 1, 1, __value ? 1 : alphaUnselectedItem >= _config.levelConfiguration.maxInventorySlot
            ? alphaNoSpace
            : alphaUnselectedItem), 0);
        
        readyContainer.gameObject.SetActive(_itemInInventory >= _config.levelConfiguration.maxInventorySlot);
        
        if(_itemInInventory >= _config.levelConfiguration.maxInventorySlot || !hasBeenUpdated)
            UpdateAlpha();
    }

    void Ready()
    {
        GameManager.OnGameStart?.Invoke();
        
        foreach (Transform child in itemContainer)
        {
            var toggle = child.transform.GetComponent<Toggle>();
            
            if(!toggle.isOn)
                Destroy(toggle.gameObject);
        }

        isPlaying = true;
        
        readyContainer.gameObject.SetActive(false);
    }

    void UpdateAlpha()
    {
        hasBeenUpdated = _itemInInventory < _config.levelConfiguration.maxInventorySlot;
        
        foreach (Transform child in itemContainer)
        {
            var image = child.transform.GetChild(0).GetComponent<Image>();
            var toggle = child.transform.GetComponent<Toggle>();
            var parentImage = child.GetComponent<Image>();

            if (!toggle.isOn)
            {
                image.DOColor(new Color(1, 1, 1, _itemInInventory >= _config.levelConfiguration.maxInventorySlot
                        ? alphaNoSpace
                        : alphaUnselectedItem), 0); 
                
                parentImage.DOColor(new Color(1, 1, 1, _itemInInventory >= _config.levelConfiguration.maxInventorySlot
                    ? alphaNoSpace
                    : alphaUnselectedItem), 0); 
            }
        }
    }
}
