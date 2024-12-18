using UnityEngine;

namespace Characters.Attributes
{
    public static class ZombieAnimationParams
    {
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Die = Animator.StringToHash("Die");
        public static readonly int LowHealth = Animator.StringToHash("LowHealth");
    }
}