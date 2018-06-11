using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    // Walls
    public GameObject wallN;
    public GameObject wallE;
    public GameObject wallW;
    public GameObject wallS;
    public GameObject floor;
    public GameObject ceiling;
    public GameObject startPoint;
    public GameObject finishPoint;

    // Position
    public int row = 0;
    public int col = 0;

    // Check flag
    public bool alreadyChecked = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMove>())
        {
            ceiling.SetActive(false);
        }
    }
}