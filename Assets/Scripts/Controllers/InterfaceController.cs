using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour {

    // Public params
    public GameObject Interface;
    public GameObject MenuDialog;
    public GameObject WinDialog;
    public TimeController timeController;
    public Camera TopCamera;
    public Toggle toggle;
    public Text elapsedGenerationTime;
    public Text seedNumberDisplay;

	// Use this for initialization
	void Start ()
    {
        MenuDialog.SetActive(false);
        WinDialog.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            MenuDialogPopUp();
	}

    public void goBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void keepPlayingButton()
    {
        MenuDialog.SetActive(false);
    }

    public void WinDialogPopUp()
    {
        WinDialog.SetActive(true);
    }

    public void MenuDialogPopUp()
    {
        MenuDialog.SetActive(true);
    }

    public void toggleTopCamera()
    {
        TopCamera.gameObject.SetActive(toggle.isOn);
    }

    public void updateGenerationTime()
    {
        elapsedGenerationTime.text = timeController.getElapsedGenerationTime().ToString() + " s";
    }

    public void updateSeedDisplay()
    {
        seedNumberDisplay.text = Random.seed.ToString();
    }
}
