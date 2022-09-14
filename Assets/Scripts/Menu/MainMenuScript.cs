using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        //Debug.Log("Exited game");
        Application.Quit();
    }

    public void openPdf()
    {
        System.Diagnostics.Process.Start("UputstvoZaUpotrebu.pdf", @"C:\Program Files\Adobe\Acrobat DC\Acrobat\AcroRd32.exe");
    }
}