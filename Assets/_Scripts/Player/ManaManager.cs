using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public PlayerStats stats;
    [SerializeField] private Image _manaBar;
    [SerializeField] private TextMeshProUGUI _manaCounter;

    public int CurrentMana;
    public int MaxMana;
    private int baseMana = 3;
    private void Start()
    {
        MaxMana = stats.MaxMana;
        SetManaAmount(0);
    }

    public void SetManaAmount(int amount)
    {
        CurrentMana = amount + baseMana; 
        MaxMana = CurrentMana;

        _manaCounter.text = CurrentMana.ToString();

        StopAllCoroutines();
        StartCoroutine(DrainBar(CurrentMana, _manaBar));
    }
    public void DrainMana(int amount)
    {
         CurrentMana -= amount;
        _manaCounter.text = CurrentMana.ToString();
        StartCoroutine(DrainBar(CurrentMana, _manaBar));
    }

    public void ResetMana()
    {
        baseMana = 3;
        CurrentMana = baseMana;
        MaxMana = baseMana;

        _manaCounter.text = CurrentMana.ToString();

        StopAllCoroutines();
        StartCoroutine(DrainBar(CurrentMana, _manaBar));
    }
    private IEnumerator DrainBar(int targetMana, Image bar)
    {
        float timer = 0;
        float duration = 1.2f;
        float startAmount = bar.fillAmount;
        float target = (float)targetMana / MaxMana;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            float newValue = Mathf.Lerp(startAmount, target, t);

            bar.fillAmount = newValue;
            yield return null;

        }
        bar.fillAmount = target;
    }
}
