using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int _maxHealth { get; private set;} = 30;
    [SerializeField] public int _health { get; private set; }
    public int shieldAmount = 0;

    [SerializeField] Image healthBarImage;
    [SerializeField] TextMeshProUGUI healthText;

    private void Start()
    {
        _health = _maxHealth;
    }

    private void Update()
    {
        if(_health < 0)
        {
            Debug.Log("Player " + gameObject.name + "died");
        }
        if(healthText != null)
        {
            healthText.text = _health.ToString();
        }

        healthBarImage.fillAmount =(float) _health / _maxHealth;

        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

    public void TakeDamageOrHeal(int amount)
    {
        _health += amount;
    }

    public void AddShield(int amount)
    {
        shieldAmount += amount;
    }
}
