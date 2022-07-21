using UnityEngine;
using UnityEngine.UI;

public class ChangeAttackScript : MonoBehaviour
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
        if (Input.GetButtonDown("Fire3"))
        {
            if (weaponScript.getProjectile().Equals(soulPush))
            {
                weaponScript.setProjectile(soulFragment);
                attackSwitch.text = "Soul fragment";
                attack.sprite = soulFragmentSprite;
            }
            else
            {
                weaponScript.setProjectile(soulPush);
                attackSwitch.text = "Soul Push";
                attack.sprite = soulPushSprite;
            }
        }
    }
}