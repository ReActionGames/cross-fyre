using UnityEngine;

namespace CrossFyre.GameInput
{
    public struct InputData
    {
        public Vector2 input;

        public float floatThreshold;
        public float innerThreshold;
        public float deadThreshold;

        public InputState state;
        public Vector2 originPoint;
        public Vector2 touchPoint;


        public float Direction
        {
            get
            {
                if (state == InputState.NotTouching || state == InputState.Dead) return 0f;

                var vector = touchPoint - originPoint;
                var angle = Vector2.SignedAngle(Vector2.right, vector);
                return angle;
            }
        }
        
    }

    public enum InputState
    {
        NotTouching,
        Dead,
        Inner,
        Normal,
        Sprint
    }
}