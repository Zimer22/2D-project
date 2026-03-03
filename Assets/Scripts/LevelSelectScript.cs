using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
    GameObject SceneController;
    SceneController SceneControllerScript;
    [SerializeField] UnityEngine.UI.Button nextlevel;
    [SerializeField] UnityEngine.UI.Button quit;

    void Start()
    {
        SceneController = GameObject.Find("SceneController");
        SceneControllerScript = SceneController.GetComponent<SceneController>();
        nextlevel.onClick.AddListener(NextLevel);
        quit.onClick.AddListener(ExitGame);
    }
    void NextLevel()
    {
        SceneControllerScript.SceneLoader("1D");
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
