using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeAlgorithm
{
    protected int rows = 0;
    protected int cols = 0;
    protected MazeCell[,] cells;

    public MazeAlgorithm(int rows, int cols, ref MazeCell[,] cells)
    {
        this.rows = rows;
        this.cols = cols;
        this.cells = cells;
    }

    public abstract void generateMaze();

    // Returns a random cell
    public MazeCell pickRandomCell()
    {
        int randomRow = Random.Range(0, rows);
        int randomCol = Random.Range(0, cols);

        return cells[randomRow, randomCol];
    }

    // Picks a neighbour that has not been checked 
    public MazeCell pickValidNeighbor(MazeCell currentCell)
    {
        MazeCell neighbor = null;
        ArrayList alreadyCheckedNeighbors = new ArrayList();

        // Getting random neighbors until one is valid, return null if there is none
        do
        {
            neighbor = pickRandomNeighbor(currentCell);
            if (!alreadyCheckedNeighbors.Contains(neighbor))
            {
                alreadyCheckedNeighbors.Add(neighbor);

                if (!neighbor.alreadyChecked)
                    return neighbor;

                //Debug.Log("Celda" + currentCell.row + neighbor.row + ":Añadido a vecinos checkeados " + neighbor.row + neighbor.col);
                if (alreadyCheckedNeighbors.Count == getNeighbourAmount(currentCell))
                {
                    // Debug.Log("Todos los vecinos de la celda " + currentCell.row + currentCell.col + " ya han sido checkeados");
                    return null;
                }
            }
        } while (neighbor.alreadyChecked);

        return neighbor;
    }

    // Picks a random existing neighbor
    public MazeCell pickRandomNeighbor(MazeCell cell)
    {
        MazeCell neighbor = null;
        int row = cell.row;
        int col = cell.col;

        do
        {
            // Pick random neighbour
            int randomValue = Random.Range(0, 4);

            // Choose neighbor by randomvalue
            switch (randomValue)
            {
                case 0:
                    if ((row + 1) >= rows)
                        break;
                    neighbor = cells[row + 1, col];
                    //Debug.Log("Escogemos arriba")
                    break;
                case 1:
                    if ((col + 1) >= cols)
                        break;
                    neighbor = cells[row, col + 1];
                    break;
                case 2:
                    if ((row - 1) < 0)
                        break;
                    neighbor = cells[row - 1, col];
                    break;
                default:
                    if ((col - 1) < 0)
                        break;
                    neighbor = cells[row, col - 1];
                    break;
            }

        } while (!neighbor);

        return neighbor;
    }

    // Break Wall between two cells
    public void breakWallInBetween(MazeCell currentCell, MazeCell neighbor)
    {
        int row1 = currentCell.row;
        int col1 = currentCell.col;
        int row2 = neighbor.row;
        int col2 = neighbor.col;

        int rowDif = Mathf.Abs(row1 - row2);
        int colDif = Mathf.Abs(col1 - col2);

        if (rowDif == 0 && colDif == 0)
        {
            Debug.Log("ERROR(breakWallInBetween): The two cells are the same!");
            return;
        }
        else if (rowDif > 1 || rowDif > 1 || (rowDif + colDif) > 1)
        {
            Debug.Log("ERROR(breakWallInBetween): Neighbour cell is not a valid neigbour cell");
            return;
        }

        // Si estamos en una fila superior
        if ((row2 - row1) > 0)
            cells[row1, col1].wallS.SetActive(false);
        else if ((row2 - row1) < 0)
            cells[row2, col2].wallS.SetActive(false);

        // Si estamos en una columna más a la derecha
        if ((col2 - col1) > 0)
            cells[row1, col1].wallE.SetActive(false);
        else if ((col2 - col1) < 0)
            cells[row2, col2].wallE.SetActive(false);

    }

    // Total amount of existing neighbours
    public int getNeighbourAmount(MazeCell cell)
    {
        if ((cell.col == 0 || cell.col == (cols - 1)) && (cell.row == 0 || cell.row == (rows - 1)))
        {
            //Debug.Log("El total de vecinos es 2, soy " + cell.row + cell.col);
            return 2;
        }
        else if (cell.col == 0 || cell.col == (cols - 1) || cell.row == 0 || cell.row == (rows - 1))
        {
            //Debug.Log("El total de vecinos es 3, soy " + cell.row + cell.col);
            return 3;
        }

        //Debug.Log("El total de vecinos es 4, soy " + cell.row + cell.col);
        return 4;
    }

    // Returns al neighbours of current cell
    public ArrayList getNeighbours(MazeCell current)
    {
        Vector2[] VonNeighbour = new Vector2[] { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
        ArrayList neighbours = new ArrayList();

        foreach (Vector2 n in VonNeighbour)
        {
            int x = current.row + (int)n.x;
            int y = current.col + (int)n.y;
            if (0 <= x && x < rows && 0 <= y && y < cols)
                neighbours.Add(cells[x, y]);
        }

        return neighbours;

    }

}
