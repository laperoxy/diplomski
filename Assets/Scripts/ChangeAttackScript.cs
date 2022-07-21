using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAttackScript : NetworkBehaviour
{
    [SerializeField] private Text attackSwitch;
    [SerializeField] private Image attack;
    
    [SerializeField] private Sprite soulPushSprite;
    [SerializeField] private Sprite soulFragmentSprite;

    [SerializeField]
    private WeaponScript weaponScript;

    public GameObject soulPush;
    public GameObject soulFragment;

    // Update is called once per frame
    void Update()
    {
        if (IsClient && IsOwner && Input.GetKeyDown(KeyCode.E))
        {
            if (weaponScript.getAndSetProjectile(soulPush,soulFragment))
            {
                attackSwitch.text = "Soul fragment";
                attack.sprite = soulFragmentSprite;
            }
            else if(weaponScript.getAndSetProjectile(soulFragment,soulPush))
            {
                attackSwitch.text = "Soul Push";
                attack.sprite = soulPushSprite;
            }
        }
    }
}