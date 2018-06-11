using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze {

    // Private params
    private int rows;
    private int cols;
    private MazeCell[,] cells;
    private MazeAlgorithm alg;

    // Const params
    private const float MazeCellDistance = 2.75f;

    // Misc params
    Transform mazeCell;

    public Maze(Transform mazeCell, int rows, int cols, MazeGenerator.GenAlgorithm alg)
    {
        this.mazeCell = mazeCell;
        this.rows = rows;
        this.cols = cols;
        cells = new MazeCell[rows, cols];
        this.alg = GetAlgorithmById(alg);
    }

    // Generate a maze template
    public void init()
    {
        Transform currentMazeCellGO = null;
        MazeCell currentMazeCell = null;

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                // Create a single maze cell
                currentMazeCellGO = GameObject.Instantiate(mazeCell, new Vector3(j * MazeCellDistance, 0, -i * MazeCellDistance), Quaternion.identity);
                currentMazeCellGO.gameObject.name = "MazeCell" + i + j;
                currentMazeCell = currentMazeCellGO.gameObject.GetComponentInChildren<MazeCell>();
                if (PlayerPrefs.GetInt("fog") == 0)
                    currentMazeCell.ceiling.SetActive(false);

                currentMazeCell.row = i;
                currentMazeCell.col = j;
                cells[i, j] = currentMazeCell;

                // Fixes for first column or row
                if (currentMazeCell)
                {
                    // First Row
                    if (i == 0)
                    {
                        Debug.Log("Activating first row");
                        currentMazeCell.wallN.SetActive(true);
                    }   // First col
                    if (j == 0)
                    {
                        Debug.Log("Activating first col");
                        currentMazeCell.wallW.SetActive(true);
                    }
                }
            }

        // Placing start and win point
        cells[0, 0].startPoint.SetActive(false);
        cells[0, 0].startPoint.SetActive(true);
        cells[rows - 1, cols - 1].floor.SetActive(false);
        cells[rows - 1, cols - 1].finishPoint.SetActive(true);
    }

    // Generate the maze itself
    public void generate()
    {
        alg.generateMaze();
    }

    public MazeCell getLastCell()
    {
        return cells[rows - 1, cols - 1];
    }

    // Create MazeAlgorithm object by enum value
    private MazeAlgorithm GetAlgorithmById(MazeGenerator.GenAlgorithm alg)
    {
        switch(alg)
        {
            case MazeGenerator.GenAlgorithm.MAZE_ALGORITHM_DEPTH_FIRST_SEARCH:
                return new DepthFirstSearch(rows, cols, ref cells);
            case MazeGenerator.GenAlgorithm.MAZE_ALGORITHM_PRIMS:
                return new PrimsAlgorithm(rows, cols, ref cells);
            case MazeGenerator.GenAlgorithm.MAZE_ALGORITHM_RECURSIVE_DIVISION:
                return new RecursiveDivision(rows, cols, ref cells);
            case MazeGenerator.GenAlgorithm.MAZE_ALGORITHM_NONE:
                break;
        }

        return null;
    }
    
    // Bolofos' Methods
    public MazeCell PickRandomValidNeighbour(MazeCell currentCell)
    {
        ArrayList neigh = getNeighbours(currentCell);
        ArrayList valids = new ArrayList();
        foreach (var n in neigh)
        {
            MazeCell neighcell = n as MazeCell;
            if (!neighcell.alreadyChecked)
                valids.Add(n);

        }
        if (valids.Count == 0)
            return null;
        int iChoice = Random.Range(0, valids.Count);
        return valids[iChoice] as MazeCell;
    }

    public ArrayList getNeighbours(MazeCell current)
    {
        Vector2[] VonNeighbour = new Vector2[] { new Vector2(1,0), new Vector2(-1, 0) , new Vector2(0, 1), new Vector2(0, -1) };
        ArrayList neighbours = new ArrayList();
        
        foreach (Vector2 n in VonNeighbour)
        {
            int x = current.row + (int)n.x;
            int y = current.col + (int)n.y;
            if(0 <= x && x < rows && 0 <= y && y < cols )
                neighbours.Add(cells[x,y]);
        }
        
        return neighbours;

    }
}
