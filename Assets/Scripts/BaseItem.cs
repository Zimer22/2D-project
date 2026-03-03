using Unity.VisualScripting;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    private PlayerScript Playerscr;
    [SerializeField] AudioClip clip;
    void Start()
    {   
    }
    public enum ItemTypes
    {
        Sword,
        Shield,
        Boots
    }
    [field:SerializeField] public ItemTypes Type {  get; set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Playerscr == null) { Playerscr = collision.GetComponent<PlayerScript>(); }
        switch (Type)
        {
            case ItemTypes.Sword:
                Playerscr.ChangePlayerDamage(1,false);
                AudioSource.PlayClipAtPoint(clip, this.gameObject.transform.position);
                this.gameObject.SetActive(false);
                break;
            case ItemTypes.Shield:
                Playerscr.ChangePlayerHealth(1,false);
                AudioSource.PlayClipAtPoint(clip, this.gameObject.transform.position);
                this.gameObject.SetActive(false);
                break;
            case ItemTypes.Boots:
                Playerscr.ChangePlayerBool("p_boots", true);
                AudioSource.PlayClipAtPoint(clip, this.gameObject.transform.position);
                this.gameObject.SetActive(false);
                break;

        }
    }
}
