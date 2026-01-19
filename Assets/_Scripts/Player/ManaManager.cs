using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public PlayerStats stats;
    [SerializeField] private Image _manaBar;
    [SerializeField] private TextMeshProUGUI _manaCounter;

    private void Start()
    {
        SetManaAmount(0);
    }

    public void SetManaAmount(int amount)
    {
        stats.CurrentMana = amount + stats.BaseManaAmount; 
        stats.MaxMana = amount + stats.BaseManaAmount;

        _manaCounter.text = stats.CurrentMana.ToString();

        StopAllCoroutines();
        StartCoroutine(DrainBar(stats.CurrentMana, _manaBar));
    }
    public void DrainMana(int amount)
    {
         stats.CurrentMana -= amount;
        _manaCounter.text = stats.CurrentMana.ToString();
        StartCoroutine(DrainBar(stats.CurrentMana, _manaBar));

    }

    public void ResetMana()
    {
        stats.BaseManaAmount = 3;
        stats.MaxMana = stats.BaseManaAmount;
    }
    private IEnumerator DrainBar(int targetMana, Image bar)
    {
        float timer = 0;
        float duration = 0.6f;
        float startAmount = bar.fillAmount;
        float target = (float)targetMana / stats.MaxMana;

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
