using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveDivision : MazeAlgorithm {

    public RecursiveDivision(int rows, int cols, ref MazeCell[,] cells) : base(rows, cols, ref cells)
    {
    }

    public override void generateMaze()
    {
        // Init variables
        MazeCell[,] smallerMatrix = null;
        MazeCell[,] biggerMatrix = null;
        ArrayList returnValue = null;
        Stack<MazeCell[,]> stack = new Stack<MazeCell[,]>();

        // Disable walls
        disableWalls();

        // Stablish first matrix
        smallerMatrix = cells;

        while (true)
        {
            while (smallerMatrix.GetLength(0) > 1 || smallerMatrix.GetLength(0) > 1)
            {
                returnValue = createRandomFrontier(smallerMatrix);
                smallerMatrix = returnValue[0] as MazeCell[,];
                biggerMatrix = returnValue[1] as MazeCell[,];

                stack.Push(biggerMatrix);
            }
            if (stack.Count == 0)
                return;

            smallerMatrix = stack.Pop();
        }
    }

    // RecursiveDivision starts assuming all walls are disabled, so we do so
    private void disableWalls()
    {
        for(int i = 0; i < rows; i ++)
            for(int j = 0; j < cols; j++)
            {
                if(j != (cols - 1))
                    cells[i, j].wallE.SetActive(false);
                if(i != 0)
                    cells[i, j].wallN.SetActive(false);
                if(j != 0)
                    cells[i, j].wallW.SetActive(false);
                if(i != (rows - 1))
                    cells[i, j].wallS.SetActive(false);
            }
    }

    // Siempre son al Este y Sur
    private ArrayList createRandomFrontier(MazeCell[,] currentSection)
    {
        ArrayList subSets = new ArrayList();

        int cRows = currentSection.GetLength(0);
        int cCols = currentSection.GetLength(1);

        int traceDirection = Random.Range(0, 2);
        int tracePosition = -1;
        int randomPass = -1;

        // Arreglos por si no se puede alguna de las dos
        if (cRows == 1)
            traceDirection = 1;
        else if (cCols == 1)
            traceDirection = 0;

        // Línea horizontal
        if(traceDirection == 0)
        {
            // Elegimos un paso entre la frontera de forma aleatoria
            randomPass = Random.Range(0, cCols);
            // Elegimos un muro de frontera entre todos los posibles
            tracePosition = Random.Range(0, cRows - 1);
            for (int i = 0; i < cCols; i++)
                if(i != randomPass)
                    currentSection[tracePosition, i].wallS.SetActive(true);

            // Creamos las submatrices generadas
            MazeCell[,] subMatrix1 = new MazeCell[tracePosition + 1, cCols];
            MazeCell[,] subMatrix2 = new MazeCell[cRows - (tracePosition + 1), cCols];

            for (int i = 0; i < subMatrix1.GetLength(0); i++)
                for (int j = 0; j < cCols; j++)
                        subMatrix1[i, j] = currentSection[i, j];

            for (int i = subMatrix1.GetLength(0); i < currentSection.GetLength(0); i++)
                for (int j = 0; j < cCols; j++)
                    subMatrix2[i - subMatrix1.GetLength(0), j] = currentSection[i, j];

            // Colocamos la menor primero
            if (subMatrix1.GetLength(0) * subMatrix1.GetLength(1) > subMatrix2.GetLength(0) * subMatrix2.GetLength(1))
            {
                subSets.Add(subMatrix2);
                subSets.Add(subMatrix1);
            }
            else
            {
                subSets.Add(subMatrix1);
                subSets.Add(subMatrix2);
            }
        }
        // Línea vertical
        else
        {
            randomPass = Random.Range(0, cRows);
            tracePosition = Random.Range(0, cCols - 1);
            for (int i = 0; i < cRows; i++)
                if (i != randomPass)
                    currentSection[i, tracePosition].wallE.SetActive(true);

            // Creamos las submatrices generadas
            MazeCell[,] subMatrix1 = new MazeCell[cRows, tracePosition + 1];
            MazeCell[,] subMatrix2 = new MazeCell[cRows, cCols - (tracePosition + 1)];

            for (int j = 0; j < cRows; j++)
                for (int i = 0; i < subMatrix1.GetLength(1); i++)
                    subMatrix1[j, i] = currentSection[j, i];

            for (int j = 0; j < cRows; j++)
                for (int i = subMatrix1.GetLength(1); i < currentSection.GetLength(1); i++)
                    subMatrix2[j, i - subMatrix1.GetLength(1)] = currentSection[j, i];

            // Colocamos la menor primer
            if (subMatrix1.GetLength(0) * subMatrix1.GetLength(1) > subMatrix2.GetLength(0) * subMatrix2.GetLength(1))
            {
                subSets.Add(subMatrix2);
                subSets.Add(subMatrix1);
            }
            else
            {
                subSets.Add(subMatrix1);
                subSets.Add(subMatrix2);
            }
        }

        return subSets;
    }
}
