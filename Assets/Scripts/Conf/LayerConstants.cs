
using UnityEngine;

namespace Conf
{
    public static class LayerConstants
    {
        public static readonly int ZombieLayer = LayerMask.NameToLayer("Zombie");

        // Method to check if a layer is defined
        public static bool IsLayerDefined(int layer)
        {
            return layer >= 0;
        }
    }
}