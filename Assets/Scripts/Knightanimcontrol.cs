using UnityEngine;

public class Knightanimcontrol : MonoBehaviour
{
    private Animator Knightanim;
    private Animator Skeletonanim;
    private AudioSource KnightAudio;
    private AudioSource SkeletonAudio;
    private GameController gameController; //this is dumb, should be able to make a animator controller for ALL objects. not one for each
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        Knightanim = GameObject.Find("PixelKnight_0").GetComponent<Animator>();
        Skeletonanim = GameObject.Find("Skeleton").GetComponent<Animator>();
        KnightAudio = GameObject.Find("PixelKnight_0").GetComponent<AudioSource>();
        SkeletonAudio = GameObject.Find("Skeleton").GetComponent<AudioSource>();
    }
    public void KnightAttackAnimStart()
    {
        Skeletonanim.SetBool("Damaged", true);
        SkeletonAudio.Play();
    }
    public void KnightAttackAnimEnd()
    {
        Skeletonanim.SetBool("Damaged", false);
        gameController.KnightAttacked();
    }
}
