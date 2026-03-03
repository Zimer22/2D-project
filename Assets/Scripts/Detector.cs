using UnityEngine;

public class Detector : MonoBehaviour
{
    public bool d_detect = false;
    [SerializeField] float d_timer;
    private void OnEnable()
    {
        d_detect = false;
    }
    public bool Onground()
    {
        if (d_timer > 0)
            return false;
        return d_detect;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        d_detect = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        d_detect = false;
    }

    void Update()
    {
        d_timer -= Time.deltaTime;
    }
    public void Disable(float duration)
    {
        d_timer = duration;
    }
    public bool CheckTimer()
    {
        return d_timer > 0; 
    }
}
