using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace GildarGaming.LD46
{
    public class MenuController : MonoBehaviour
    {
        public Button startGameButton;
        public Button instructionsButton;
        public Button quitButton;
        public Button backButton;
        public TMP_Text instructionText;


        public void OnCLick(Object eventObj)
        {
            if (((GameObject)eventObj).GetComponent<Button>() == startGameButton)
            {
                SceneManager.LoadScene(1);
            }
            if (((GameObject)eventObj).GetComponent<Button>() == instructionsButton)
            {
                //startGameButton.gameObject.SetActive(false);
                //instructionsButton.gameObject.SetActive(false);
                //quitButton.gameObject.SetActive(false);
                //instructionText.gameObject.SetActive(true);
                //backButton.gameObject.SetActive(true);
            }
            if (((GameObject)eventObj).GetComponent<Button>() == quitButton)
            {
                Application.Quit();
            }
        }
    }
}
