using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton_GameManager : Singleton<Singleton_GameManager>
{    
    /// <summary>
    /// ÉXÉRÉA
    /// </summary>>
    public int Score { get; private set; } 

    private void Update()
    {
        Score++;
    }

    
}
