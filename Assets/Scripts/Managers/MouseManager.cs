namespace Managers
{
    using Conf;
    using UnityEngine;

    public class MouseManager : SingletonMono<MouseManager>
    {
        public PlantTypeEnum currentPlantType;

        private GameObject plantFMouse;

        //check mouse if selected or not
        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            private set { isSelected = value; }
        }

        public void SetMouseSelected(PlantTypeEnum plantType)
        {
            IsSelected = true;
            GameObject currentPrefab = PlantManager.Instance.GetPlantPrefabByType(plantType);
            if (currentPrefab != null)
            {
                plantFMouse = GameObject.Instantiate(currentPrefab, Vector3.zero, Quaternion.identity,
                    GridManager.Instance.transform);
                plantFMouse.GetComponent<SpriteRenderer>().sortingOrder = 2;
                currentPlantType = plantType;
            }
            else
            {
                Debug.LogError("Cannot get the prefab. ");
            }
        }

        private void Start()
        {
            currentPlantType = PlantTypeEnum.None;
        }

        private void Update()
        {
            if (IsSelected)
            {
                SetPlantFollowMouse();
            }

            if (Input.GetMouseButtonDown(0))
            {
                //try to plan the current plant
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //Cancel the selected state of the mouse
                CancelSelected();
            }

        }

        public void CancelSelected()
        {
            // Debug.Log("what");
            IsSelected = false;
            Destroy(plantFMouse);
            plantFMouse = null;
            currentPlantType = PlantTypeEnum.None;
        }

        private void SetPlantFollowMouse()
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            plantFMouse.transform.position = mouseWorldPos;
        }
    }
}