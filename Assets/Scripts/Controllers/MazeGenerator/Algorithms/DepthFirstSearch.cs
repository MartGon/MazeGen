using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearch : MazeAlgorithm
{
    public DepthFirstSearch(int rows, int cols, ref MazeCell[,] cells) : base(rows, cols, ref cells)
    {
    }

    public override void generateMaze()
    {
        // Implementing Depth First Search Algorithm
        Stack<MazeCell> stack = new Stack<MazeCell>();
        int totalCells = rows * cols;
        int checkedCellCount = 0;

        // Step 1 - We choose a starting cell
        MazeCell currentCell = cells[0, 0];

        currentCell.alreadyChecked = true;
        checkedCellCount++;
        stack.Push(currentCell);

        currentCell.floor.SetActive(false);
        currentCell.startPoint.SetActive(true);
        MazeCell neighborCell;
        while (true)
        {
            // Step 2 - A random neighbour cell is picked
            neighborCell = pickValidNeighbor(currentCell);
            //neighborCell = PickRandomValidNeighbour(currentCell);
            if (neighborCell == null)
            {
                // Pick the previous cell in case there was not valid neighbour
                //Debug.Log("Estoy en currentCell : " + currentCell.row + currentCell.col);
                currentCell = stack.Pop();
                //Debug.Log("Retrocediendo a " + currentCell.row + currentCell.col);
                continue;
            }

            // Step 3 - Break Wall in between
            breakWallInBetween(currentCell, neighborCell);
           // Debug.Log("Estoy en currentCell : " + currentCell.row + currentCell.col);
            //Debug.Log("Avanzando a " + neighborCell.row + neighborCell.col);

            if (!stack.Contains(currentCell))
                stack.Push(currentCell);
            stack.Push(neighborCell);

            // Step 4 - Check neighbour cell
            neighborCell.alreadyChecked = true;
            checkedCellCount++;
            //Debug.Log("El total de celdas checkeadas son " + checkedCellCount);
            if (checkedCellCount == totalCells)
                return;
            

            // Step 5 - Neighbor becomes current Cell
            currentCell = neighborCell;
        }

    }

}
