using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _activePlayer;
    [SerializeField] private ManaManager _manaManager;

    [SerializeField] private GameObject PlayerTurnText, AiTurnText;

    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;
    public ActivePlayer CurrentPlayer { get; private set; }

    public enum ActivePlayer
    {
        Player,
        Ai
    }

    [SerializeField] private PlayerDeckManager playerDeck;
    [SerializeField] private Button _nextTurnButton;


    [SerializeField] private EnemyDeckManager _enemyDeck;

    public int roundCounter { get; private set; }

    private bool paused;
    [SerializeField] private GameObject pauseScreen;
    private void Start()
    {
        CurrentPlayer = ActivePlayer.Player;
        roundCounter = 0;

        //disable turn text
        PlayerTurnText.SetActive(false);
        ShowText(PlayerTurnText, AiTurnText);

        //disable end screens
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);

        //disable pause screen
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseAndUnpause();
        }
    }
    public void PauseAndUnpause()
    {
        paused = !paused;

        GameObject cardHolder = transform.GetChild(0).gameObject;
        if (cardHolder.CompareTag("CardHolder"))

            if (paused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);

                ResetCardSize();
                //disable cards
                cardHolder.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);

            ResetCardSize();

            //enable cards
            cardHolder.SetActive(true);
        }
    }

    public void ResetCardSize()
    {
        //reset all card sizes
        int children = transform.childCount;

        for (int i = 0; i < children; i++)
        {
            transform.GetChild(i).localScale = Vector3.one;
        }
    }
    public void ChangeTurn()
    {
        //change turn
        if (CurrentPlayer == ActivePlayer.Player) CurrentPlayer = ActivePlayer.Ai;
        else CurrentPlayer = ActivePlayer.Player;


        //if players turn enable deck
        if (CurrentPlayer == ActivePlayer.Player)
        {
            ShowText(PlayerTurnText, AiTurnText);

            //update round
            roundCounter++;

            //increase mana
            _manaManager.SetManaAmount(roundCounter);
        }

        //
        if(CurrentPlayer == ActivePlayer.Ai)
        {
            ShowText(AiTurnText, PlayerTurnText);

            //use enemy turn
            _enemyDeck.UseTurn();
        }
    }

    private void ShowText(GameObject go1, GameObject go2)
    {
        //flip object activity
        go1.SetActive(!go1.activeInHierarchy);
        go2.SetActive(!go2.activeInHierarchy);

        //get components from children
        TextMeshProUGUI text = go1.GetComponentInChildren<TextMeshProUGUI>();
        Image image = go1.GetComponentInChildren<Image>();

        //start fade in and out
        StopAllCoroutines();
        StartCoroutine(FadeInAndOutRoutine(image, text, 0.5f));
    }

    IEnumerator FadeInAndOutRoutine(Image image, TextMeshProUGUI text, float duration)
    { 

        float time = 0f;

        //fade in Image + text
        float startAplha = 0;
        float targetAlpha = 1;

        while (time < duration)
        {
                time += Time.deltaTime;
                float t = time / duration;
                float newAlpha = Mathf.Lerp(startAplha, targetAlpha, t);

            SetAlpha(image, text, newAlpha);
            yield return null;
        }
        SetAlpha(image, text, 1); //set alpha to 100%


        //wait between fade in and out
        yield return new WaitForSeconds(0.5f);

        //fade out image + text
        time = 0f;
        startAplha = 1;
        targetAlpha = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            float newAlpha = Mathf.Lerp(startAplha, targetAlpha, t);

            SetAlpha(image, text, newAlpha);
            yield return null;
        }
        SetAlpha(image, text, 0); //set alpha to 0%
    }

    void SetAlpha(UnityEngine.UI.Image image, TextMeshProUGUI text, float alpha)
    {
        Color imgColor = image.color;
        imgColor.a = alpha;
        image.color = imgColor;

        Color txtColor = text.color;
        txtColor.a = alpha;
        text.color = txtColor;
    }

    public void OpenWinScreen()
    {
        WinScreen.SetActive(true);  
    }

    public void OpenLoseScreen()
    {
        LoseScreen.SetActive(true);
    }
}
