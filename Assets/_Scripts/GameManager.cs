using UnityEngine;

public class GameManager : MonoBehaviour
{
   public int FightCount {  get; private set; }
    [SerializeField] CharacterAnimator enemyAnimator;

    private void Start()
    {
        FightCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FightCount++;
            enemyAnimator.SwitchCharacter(FightCount);
        }
    }

    public void StartNewFight()
    {

    }
}
