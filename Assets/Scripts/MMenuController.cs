using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MMenuController : MonoBehaviour
{
    GameObject SceneController;
    SceneController SceneControllerScript;
    [SerializeField] UnityEngine.UI.Button StartButton;
    [SerializeField] UnityEngine.UI.Button EndButton;
    void Start()
    {
        SceneController = GameObject.Find("SceneController");
        SceneControllerScript = SceneController.GetComponent<SceneController>();
        StartButton.onClick.AddListener(StartGame);
        EndButton.onClick.AddListener(ExitGame);
    }
    private void StartGame()
    {
        SceneControllerScript.SceneLoader("1D");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
