using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool _pause;
    public void PauseUnpause()
    {
        _pause = !_pause;

        if( _pause ) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
    
}
