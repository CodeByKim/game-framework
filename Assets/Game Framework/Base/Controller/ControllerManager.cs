using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameFramework
{
    public static class ControllerManager
    {
        private static readonly GameFrameworkLinkedList<GameFrameworkController> gameFrameworkControllers = new GameFrameworkLinkedList<GameFrameworkController>();

        public const int GameFrameworkSceneId = 0;

        public static T GetController<T>() where T : GameFrameworkController
        {
            return GetController(typeof(T)) as T;
        }

        public static GameFrameworkController GetController(Type type)
        {
            LinkedListNode<GameFrameworkController> current = gameFrameworkControllers.First;
            while(current != null)
            {
                if(current.Value.GetType() == type)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        public static GameFrameworkController GetController(string typeName)
        {
            LinkedListNode<GameFrameworkController> current = gameFrameworkControllers.First;
            while (current != null)
            {
                Type type = current.Value.GetType();

                if (type.FullName == typeName || type.Name == typeName)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        public static void RegisterController(GameFrameworkController controller)
        {
            Type type = controller.GetType();

            LinkedListNode<GameFrameworkController> current = gameFrameworkControllers.First;
            while(current != null)
            {
                if(current.Value.GetType() == type)
                {
                    Debug.LogError("controller is already...");
                    return;
                }

                current = current.Next;
            }

            gameFrameworkControllers.AddLast(controller);
        }

        public static void Shutdown()
        {
            
        }
    }
}

