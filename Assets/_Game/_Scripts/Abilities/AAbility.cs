using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AAbility : MonoBehaviour
{
    public GameObject uiAbility { get; set; }
    public Image barDuration { get; set; }
    public float abilityDuration = 0;

    public bool autoDestroy = true;
    public Color goodRemainingDurationColor = Color.red;
    public Color badRemainingDurationColor = Color.green;

    protected void Start()
    {
        UpdateDuration();

        if (autoDestroy)
        {
            Destroy(gameObject, abilityDuration);
            Destroy(uiAbility, abilityDuration);
        }
    }


    public void Init(GameObject __uiAbility, Image __barDuration)
    {
        uiAbility = __uiAbility;
        barDuration = __barDuration;
    }

    void UpdateDuration()
    {
        barDuration.fillAmount = 1;
        barDuration.gameObject.SetActive(true);
        StartCoroutine(UpdateUi());
        IEnumerator UpdateUi()
        {
            var time = 0f;
            var image = uiAbility.GetComponent<Image>();
            
            while (time < abilityDuration)
            {
                time += Time.deltaTime;
                barDuration.fillAmount = 1 - time / abilityDuration;
                barDuration.color = Color.Lerp(goodRemainingDurationColor, badRemainingDurationColor,
                    1 - time / abilityDuration);
                
                image.fillAmount = 1 - time / abilityDuration;
                
                yield return null;
            }

            barDuration.fillAmount = 0;
            barDuration.gameObject.SetActive(false);
        }
    }
}
