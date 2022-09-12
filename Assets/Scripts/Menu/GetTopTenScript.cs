using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GetTopTenScript : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] trophies;
    public void Start()
    {
        StartCoroutine(WebPost.GetTop10Players(players,trophies));
    }
}
