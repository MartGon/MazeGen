using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

    // Public parameters
    public Transform mazeCell;
    public Transform winTrigger;
    public int rows;
    public int cols;
    public GenAlgorithm alg;
    public Camera cam;
    public GameObject player;
    public InterfaceController interfaceController;

    private const float offset = 3.327f;

    public enum GenAlgorithm
    {
        MAZE_ALGORITHM_DEPTH_FIRST_SEARCH,
        MAZE_ALGORITHM_PRIMS,
        MAZE_ALGORITHM_RECURSIVE_DIVISION,
        MAZE_ALGORITHM_NONE
    }

    void Start()
    {
        StartCoroutine(generateMaze());
    }

    IEnumerator generateMaze ()
    {
        
        // Getting variables from config
        rows = PlayerPrefs.GetInt("rows");
        cols = PlayerPrefs.GetInt("cols");
        alg = (GenAlgorithm)PlayerPrefs.GetInt("alg");
        if (PlayerPrefs.GetInt("seed") != 0)
            Random.InitState(PlayerPrefs.GetInt("seed"));

        interfaceController.updateSeedDisplay();

        // Generating maze
        Maze maze = new Maze(mazeCell, rows, cols, alg);
        maze.init();
        maze.generate();

        // Placing winner spot
        Vector3 winPosition = maze.getLastCell().transform.position;
        winTrigger.position = winPosition;

        int side = Mathf.Max(rows, cols);

        // Placing Top-Down View Camera
        float x = winPosition.x;
        float y =  3f * side * 3 / (2 * Mathf.Tan(Mathf.Deg2Rad * cam.fieldOfView));
        float z = winPosition.z / 2;
        cam.transform.position = new Vector3(winPosition.x/2, y, z);

        // Placing Player
        player.transform.position = new Vector3(0, 1.1275f, 0);
        player.SetActive(true);

        interfaceController.updateGenerationTime();

        yield return null;
    }
}
