using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public List<PlayerStats> statsObjects = new List<PlayerStats>();
    public int currentEnemy = 0;
     public int currentHealth { get; private set;}
    public int currentMaxHealth;
    public int currentDefence;

    [Header("Links to Objects")]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI shieldCounter;

    [Header("Links to scripts")] 

    [SerializeField] CharacterAnimator characterAnimator;
    [SerializeField] GameObject deathParticles;
    [SerializeField] TurnManager turnManager;

    [SerializeField] private List<AudioClip> MonsterDeathSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> HumanDeathSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> WomanDeathSounds = new List<AudioClip>();

    private bool isDead = false;
    private void Start()
    {
        currentMaxHealth = statsObjects[currentEnemy].MaxHealth;
        currentHealth = currentMaxHealth;
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        UpdateShield(0);
    }

    private void Update()
    {
        //check health
        if (currentHealth <= 0 && !isDead)
        {
            HandleDeath();
        }
        //update health amount
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, currentMaxHealth);
    }

    public void Heal(int amount)
    {
        UpdateShield(currentDefence);
        currentHealth += amount;
        StopAllCoroutines();
        StartCoroutine(DrainBar(currentHealth, healthBarImage));
    }

    public void TakeDamage(int amount)
    {
        UpdateShield(currentDefence);
        currentHealth -= amount;
        StopAllCoroutines();
        StartCoroutine(DrainBar(currentHealth, healthBarImage));
    }

    public void AddShield(int amount)
    {
        currentDefence += amount;
        UpdateShield(currentDefence);
    }

    public void IncreaseHealthbarSize()
    {
        //add extra max health on second fight
        if (this.gameObject.CompareTag("Player"))
        {
            currentMaxHealth += 10;
        }
    }

    public void EnableNewCharacterStats()
    {
        //use new stats scriptable object
        currentEnemy++;

        //reset health
        currentMaxHealth = statsObjects[currentEnemy].MaxHealth;
        currentHealth = currentMaxHealth;
        healthBarImage.fillAmount = 1;
        healthText.text = currentHealth.ToString();
        //reset shield
        UpdateShield(0);

        isDead = false;
    }
    public void UpdateShield(int amount)
    {
        currentDefence = amount;
        shieldCounter.text = amount.ToString();
    }

    public void HandleDeath()
    {
        //player death sound
        if(transform.CompareTag("Enemy"))
        {
            if(currentEnemy < 1)
            {
                //woman sound
                AudioManager.Instance.PlayClip(WomanDeathSounds);
            }
            else
            {
                //monster sound
                AudioManager.Instance.PlayClip(MonsterDeathSounds);
            }
        }
        else
        {
            //human sound
            AudioManager.Instance.PlayClip(HumanDeathSounds);
        }

        //play death animation
        characterAnimator.DeathAnimation();
        Instantiate(deathParticles, transform);
        isDead = true;

        //wait before showing end screen
        StartCoroutine(OpenEndScreen(3f));

        //disable cards
        GameObject cardHolder = turnManager.transform.GetChild(0).gameObject;
        if (cardHolder.CompareTag("CardHolder"))
            cardHolder.SetActive(false);
    }

    private IEnumerator DrainBar(int targetHealth, Image bar)
    {
        float timer = 0;
        float duration = 0.5f;
        float startAmount = bar.fillAmount;
        float target = (float)targetHealth / currentMaxHealth;

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

    IEnumerator OpenEndScreen(float time)
    {
        yield return new WaitForSeconds(time);
        //if player open lose screen
        if (this.gameObject.CompareTag("Player"))
        {
            turnManager.OpenLoseScreen();
        }
        //if Ai open win screen
        else if(this.gameObject.CompareTag("Enemy") && currentEnemy < 2)
        {
            turnManager.OpenWinScreen();
        }
        else
        {
            turnManager.OpenFinalWinScreen();
        }
    }
}
