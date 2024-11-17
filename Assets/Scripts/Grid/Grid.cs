using UnityEngine;

namespace Grid
{
    public class LogicGrid 
    {
        public bool IsPlanted { get; set; }
        public Vector3 PointWorldPos { get; set; }

        public GameObject Plant;
        
        public int RowIndex { get; set; }
    }
}

