using Code.Core;

namespace Code.Managers
{
    public class PlayerCombatManager : MonoSingleton<PlayerCombatManager>
    {
        public float attackPower;
        public float attackSpeed;

        public void AddAttackPower(float value)
        {
            attackPower += value;
        }

        public void AddAttackSpeed(float value)
        {
            attackSpeed += value;
        }
    }
}