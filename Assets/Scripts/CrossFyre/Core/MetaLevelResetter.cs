using CrossFyre.Interfaces;
using UnityEngine;

namespace CrossFyre.Core
{
    public class MetaLevelResetter : MonoBehaviour
    {
        private IResetable[] resetables;

        private void Start()
        {
            resetables = this.FindMonoBehavioursOfInterface<IResetable>();
        }

        public void ResetMetaLevel()
        {
            foreach (var resetable in resetables)
            {
                resetable.ResetObject();
            }
        }
    }
}
