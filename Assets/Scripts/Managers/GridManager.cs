using System.Collections.Generic;
using UnityEngine;

public class GridManager : SingletonMono<GridManager>
{
    /*to preset a list to store the position of each grid, and when the moust click the screen, then campare to each grid and find the nearest
    grid at the selected grid, and then put a plant on this position
     */

    private List<Grid> gridsPos = new List<Grid>();
    private void Start()
    {
        GenerateGridPos();
    }

    //private void OnMouseDown()
    //{
    //    Plant();
    //}
     public void SetGridata(GameObject plant)
    {
        Grid grid = GetGridByMouse();
        grid.plant = plant;
        grid.IsPlanted = true; 
    }

    public Grid GetGridByMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Grid gird = GetGridByWorldCoordinate(worldPos);
        return gird;
    }

    public Grid GetGridByWorldCoordinate(Vector3 worldPos)
    {
        worldPos.z = 0;
        float distance = 1.0f; //because the width and height is close to 1.5f, so if the moust is out of this range, it could return null
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
        return index != -1 ? gridsPos[index] : null;
    }

    private void GenerateGridPos()
    {
        for (int col = 0; col < 9; col++)
        {
            for (int row = 0; row < 5; row++)
            {
                Grid grid = new Grid();
                grid.IsPlanted = false;
                grid.plant = null;
                grid.PointWorldPos = new Vector2(-7f + (col * 1.4f), -3.75f + (row * 1.7f));
                gridsPos.Add(grid);
            }
        }
    }
}
