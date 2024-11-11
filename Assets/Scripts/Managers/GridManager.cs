namespace Managers
{
    using System.Collections.Generic;
    using UnityEngine;
    using Grid;

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

        private readonly List<LogicGrid> gridsPos = new List<LogicGrid>();

        private void Start()
        {
            GenerateGridPos();
        }

        public void SetGridata(GameObject plant)
        {
            LogicGrid grid = GetGridByMouse();
            grid.Plant = plant;
            grid.IsPlanted = true;
        }

        public LogicGrid GetGridByRowIndex(GridRowEnum rowIndex)
        {
            return gridsPos[(int)rowIndex * 9];
        }

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
            int index = -1;
            for (int i = 0; i < gridsPos.Count; i++)
            {
                float tempDis = Vector3.Distance(gridsPos[i].PointWorldPos, worldPos);
                if (tempDis < distance)
                {
                    distance = tempDis;
                    index = i;
                }
            }

            //Debug.Log(index);
            return index != -1 ? gridsPos[index] : null;
        }

        private void GenerateGridPos()
        {
            for (int col = 0; col < 9; col++)
            {
                for (int row = 0; row < 5; row++)
                {
                    LogicGrid grid = new LogicGrid();
                    grid.IsPlanted = false;
                    grid.Plant = null;
                    grid.PointWorldPos = new Vector2(-7f + (col * 1.4f), -3.75f + (row * 1.7f));
                    gridsPos.Add(grid);
                }
            }
        }

        public void ClearAllPlant()
        {
            for (int i = 0; i < gridsPos.Count; i++)
            {
                if (gridsPos[i].Plant != null)
                {
                    Destroy(gridsPos[i].Plant.gameObject);
                    gridsPos[i].Plant = null;
                    gridsPos[i].IsPlanted = false;
                }
            }
        }
    }
}