using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public Button TryAgainButton;

    public void OnCLick()
    {
        SceneManager.LoadScene(0);
    }
}
