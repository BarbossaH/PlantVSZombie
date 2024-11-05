using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    /*to preset a list to store the position of each grid, and when the moust click the screen, then campare to each grid and find the nearest
    grid at the selected grid, and then put a plant on this position
     */

    private List<Grid> gridsPos = new List<Grid>();
    [SerializeField] GameObject plantPrefab;
    private void Start()
    {
        GenerateGridPos();
    }

    private void OnMouseDown()
    {
        Plant();
    }

    private void Plant()
    {
        //TODO: check the state of mouse if selecting a plant
        Vector2 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        float distance = 1000f;
        int index = 0;
        for (int i = 0; i < gridsPos.Count; i++)
        {
            float tempDis = Vector3.Distance(gridsPos[i].PointPos, worldPos);
            if (tempDis < distance)
            {
                distance = tempDis;
                index = i;
            }
        }
        if (!gridsPos[index].IsPlanted)
        {
            GameObject ob = GameObject.Instantiate(plantPrefab, gridsPos[index].PointPos, Quaternion.identity, transform);
            gridsPos[index].IsPlanted=true;
        }
    }
    private void GenerateGridPos()
    {
        for (int col = 0; col < 9; col++)
        {
            for (int row = 0; row < 5; row++)
            {
                Grid grid = new Grid();
                grid.IsPlanted = false;
                grid.PointPos = new Vector2(-7f + (col * 1.4f), -3.75f + (row * 1.7f));
                gridsPos.Add(grid);
            }
        }
    }

    private void CreatePlantsTest()
    {
        for (int col = 0; col < 9; col++)
        {
            for(int row = 0; row < 5; row++)
            {
              GameObject ob=  GameObject.Instantiate(plantPrefab, new Vector3(-7f + (col * 1.4f), -3.75f + (row * 1.7f), 0),Quaternion.identity, transform);
                Debug.Log(ob.transform.position);
            }
        }
    }
}
