
namespace Grid
{
    using UnityEngine;

    public class LogicGrid 
    {
        public bool IsPlanted { get; set; }
        public Vector3 PointWorldPos { get; set; }

        public GameObject Plant;
        
        public int RowIndex { get; set; }
    }
}

