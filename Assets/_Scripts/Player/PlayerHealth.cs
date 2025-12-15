using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _health;



    private void Start()
    {
        _health = _maxHealth;
    }

    private void Update()
    {
        if(_health < 0)
        {
            //die
        }
    }

    public void TakeDamageOrHeal(int amount)
    {
        _health += amount;
    }


}
