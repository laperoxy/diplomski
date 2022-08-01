using System;
using Unity.Netcode;
using UnityEngine;

public class ItemCollectionScript : NetworkBehaviour
{
    [SerializeField] private NetworkVariable<bool> networkGotKey = new NetworkVariable<bool>();
    public GameObject floatingText;
    private float lastPopupTime; //time in seconds
    private const float POPUP_COOLDOWN = 3;

    // city gate trigger audio
    public AudioClip SoundToPlay;
    public float Volume;
    private AudioSource audioToPlay;

    //public GameObject endGame;

    void Start()
    {
        audioToPlay = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
  
            if (collision.gameObject.CompareTag("key"))
            {
                if (IsServer)
                {
                    transitionKeyOwnershipToGrabber();
                    Destroy(collision.gameObject);
                    Destroy(GameObject.FindGameObjectWithTag("ClockTower"));
                }
                SetAndShowText("Acquired city gate key");
            }

            if (collision.gameObject.CompareTag("ClockTower"))
            {
                SetAndShowText("Looks like the door is locked \n" +
                               "I will have to find another way to the top");
            }

            if (!networkGotKey.Value && collision.gameObject.CompareTag("ClockTowerSign"))
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
                if (IsServer)
                {
                    gameObject.GetComponent<PlayerControlNew>().transform.position = new Vector3(-4.53f, 2.0f, 0);
                }
            }

            if (collision.gameObject.CompareTag("EndgameLight"))
            {
                //endGame.SetActive(true);
            }
    }

    private void transitionKeyOwnershipToGrabber()
    {
        networkGotKey.Value = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
            if (col.gameObject.CompareTag("CityEnterance"))
            {
                if (networkGotKey.Value)
                {
                    SetAndShowText("Used key to open the gate");
                    if (IsServer)
                    {
                        networkGotKey.Value = false;
                        Destroy(col.gameObject);
                    }
                    audioToPlay.PlayOneShot(SoundToPlay, Volume);
                }
                else
                {
                    SetAndShowText("City gate closed, first find the key");
                }
            }
    }

    private void updatePopupTime()
    {
        lastPopupTime = Time.time;
    }

    private void SetAndShowText(String text)
    {
        if (IsClient && IsOwner)
        {
            if (shouldShowText())
            {
                floatingText.GetComponentInChildren<TextMesh>().text = text;
                Instantiate(floatingText, transform.position, Quaternion.identity);
                updatePopupTime();
            }
        }
    }

    private bool shouldShowText()
    {
        return Time.time - POPUP_COOLDOWN > lastPopupTime;
    }
}