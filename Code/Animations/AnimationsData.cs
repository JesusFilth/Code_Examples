using UnityEngine;

public class AnimationsData
{
    public class Character
    {
        public static int IsWalk = Animator.StringToHash(nameof(IsWalk));
        public static int Jump = Animator.StringToHash(nameof(Jump));
        public static int Attack = Animator.StringToHash(nameof(Attack));
        public static int Craft = Animator.StringToHash(nameof(Craft));
        public static int Idel = Animator.StringToHash(nameof(Idel));
        public static int Die = Animator.StringToHash(nameof(Die));
    }
}
