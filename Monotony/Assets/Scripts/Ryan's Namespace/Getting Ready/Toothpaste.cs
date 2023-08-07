using UnityEngine;

namespace RyansNamespace {
    public class Toothpaste : Equippable
    {
        protected override void Equip(GameObject GO)
        {
            GO.GetComponent<Toothbrush>().AddToothpaste();
        }
    }
}
