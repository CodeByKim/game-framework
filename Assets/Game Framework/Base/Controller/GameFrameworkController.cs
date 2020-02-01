using UnityEngine;

namespace GameFramework
{
    public abstract class GameFrameworkController : MonoBehaviour
    {
        protected virtual void Awake()
        {
            ControllerManager.RegisterController(this);
        }
    }
}
