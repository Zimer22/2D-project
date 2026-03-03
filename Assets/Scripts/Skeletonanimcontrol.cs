using UnityEngine;

public class Skeletonanimcontrol : MonoBehaviour
{
    private Animator Knightanim;
    private Animator Skeletonanim;
    private AudioSource KnightAudio;
    private AudioSource SkeletonAudio;
    private GameController gameController;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        Knightanim = GameObject.Find("PixelKnight_0").GetComponent<Animator>();
        Skeletonanim = GameObject.Find("Skeleton").GetComponent<Animator>();
        KnightAudio = GameObject.Find("PixelKnight_0").GetComponent<AudioSource>();
        SkeletonAudio = GameObject.Find("Skeleton").GetComponent<AudioSource>();
    }
    public void SkeletonAttackAnimStart()
    {
        Knightanim.SetBool("Damaged", true);
        KnightAudio.Play();
    }

    public void SkeletonAttackAnimEnd()
    {
        Knightanim.SetBool("Damaged", false);
        gameController.SkeletonAttacked();
    }
}
