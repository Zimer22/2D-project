using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    private GameObject    Player;
    private PlayerScript  Playerscr;
    private GameObject    Timer;
    private TimerScript   Timerscr;
    private Animator      Knightanim;
    private Animator      Skeletonanim;
    private GameObject    SceneController;
    private SceneController SceneControllerScript;
    private Button        FightButton;
    private TMP_Text      ButtonText;
    private TMP_Text      EndText;
    private TMP_Text      PHealth;  
    private TMP_Text      EHealth;
    private TMP_Text      PDamage;
    private TMP_Text      EDamage;
    private TMP_Text      Turns;
    private Scene         scene;
    private int           gc_enemyhealth;
    private int           gc_enemydamage;
    private int           gc_turns;
    private int           gc_plmaxhealth;
    private int           gc_plcurrenthealth;
    private int           gc_pldamage;
    private bool          gc_firstnight;
    private bool          gc_playerlost;
    private bool          gc_notfirstloop;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        scene = SceneManager.GetActiveScene();
        SceneController = GameObject.Find("SceneController");
        SceneControllerScript = SceneController.GetComponent<SceneController>();
    }
    void Start()        
    {
        SceneManager.activeSceneChanged += ChangedActiveScene; 
    }
    void ChangedActiveScene(Scene current, Scene next)
    {
        scene = next;
        switch (next.name[1])
        {
            case 'D': //this naming system is limited to 1 digit scenes, not a problem for a small game but good to have in mind
                Player = GameObject.Find("Player");
                Playerscr = Player.GetComponent<PlayerScript>();
                Timer = GameObject.Find("Timer");
                Timerscr = Timer.GetComponentInChildren<TimerScript>();
                Playerscr.ChangePlayerHealth(gc_plmaxhealth, true); // true means replace value instead of add
                Playerscr.ChangePlayerDamage(gc_pldamage, true);
                SetupDay();
                break;
            case 'N':
                FightButton = GameObject.Find("Button").GetComponent<Button>();
                FightButton.onClick.AddListener(Fight);
                ButtonText = FightButton.GetComponentInChildren<TMP_Text>();
                Knightanim = GameObject.Find("PixelKnight_0").GetComponent<Animator>();
                Skeletonanim = GameObject.Find("Skeleton").GetComponent<Animator>();
                PHealth = GameObject.Find("PHealth").GetComponent<TMP_Text>();
                PDamage = GameObject.Find("PDamage").GetComponent<TMP_Text>();
                EHealth = GameObject.Find("EHealth").GetComponent<TMP_Text>();
                EDamage = GameObject.Find("EDamage").GetComponent<TMP_Text>();
                EndText = GameObject.Find("FightEnd").GetComponent<TMP_Text>();
                Turns = GameObject.Find("Turns").GetComponent<TMP_Text>();//is there a better way to do this?, probably but this would be behind a loading screen so it wouldnt matter much (plus its saved on memory)
                FightButton.gameObject.SetActive(true);
                SetupNight();
                SetupNPlayer();
                EditNText();
                break;
        }
    }
    void SetupNPlayer() //update player health and damage
    {
        gc_plmaxhealth = Playerscr.GetPlayerHealth();
        gc_pldamage = Playerscr.GetPlayerDamage();  
        if(gc_firstnight) 
        {
            gc_plmaxhealth++;
            gc_pldamage++;
            gc_plcurrenthealth = gc_plmaxhealth;
            gc_firstnight = false;
            gc_notfirstloop = true;
        }
    }
    void SetupNight() // setup enemy depending on stage
    {
        switch (scene.name)
        {
            case "1N":
                if (!gc_notfirstloop) { gc_firstnight = true; } //make sure player gets health
                 gc_enemydamage = 1;
                 gc_enemyhealth = 5; 
                 gc_turns = 3;
                 break;
            case "2N": //not implemented

                break;
            case "3N": //not implemented

                break;
            default:
                print("not night");
                break;
        }
    }
    public void SetupDay() //setup timer depending on stage (could be useful for more mechanics)
    {
        switch (scene.name)
        {
            case "1D":
                Timerscr.StartTimer(120d);
                break;
            case "2D":
                break;
            case "3D":
                break;
            default :
                break;
        }
    }
    int CheckCombat() // 0 = fight still going - 1(draw) 2(enemy killed) and 4(timeout) are player wins - 3 is defeat
    {
        if (gc_plcurrenthealth <= 0 && gc_enemyhealth <= 0)
        {
            return 1;
        }
        else if (gc_plcurrenthealth <= 0)
        {
            return 3;
        }
        else if ( gc_enemyhealth <= 0)
        {
            return 2;
        }
        else if (gc_turns <= 0) 
        {
            return 4; 
        }
        else  { return 0; }  
    }
    async void DayEnd(int status) // 
    {
        switch (status)
        {
            case 1:
                EndText.text = "The enemy is forced to retreat";
                await Task.Delay(1500);
                SceneControllerScript.SceneLoader("Level Select");
                break;
            case 2:
                EndText.text = "The enemy is defeated";
                await Task.Delay(2000);
                SceneControllerScript.SceneLoader("Level Select");
                break;
            case 3:
                EndText.text = "You are defeated";
                gc_plmaxhealth = 1; //resetting values
                gc_pldamage = 1;
                await Task.Delay(1500);
                SceneControllerScript.SceneLoader("MainMenu");
                break;
            case 4:
                EndText.text = "The enemy retreats";
                await Task.Delay(1500);
                SceneControllerScript.SceneLoader("Level Select");
                break;
        }
    }
    void NextTurn() //separating combat into another script would be a good idea if this project gets bigger
    {
        int status = CheckCombat();
        if (status != 0) { DayEnd(status); return; }
        else { FightButton.gameObject.SetActive(true); }
    }
    void Fight() //fight button pressed
    {
        FightButton.gameObject.SetActive(false);
        gc_turns--;
        Knightanim.SetBool("Attacking", true);
    }
    void EditNText()
    {
        PHealth.text = "Health = " + gc_plcurrenthealth;
        PDamage.text = "Damage = " + gc_pldamage;
        EHealth.text = "Health = " + gc_enemyhealth;
        EDamage.text = "Damage = " + gc_enemydamage;
        Turns.text = "Turns left = " + gc_turns;
    }
    public void KnightAttacked()
    {
        Knightanim.SetBool("Attacking", false);
        gc_enemyhealth -= gc_pldamage;
        EditNText();
        if (gc_enemyhealth <= 0) { Skeletonanim.SetBool("Dead", true); NextTurn(); }
        else Skeletonanim.SetBool("Attacking", true);
    }
    public void SkeletonAttacked()
    {
        Skeletonanim.SetBool("Attacking", false);
        gc_plcurrenthealth -= gc_enemydamage;
        if ( gc_plcurrenthealth <= 0) { Knightanim.SetBool("Dead", true); }
        EditNText();
        NextTurn();
    }
}
