using UnityEngine;

namespace IzumiLib
{
    [DisallowMultipleComponent]
    public class DestroyFuncSupplier : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }

}