using System;
using System.Collections.Generic;

namespace GameFramework
{
    public static class ModuleManager
    {
        private static readonly GameFrameworkLinkedList<GameFrameworkModule> gameFrameworkModules = new GameFrameworkLinkedList<GameFrameworkModule>();

        public static void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach(var module in gameFrameworkModules)
            {
                module.Update(elapseSeconds, realElapseSeconds);
            }
        }

        public static void Shutdown()
        {
            for(var current = gameFrameworkModules.Last; current != null; current = current.Previous)
            {
                current.Value.Shutdown();
            }

            gameFrameworkModules.Clear();
            //ReferencePool.ClearAll();            
        }
    }
}