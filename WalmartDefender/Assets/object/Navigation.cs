using System;
using UnityEngine;

namespace Game
{
    public abstract class Navigation : INavigation
    {
        public Transform objectivePoint;
        public float speed;

        public Navigation()
        {
        }

        void INavigation.OnReachedDestination()
        {
            
        }
    }


    
}



