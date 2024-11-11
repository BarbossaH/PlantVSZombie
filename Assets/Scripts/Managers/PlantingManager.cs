namespace Managers
{
    using Grid;
    using Characters.Plant;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using Conf;
    public class PlantingManager : SingletonMono<PlantingManager>
    {
        // for planting and previewing the plant selected  

        //private GameObject plantInGrid;
        private LogicGrid currentGrid;

        //get the right grid from grid manager ( grid is not null and not planted ),preview the plant
        private void Update()
        {
            SetCurrentGrid();
            PreviewPlant();
            if (Input.GetMouseButtonDown(0))
            {
                //plant a plant
                if (IsPointerOverUI()) return;
                Plant();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //cancel preview plant
                CancelPreviewPlant();
            }
        }

        //check pointer is on the ui or not to prevent the click event pass through ui
        private bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        private void Plant()
        {
            if (currentGrid != null && currentGrid.Plant != null && !currentGrid.IsPlanted)
            {
                currentGrid.Plant.GetComponent<PlantBase>().Plant();
                currentGrid.IsPlanted = true;
                //GridManager.Instance.SetGridData(currentGrid.plant);
                //notify observers, like ui
                NotificationCenter.Instance.NotifyObserver(EventTypeEnum.PlantingEvent,
                    MouseManager.Instance.currentPlantType);


                currentGrid = null;
                //change mouse state
                MouseManager.Instance.CancelSelected();
            }
        }

        private void SetCurrentGrid()
        {
            if (MouseManager.Instance.currentPlantType == PlantTypeEnum.None) return;
            LogicGrid grid = GridManager.Instance.GetGridByMouse();
            if (grid != null)
            {
                if (grid != currentGrid)
                {
                    //if mouse move to the next grid, the current grid preview plant should be destroyed
                    CancelPreviewPlant();
                    currentGrid = grid;
                }
            }
            else
            {
                //if the mouse move too far, like at the edge of the screen
                CancelPreviewPlant();
                currentGrid = null;
            }
        }

        private void PreviewPlant()
        {
            if (currentGrid != null)
            {
                if (currentGrid.IsPlanted) return;
                //show the preview of the plant in the grid
                if (currentGrid.Plant == null)
                {
                    GameObject prefab =
                        PlantManager.Instance.GetPlantPrefabByType(MouseManager.Instance.currentPlantType);
                    if (prefab != null)
                    {
                        currentGrid.Plant = GameObject.Instantiate(prefab, currentGrid.PointWorldPos,
                            Quaternion.identity, PlantManager.Instance.transform);
                        currentGrid.Plant.GetComponent<PlantBase>().ShowPlant(0.6f);
                        currentGrid.Plant.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    }
                }
            }
        }

        private void CancelPreviewPlant()
        {
            // if (currentGrid != null && !currentGrid.IsPlanted)
            if(currentGrid is {IsPlanted: false})
            {
                Destroy(currentGrid.Plant);
            }
        }

    }
}