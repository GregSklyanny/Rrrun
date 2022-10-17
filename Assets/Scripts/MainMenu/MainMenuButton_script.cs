using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton_script : MonoBehaviour
{
    
    [SerializeField]
    private Text _highscoretext;
    private int _highscore = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        if (PlayerPrefs.HasKey("highscore"))
        {
            _highscore = PlayerPrefs.GetInt("highscore");
            _highscoretext.text = $"Highscore: {_highscore}";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {

        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
}
