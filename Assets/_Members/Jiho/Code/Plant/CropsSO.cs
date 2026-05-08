using System.Collections.Generic;
using Code.Combat;
using UnityEngine;

namespace Code.Plant.Crops
{
    [CreateAssetMenu(fileName = "Crops", menuName = "SO/Crops", order = 0)]
    public class CropsSO : ScriptableObject
    {
        public bool canAttack;
        public CropsRarity cropsRarity;
        public StatType eatStatType;
        public float eatStatValue;
        public List<string> cropsNameList = new(4);
        public List<string> cropsVisualNameList = new(4);
        public List<Sprite> spriteList = new(4);
        public List<float> growTimeList = new(4);
        public List<float> damageList = new(4);
        public List<float> attackSpeedList = new(4);
        public List<float> attackRangeList = new(4);
    }
}