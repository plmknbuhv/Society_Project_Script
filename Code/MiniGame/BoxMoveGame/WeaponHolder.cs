using AKH.Scripts.Entities;
using UnityEngine;

namespace Work.SHS.Code.MiniGame.BoxMoveGame
{
    public class WeaponHolder : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        [SerializeField] private Weapon[] weapons;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public void DropWeapons()
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.Drop();
            }
        }
    }
}