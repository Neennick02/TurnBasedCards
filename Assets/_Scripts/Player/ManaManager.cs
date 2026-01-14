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

        UpdateManaBar();
        _manaCounter.text = stats.CurrentMana.ToString();
    }
    public void DrainMana(int amount)
    {
         stats.CurrentMana -= amount;
        _manaCounter.text = stats.CurrentMana.ToString();
        UpdateManaBar();
    }

    private void UpdateManaBar()
    {
        _manaBar.fillAmount = (float)stats.CurrentMana / stats.MaxMana;
    }
}
