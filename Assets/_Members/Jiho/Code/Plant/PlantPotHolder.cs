using System.Collections.Generic;
using Code.Core.Events;
using UnityEngine;

namespace Code.Plant
{
    public class PlantPotHolder : MonoBehaviour
    {
        [SerializeField] private List<PlantPot> plantPotList;
        [SerializeField] private float radius = 5f;
        [SerializeField] private float rotateSpeed = 45f;

        private float _currentAngle;

        private void OnEnable()
        {
            Bus<AddPlantPotEvent>.Subscribe(HandleAddPlantPot);
            Bus<SelectFertilizerEvent>.Subscribe(HandleFertilizer);
        }

        private void OnDestroy()
        {
            Bus<AddPlantPotEvent>.Unsubscribe(HandleAddPlantPot);
            Bus<SelectFertilizerEvent>.Unsubscribe(HandleFertilizer);
        }

        private void Update()
        {
            if (plantPotList == null || plantPotList.Count == 0)
                return;
            
            _currentAngle -= rotateSpeed * Time.deltaTime;

            float angleStep = 360f / plantPotList.Count;

            for (int i = 0; i < plantPotList.Count; ++i)
            {
                float angle = _currentAngle + angleStep * i;
                float rad = angle * Mathf.Deg2Rad;

                Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;

                plantPotList[i].transform.position = transform.position + offset;
            }
        }

        private void HandleAddPlantPot(AddPlantPotEvent evt)
        {
            plantPotList.Add(evt.PlantPot);
        }

        private void HandleFertilizer(SelectFertilizerEvent evt)
        {
            foreach (var plantPot in plantPotList)
                plantPot.ReduceGrowTimer(5f);
        }
    }
}