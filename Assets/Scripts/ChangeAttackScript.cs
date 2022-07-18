using UnityEngine;
using UnityEngine.UI;

public class ChangeAttackScript : MonoBehaviour
{
    [SerializeField] private Text attackSwitch;
    [SerializeField] private Image attack;
    
    [SerializeField] private Sprite soulPushSprite;
    [SerializeField] private Sprite soulFragmentSprite;

    private WeaponScript ws;

    public GameObject soulPush;
    public GameObject soulFragment;
    
    

    private void Start()
    {
        ws = GetComponent<WeaponScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ws.getProjectile().Equals(soulPush))
            {
                ws.setProjectile(soulFragment);
                attackSwitch.text = "Soul fragment";
                attack.sprite = soulFragmentSprite;
            }
            else
            {
                ws.setProjectile(soulPush);
                attackSwitch.text = "Soul Push";
                attack.sprite = soulPushSprite;
            }
        }
    }
}