using System;
using Unity.VisualScripting;

namespace Gamex
{
    public class BuildManager
    {
        private static BuildManager buildManager;
        private Defense defenseToBuild;
        private Node selectedNode;
        private SelectUIScript selectUI;

        public static BuildManager GetInstance()
        {
            if (buildManager == null)
            {
                return new BuildManager();
            }
            return buildManager;
        }

        public BuildManager()
        {
        }

        public Defense GetDefenseToBuild()
        {
            return null;
        }
        public bool CanBuild()
        {
            return true;
        }

        public void SelectNode(Node node)
        {
            
        }

        public void DeSelectNode()
        {

        }

        public void SelectDefenseToBuild(Defense defense)
        {

        }

        public void BuildDefenseOn(bool activate, bool upgrade, bool JSP)
        {

        }
    }
}

