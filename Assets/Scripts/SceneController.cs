using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Scene currentscene;
    private Scene lastscene;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        currentscene = SceneManager.GetActiveScene();
    }
    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void ChangedActiveScene(Scene current, Scene next)
    {
        lastscene = current;
        currentscene = next;
    }   
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //SceneManager.SetActiveScene(scene);
    }
    public void SceneLoader(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void CycleDay()
    {
        if (currentscene.name[1] == 'D')
        {
            char stagenumber = currentscene.name[0];
            string stage = stagenumber.ToString() + 'N';
            SceneLoader(stage);
        }
        else if (currentscene.name[1] == 'N')
        {
            char stagenumber = currentscene.name[0];
            string stage = stagenumber.ToString() + 'D';
            SceneLoader(stage);
        }
        else throw new ArgumentOutOfRangeException("how is it neither day OR NIGHT????");
    }
}
