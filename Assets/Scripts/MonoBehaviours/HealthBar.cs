using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;

    public Image meterImage;
    public TextMeshProUGUI hpText;

    [HideInInspector]
    public Player character;

    float maxHitPoints;
    
    
    // Start is called before the first frame update
    void Start()
    {
        maxHitPoints = character.maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        {
            meterImage.fillAmount = hitPoints.value / maxHitPoints;

            hpText.text = "HP: " + (meterImage.fillAmount * 100);
        }
    }
}
