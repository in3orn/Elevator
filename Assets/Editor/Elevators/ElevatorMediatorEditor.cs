using UnityEditor;
using UnityEngine;

namespace Krk.Elevators
{
    [CustomEditor(typeof(ElevatorMediator))]
    public class ElevatorMediatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var mediator = (ElevatorMediator) target;

            if (mediator.Controller == null) return;

            for (var i = 0; i < mediator.Controller.Config.floors.Length; i++)
            {
                var floor = mediator.Controller.Config.floors[i];
                if (GUILayout.Button($"{floor.number} - {floor.name}"))
                    mediator.Controller.AddTargetFloor(i);
            }

            if (GUILayout.Button("Reset"))
                mediator.Controller.Reset();
        }
    }
}