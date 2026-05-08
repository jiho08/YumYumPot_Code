using Code.Combat;
using Code.Core;
using Code.Players;
using GondrLib.Dependencies;

namespace Code.Managers
{
    public class StatManager : MonoSingleton<StatManager>
    {
        [Inject] private Player _player;
        
        public void ApplyStat(StatType statType, float statValue)
        {
            switch (statType)
            {
                case StatType.AttackPower:
                {
                    PlayerCombatManager.Instance.AddAttackPower(statValue);
                    break;
                }
                
                case StatType.AttackSpeed:
                {
                    PlayerCombatManager.Instance.AddAttackSpeed(statValue);
                    break;
                }

                case StatType.Health:
                {
                    var health = _player.GetCompo<EntityHealth>();
                    health.AddMaxHealth(statValue);
                    break;
                }

                case StatType.MoveSpeed:
                {
                    var movement = _player.GetCompo<PlayerMovement>();
                    movement.AddMoveSpeed(statValue);
                    break;
                }
            }
        }
    }
}