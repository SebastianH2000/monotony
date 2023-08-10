using UnityEngine;
using SebastiansNamespace;

namespace RyansNamespace {
    public class Skincare : Equippable
    {
        protected override void Equip(GameObject GO)
        {
            if (!GameObject.Find("Mirror Monster").GetComponent<GettingReadyMonster>().isMonster) {
                GO.GetComponent<Player>().CompleteSkincareTask();
            }
        }
    }
}
