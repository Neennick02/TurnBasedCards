using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public int CurrentMana { get; private set; }
    private int maxMana;
    private int baseManaAmount = 3;
    [SerializeField] private Image _manaBar;
    [SerializeField] private TextMeshProUGUI _manaCounter;

    private void Start()
    {
        SetManaAmount(0);
    }

    public void SetManaAmount(int amount)
    {
        CurrentMana = amount + baseManaAmount; 
        maxMana = amount + baseManaAmount;

        UpdateManaBar();
        _manaCounter.text = CurrentMana.ToString();
    }
    public void DrainMana(int amount)
    {
         CurrentMana -= amount;
        _manaCounter.text = CurrentMana.ToString();

        /*float target = CurrentMana - amount;
        float drainSpeed = 0.5f;
        Mathf.Lerp(CurrentMana, target, drainSpeed * Time.deltaTime);*/


        StartCoroutine(DrainBar(CurrentMana - amount));
    }

    private void UpdateManaBar()
    {
        _manaBar.fillAmount = (float)CurrentMana / maxMana;
    }

    IEnumerator DrainBar(int target)
    {
        float elapsedTime = 0f;
        float waitTime = 1f;
        while (elapsedTime < waitTime)
        {
            _manaBar.fillAmount = Mathf.Lerp(CurrentMana, target, (elapsedTime/ waitTime));
            elapsedTime += Time.deltaTime;
        }
        return null;
    }
}
