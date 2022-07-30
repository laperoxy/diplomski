using System;
using UnityEngine;

public class ItemCollectionScript : MonoBehaviour
{
    [SerializeField] private bool gotKey;
    public GameObject floatingText;
    private float lastPopupTime; //time in seconds
    private const float POPUP_COOLDOWN = 3; 
    
    // city gate trigger audio
    public AudioClip SoundToPlay;
    public float Volume;
    private AudioSource audioToPlay;
    
    [SerializeField] private GameObject victoryScreen = null;
    
    void Start()
    {
        audioToPlay = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            transitionKeyOwnershipToGrabber();
            Destroy(GameObject.FindGameObjectWithTag("key"));
            SetAndShowText("Acquired city gate key");
            Destroy(GameObject.FindGameObjectWithTag("ClockTower"));
        }

        if (collision.gameObject.CompareTag("ClockTower"))
        {
            SetAndShowText("Looks like the door is locked \n" +
                        "I will have to find another way to the top");
        }

        if (!gotKey && collision.gameObject.CompareTag("ClockTowerSign"))
        {
            SetAndShowText("There is the abandoned Clock tower \n" +
                           "Looks like there is something shiny on top");
        }

        if (collision.gameObject.CompareTag("House"))
        {
            SetAndShowText("Wait for the floating House to come around \n"
                + "Dont get wet!");
        }

        if (collision.gameObject.CompareTag("Teleporter"))
        {
            gameObject.GetComponent<PlayerControlNew>().transform.position = new Vector3(-4.53f, 2.0f, 0);
        }

        if (collision.gameObject.CompareTag("End"))
        {
            victoryScreen.SetActive(true);
        }
    }

    private void transitionKeyOwnershipToGrabber()
    {
        GameObject followedGameObject = Camera.main.GetComponent<FollowPlayerScript>().followedGameObject;
        if (gameObject.Equals(followedGameObject))
        { 
            gotKey = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("CityEnterance"))
        {
            if (gotKey)
            {
                SetAndShowText("Used key to open the gate");
                OpenCityGate();
            }
            else
            {
                SetAndShowText("City gate closed, first find the key");
            }
        }
    }

    private void OpenCityGate()
    {
        gotKey = false;
        Destroy(GameObject.FindGameObjectWithTag("CityEnterance"));
        audioToPlay.PlayOneShot(SoundToPlay,Volume);
    }

    private void updatePopupTime()
    {
        lastPopupTime = Time.time;
    }
    
    private void SetAndShowText(String text)
    {
        if (shouldShowText())
        {
            floatingText.GetComponentInChildren<TextMesh>().text = text;
            Instantiate(floatingText, transform.position, Quaternion.identity);
            updatePopupTime();
        }
    }

    private bool shouldShowText()
    {
        return Time.time - POPUP_COOLDOWN > lastPopupTime;
    }
}