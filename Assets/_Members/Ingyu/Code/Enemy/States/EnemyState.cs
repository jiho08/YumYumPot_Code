using Code.Entities;
using Code.FSM;
using UnityEngine;

namespace Code.Ennemis.States {
    public abstract class EnemyState : EntityState
    {
        protected Enemy _enemy;

        protected EnemyState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _enemy = entity as Enemy;
        }
    }


}


