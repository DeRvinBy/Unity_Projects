using UnityEngine;
using Scripts.Game.Player;

namespace Scripts.Game.PickUpObjects
{
    public class SpaceStone : PickUpObject
    {
        [Header("Option of Space Stone")]

        [SerializeField]
        [Tooltip("Cost of one stone")]
        private int Cost = 1;

        protected override void Action(PlayerStats player)
        {
            player.ChangeSpaceStone(Cost);
            DestroyPuckUpObject();
        }
    }
}