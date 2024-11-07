using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingManager : SingletonMono<PlantingManager>
{
    // for planting and previewing the plant selected  

    //private GameObject plantInGrid;
    private Grid currentGrid;
    //get the right grid from grid manager ( grid is not null and not planted ),preveiw the plant
    private void Update()
    {
        SetCurrentGrid();
        PreviewPlant();
        if (Input.GetMouseButtonDown(0))
        {
            //plant a plant
            Plant();
            //notify ui refresh
        }
        else if (Input.GetMouseButtonDown(1)) {
            //cancel previewplant
            CancelPreviewPlant();
        }
    }

    private void Plant()
    {
        if (currentGrid != null&& currentGrid.plant!=null)
        {
            Debug.Log(1234);
            currentGrid.plant.GetComponent<PlantBase>().Plant();
            currentGrid.IsPlanted = true;
            currentGrid = null;
            MouseManager.Instance.CancelSelected();
            NotificationCenter.Instance.NotifyObserver(Event_Type.Planting_Event, PlantTypeEnum.SunFlower);
        }
    }

    private void SetCurrentGrid()
    {
        if (MouseManager.Instance.currentPlantType == PlantTypeEnum.None) return;
        Grid grid = GridManager.Instance.GetGridByMouse();
        if (grid != null ) { 
            if( grid != currentGrid)
            {
                //if mouse move to the next grid, the currentgrid preview plant should be destoried
                CancelPreviewPlant();
                currentGrid = grid;
            }           
        }
        else
        {
            //if the moust move too far, like at the edge of the screen
            CancelPreviewPlant();
            currentGrid = null;
        }
    }
   private void PreviewPlant() 
   {
        if (currentGrid != null )
        {
            if (currentGrid.IsPlanted) return;
            //show the preview of the plant in the grid
            if (currentGrid.plant == null)
            {
                GameObject prefab = PlantManager.Instance.GetPlantPrefbByType(MouseManager.Instance.currentPlantType);
                if (prefab != null) {
                    currentGrid.plant = GameObject.Instantiate(prefab, currentGrid.PointWorldPos, Quaternion.identity, PlantManager.Instance.transform);
                    currentGrid.plant.GetComponent<PlantBase>().ShowPlant(0.6f);
                    currentGrid.plant.GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
            }
        }
    }

    private void CancelPreviewPlant()
    {
        if (currentGrid != null && !currentGrid.IsPlanted)
        {
            Destroy(currentGrid.plant);
        }
    }

}
