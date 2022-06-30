using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button exitButton;
    [SerializeField] Text winText;
    [SerializeField] Text lostText;

/* Tutorial tentativa 

    [SerializeField] GameObject editorTutorial;
    [SerializeField] GameObject mobileTutorial;
    [SerializeField] PlayerController playerController;


    public void Tutorial() {
        playButton.gameObject.SetActive(false);
#if UNITY_EDITOR
        editorTutorial.SetActive(true);
        if(Input.GetMouseButtonDown(0)) {
            Debug.Log("Entrei aqui");
            playerController.StartGame();
        }
#else
        mobileTutorial.SetActive(true);
        if(Input.touchCount > 0) playerController.StartGame();
#endif
    }
*/
    public void LostGame(){
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        lostText.gameObject.SetActive(true);
    }


    private void OnDisable() {
        playButton.gameObject.SetActive(false);
        lostText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    public void WinGame(){
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        winText.gameObject.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }

}
