using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class Map
    {
        private List<Node> Nodes;
        private Transform spawnPoint;
        private Transform EndPoint;
        //private NavMesh surface;
        private BuildManager buildManager;

        public Map()
        {
        }

        public bool IsValidPosition(Vector3 position)
        {
            return true; 
        }
        public void PlaceDefense(Vector3 position, Defense defense)
        {

        }


    }
}

