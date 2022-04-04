using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickController : MonoBehaviour
{
    [SerializeField] private LevelConfiguration _config;
    [SerializeField] private Toggle itemPrefab;

    [SerializeField] private Transform itemContainer;

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
            Instantiate(itemPrefab, itemContainer).onValueChanged.AddListener(isOn => ToggleItemSlot(isOn, index));
        }
    }

    void OnReset()
    {
        _config = FindObjectOfType<LevelConfiguration>();
    }

    void ToggleItemSlot(bool __value, int __index)
    {
        
    }
}
