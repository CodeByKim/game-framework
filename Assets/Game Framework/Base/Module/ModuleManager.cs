using System;
using System.Collections.Generic;

using UnityEngine;

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
            ReferencePool.ClearAll();            
        }

        public static T GetModule<T>() where T : class
        {
            Type moduleType = typeof(T);

            /*if(moduleType.FullName.StartsWith("GameFramework."))
            {
                Debug.LogError("Not GameFramework Module");
                return null;
            }*/
            
            return GetModule(typeof(T)) as T;
        }

        private static GameFrameworkModule GetModule(Type moduleType)
        {
            foreach(var module in gameFrameworkModules)
            {
                if(module.GetType() == moduleType)
                {
                    return module;
                }
            }

            return CreateModule(moduleType);
        }

        private static GameFrameworkModule CreateModule(Type moduleType)
        {
            GameFrameworkModule module = (GameFrameworkModule)Activator.CreateInstance(moduleType);
            LinkedListNode<GameFrameworkModule> current = gameFrameworkModules.First;

            while(current != null)
            {
                if(module.Prioruty > current.Value.Prioruty)
                {
                    break;
                }

                current = current.Next;
            }

            if(current != null)
            {
                gameFrameworkModules.AddBefore(current, module);
            }
            else
            {
                gameFrameworkModules.AddLast(module);
            }

            return module;
        }
    }
}