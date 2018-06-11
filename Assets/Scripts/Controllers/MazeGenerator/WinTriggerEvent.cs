using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTriggerEvent : MonoBehaviour {

    public InterfaceController interfaceController;
    public TimeController timeController;

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        PlayerMove player = other.GetComponentInChildren<PlayerMove>();

        if (player)
        {
            //Debug.LogError("Has ganado, enhorabuena!");

            // Spawn UI with winning text
            interfaceController.WinDialogPopUp();

            PlayerPrefs.SetInt("completedMazes", PlayerPrefs.GetInt("completedMazes") + 1);

            timeController.updateScore();

            gameObject.SetActive(false);
        }
    }
}
