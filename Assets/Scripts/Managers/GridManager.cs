using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Plant;
using UnityEngine;
using Grid;
using Conf;

namespace Managers
{
    
    public enum GridRowEnum
    {
        FirstRow = 0,
        SecondRow,
        ThirdRow,
        ForthRow,
        FifthRow
    }

    public enum GridColEnum
    {
        FirstCol = 0,
        SecondCol,
        ThirdCol,
        ForthCol,
        FifthCol,
        SixthCol,
        SeventhCol,
        EighthCol,
        NinethCol
    }

    public class GridManager : SingletonMono<GridManager>
    {
        /*to preset a list to store the position of each grid, and when the moust click the screen, then campare to each grid and find the nearest
        grid at the selected grid, and then put a plant on this position
         */
        private readonly Vector2 gridSize = new Vector2(1.4f, 1.65f);
        private readonly Vector2 firstGridPosition = new Vector2(-7f, -3.75f);
        // private readonly List<LogicGrid> gridsPos = new List<LogicGrid>();
        private Dictionary<int, List<LogicGrid>> allGridsDic = new Dictionary<int, List<LogicGrid>>();
        private readonly int rowCount = 5;
        private readonly int colCount = 9;
        private void Start()
        {
            GenerateGridPos();
        }

        // private void FixedUpdate()
        // {
        //     // DetectZombieFixedUpdate();
        // }


        public LogicGrid GetGridByMouse()
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            LogicGrid grid = GetGridByWorldCoordinate(worldPos);
            return grid;
        }

        public LogicGrid GetGridByWorldCoordinate(Vector3 worldPos)
        {
            worldPos.z = 0;
            float
                distance = 1.0f; //because the width and height is close to 1.5f, so if the moust is out of this range, it could return null
            int rowIndex = -1;
            int colIndex = -1;
            for (int col = 0; col < colCount; col++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    float tempDis = Vector3.Distance(allGridsDic[row][col].PointWorldPos, worldPos);
                    if (tempDis < distance)
                    {
                        distance = tempDis;
                        rowIndex = row;
                        colIndex = col;
                    }
                }
            }
            //Debug.Log(index);
            return (rowIndex != -1&&colIndex!=-1) ? allGridsDic[rowIndex][colIndex] : null;
        }

        private void GenerateGridPos()
        {
            for (int row = 0; row < rowCount; row++)
            {
                allGridsDic[row] = new List<LogicGrid>();
                for (int col = 0; col < colCount; col++)
                {
                    LogicGrid grid = new LogicGrid();
                    grid.IsPlanted = false;
                    grid.Plant = null;
                    grid.RowIndex = row;
                    grid.PointWorldPos = new Vector2(firstGridPosition.x + (col * gridSize.x), firstGridPosition.y + (row * gridSize.y));
                    // gridsPos.Add(grid);
                    allGridsDic[row].Add(grid);
                }
            }
        }

        public void ClearAllPlant()
        {
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    if (allGridsDic[row][col].Plant is not null)
                    {
                        Destroy(allGridsDic[row][col].Plant);
                        allGridsDic[row][col].Plant = null;
                        allGridsDic[row][col].IsPlanted = false;
                    }
                }
            }
        }

       

    }
}