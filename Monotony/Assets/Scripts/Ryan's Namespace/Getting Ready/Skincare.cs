using UnityEngine;

namespace RyansNamespace {
    public class Skincare : Equippable
    {
        protected override void Equip(GameObject GO)
        {
            GO.GetComponent<Player>().CompleteSkincareTask();
        }
    }
}
