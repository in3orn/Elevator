using UnityEditor;
using UnityEngine;

namespace Krk.Doors
{
    [CustomEditor(typeof(DoorMediator))]
    public class DoorMediatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var mediator = (DoorMediator) target;

            if (GUILayout.Button("Open"))
                mediator.HandleDoorOpened();

            if (GUILayout.Button("Close"))
                mediator.HandleDoorClosed();
        }
    }
}