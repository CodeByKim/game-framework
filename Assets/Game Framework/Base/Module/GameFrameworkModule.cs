using System;

namespace GameFramework
{
    public abstract class GameFrameworkModule
    {
        public virtual int Prioruty => 0;

        public abstract void Update(float elapseSeconds, float realElapseSeconds);

        public abstract void Shutdown();
    }
}
