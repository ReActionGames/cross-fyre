using UnityEngine;

namespace CrossFyre.GameInput
{
    public interface IInputProvider
    {
        Vector3 GetInitialPosition();
        InputData GetInput();
    }
}