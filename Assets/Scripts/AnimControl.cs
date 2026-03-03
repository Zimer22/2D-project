using UnityEngine;

public class AnimControl1 : MonoBehaviour
{
    private GameController gameController;
    void Start()
    {
       gameController = GameObject.Find("SceneController").GetComponent<GameController>();
    }
    public void KnightAttackAnimEnd()
    {
        gameController.KnightAttacked();
    }
    public void SkeletonAttackAnimEnd()
    {
        gameController.SkeletonAttacked();
    }
}
