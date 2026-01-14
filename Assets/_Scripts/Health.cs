using NUnit.Framework;
using NUnit.Framework.Internal.Filters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public PlayerStats statsObject;

    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI shieldCounter;

    public List<GameObject> activeDotEffects = new List<GameObject>();

    [SerializeField] CharacterAnimator characterAnimator;
    private void Start()
    {
        statsObject.Health = statsObject.MaxHealth;
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        UpdateShield(0);
    }

    private void Update()
    {
        if(statsObject.Health <= 0)
        {
            Debug.Log("Player " + gameObject.name + "died");
            characterAnimator.DeathAnimation();
        }
        if(healthText != null)
        {
            healthText.text = statsObject.Health.ToString();
        }

        //healthBarImage.fillAmount =(float)statsObject.Health / statsObject.MaxHealth;

        statsObject.Health = Mathf.Clamp(statsObject.Health, 0, statsObject.MaxHealth);
    }

    public void TakeDamageOrHeal(int amount)
    {
        statsObject.Health += amount;
        StopAllCoroutines();
        StartCoroutine(DrainBar(statsObject.Health, healthBarImage));
    }

    public void AddShield(int amount)
    {
        statsObject.Defence += amount;
        UpdateShield(statsObject.Defence);
    }

    public void UpdateShield(int amount)
    {
        shieldCounter.text = amount.ToString();
    }

    private IEnumerator DrainBar(int targetHealth, Image bar)
    {
        float timer = 0;
        float duration = 0.5f;
        float startAmount = bar.fillAmount;
        float target = (float)targetHealth / statsObject.MaxHealth;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer /duration;
            float newValue = Mathf.Lerp(startAmount, target, t);

            bar.fillAmount = newValue;
            yield return null;

        }
        bar.fillAmount = target;
    }
}
