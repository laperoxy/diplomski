using UnityEngine;
using UnityEngine.UI;

public class ChangeAttackScript : MonoBehaviour
{

    [SerializeField] public Text attackSwitch;
    [SerializeField] public Image attack;

    public ProjectileScript prefabSplash;
    public ProjectileScript prefabWave;

    public Sprite sprite1;
    public Sprite sprite2;

    private void Start()
    {
        MovementScript.setProjectilePrefab(prefabSplash);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if(attackSwitch.text.Equals("Water Splash"))
            {
                attackSwitch.text = "Wave Attack";
                attack.sprite = sprite2;
                MovementScript.setProjectilePrefab(prefabWave);
            }
            else
            {
                attackSwitch.text = "Water Splash";
                attack.sprite = sprite1;
                MovementScript.setProjectilePrefab(prefabSplash);
            }

        }
    }
}
