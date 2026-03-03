using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D        PlayerRB;
    private Detector           PlayerDetector;
    private Animator           PlayerAnim;
    private ParticleSystem     PlayerParticles;
    private AudioSource        PlayerAudio;
    private bool               p_grounded;
    private bool               p_space;
    private int                p_extrajumps;
    private int                p_maxjumps;
    private float              p_inputx;
    [SerializeField] float     p_speed = 4.0f;
    [SerializeField] float     p_airctrlmult = 0.2f;
    [SerializeField] float     p_decelmult = 2.0f;
    [SerializeField] float     p_jumpforce = 7.5f;
    [SerializeField] float     p_jumptiming = 0.2f;
    [SerializeField] float     p_maxspeed = 4.0f;
    //-------------player stats-----------------------
    [SerializeField] int p_damage; 
    [SerializeField] int p_health;
    [SerializeField] bool p_boots;

    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        PlayerAnim = GetComponent<Animator>();
        PlayerDetector = GetComponentInChildren<Detector>();
        PlayerParticles = GetComponentInChildren<ParticleSystem>();
        PlayerAudio = GetComponent<AudioSource>();  
    }
    void Update()
    {
        if (PlayerDetector.Onground())
        {
            p_grounded = true;
            PlayerAnim.SetBool("Midair", false);
            if (p_boots) { p_extrajumps = p_maxjumps; }
        }
        else
        {
            p_grounded = false;
            PlayerAnim.SetBool("Midair", true);
        }

        float inputx = UnityEngine.Input.GetAxisRaw("Horizontal"); // Input detection
        p_inputx = inputx;
        p_space = UnityEngine.Input.GetKey(KeyCode.Space);
    }
    private void FixedUpdate()
    {
        if (p_inputx > 0) // Rotation + animations + deceleration
        {
            transform.localScale = new Vector3(6.5f, 6.5f, 6.5f);
            PlayerAnim.SetBool("Moving", true);
        }
        else if (p_inputx < 0)
        {
            transform.localScale = new Vector3(-6.5f, 6.5f, 6.5f);
            PlayerAnim.SetBool("Moving", true);
        }
        else
        {
            if (p_grounded) { PlayerRB.AddForceX(-PlayerRB.linearVelocityX * p_decelmult); }
            ;
            PlayerAnim.SetBool("Moving", false); //maybe add stopping animation to make inertia look better
        }
        if (p_grounded && PlayerRB.linearVelocityX < p_maxspeed && PlayerRB.linearVelocityX > -p_maxspeed)// X axis movement + air control + max speed
        {
            PlayerRB.AddForceX(p_inputx * p_speed);
        }
        else if (!p_grounded) { PlayerRB.AddForceX(p_inputx * p_speed * p_airctrlmult); }
            
        if (p_grounded && p_space) //jumping
        {
            PlayerRB.AddForceY(Convert.ToInt32(p_space) * p_jumpforce);
            PlayerDetector.Disable(Convert.ToInt32(p_space) * p_jumptiming);
            PlayerAudio.Play();
        }
        else if(p_extrajumps > 0 && p_space && !PlayerDetector.CheckTimer())
        {
            PlayerRB.AddForceY(Convert.ToInt32(p_space) * p_jumpforce);
            PlayerDetector.Disable(Convert.ToInt32(p_space) * p_jumptiming);
            PlayerAudio.Play();
            p_extrajumps--;
        }
    }   
    public void ChangePlayerHealth(int change ,bool replace)
    {
        if (!replace) { p_health += change; }
        else { p_health = change; }
    }
    public void ChangePlayerDamage(int change,bool replace)
    {
        if (!replace) { p_damage += change; }
        else { p_damage = change; }
    }
    public int GetPlayerHealth()
    {
        return p_health;
    }
    public int GetPlayerDamage()
    {
        return p_damage;
    }
    public void ChangePlayerBool(string target,bool change)
    {
        if (target == "p_boots")
        {
            p_boots = change;
            p_maxjumps = 1;
        }
    }
    public void callanimevent() //steps
    {
      PlayerParticles.Play();
      PlayerAudio.Play();
    }
}
