using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimsAlgorithm : MazeAlgorithm {

    public PrimsAlgorithm(int rows, int cols, ref MazeCell[,] cells) : base(rows, cols, ref cells)
    {
    }

    public override void generateMaze()
    {
        // Implementing Prim's Algorithm
        ArrayList setCells = new ArrayList();

        // Step 1 - Pick one randomCell
        MazeCell currentCell = pickRandomCell();
        MazeCell secondCell = null;

        // Step 2 - Add that cell to the set
        currentCell.alreadyChecked = true;
        setCells.Add(currentCell);

        while (true)
        {
            // Step 3 - Pick one unchecked neighbor from one of all among the current set
            while(true)
            {
                int randomIndex = Random.Range(0, setCells.Count);
                currentCell = setCells[randomIndex] as MazeCell;
                secondCell = pickValidNeighbor(currentCell);

                if (secondCell)
                    break;
                else
                    setCells.Remove(currentCell);

                // Once we have no more cells to choose, then we have finished
                if (setCells.Count == 0)
                    return;
            }

            // Step 4 - Add the chosen neighbour to the set
            secondCell.alreadyChecked = true;
            setCells.Add(secondCell);

            // Step 5 - Break the wall in between them
            breakWallInBetween(currentCell, secondCell);
        }
    }
}
