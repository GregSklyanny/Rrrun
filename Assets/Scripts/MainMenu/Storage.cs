using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage Instance;

    
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (Storage.Instance == null)
        {
            Storage.Instance = this;
        }

        PlayerPrefs.GetInt("Highscore");
        
    }

    // Update is called once per frame

}
