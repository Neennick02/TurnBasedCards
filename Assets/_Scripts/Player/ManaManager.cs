using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public int CurrentMana;
    private int maxMana;
    [SerializeField] private int baseManaAmount = 3;
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
        UpdateManaBar();
    }

    private void UpdateManaBar()
    {
        _manaBar.fillAmount = (float)CurrentMana / maxMana;
    }

    IEnumerator DrainBar(int target)
    {
        yield return null;
    }
}
