namespace Managers
{
    using Conf;
    using UnityEngine;

    public class MouseManager : SingletonMono<MouseManager>
    {
        public PoolTypeEnum currentPlantType;

        private GameObject plantFMouse;

        //check mouse if selected or not
        private Camera mainCamera;
        private bool IsSelected { get; set; }
        public void SetMouseSelected(PoolTypeEnum plantType)
        {
            IsSelected = true;
   

            if (plantType != PoolTypeEnum.None)
            {
                plantFMouse = ObjectPoolManager.Instance.GetObject(plantType, Vector3.zero, Quaternion.identity);
                plantFMouse.GetComponent<SpriteRenderer>().sortingOrder = 2;
                currentPlantType = plantType;
            }
        }

        private void Start()
        {
            mainCamera= Camera.main;
            currentPlantType = PoolTypeEnum.None;
        }

        private void Update()
        {
            if (IsSelected)
            {
                SetPlantFollowMouse();
            }

           if (Input.GetMouseButtonDown(1))
           {
            //Cancel the selected state of the mouse
            CancelSelected();
           }

        }

        public void CancelSelected()
        {
            // Debug.Log("what");
            IsSelected = false;
            // Destroy(plantFMouse);
            ObjectPoolManager.Instance.ReturnObject(currentPlantType, plantFMouse);
            plantFMouse = null;
            currentPlantType = PoolTypeEnum.None;
        }

        private void SetPlantFollowMouse()
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            plantFMouse.transform.position = mouseWorldPos;
        }
    }
}