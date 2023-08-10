using UnityEngine;

namespace RyansNamespace {
    public class Toothpaste : Equippable
    {
        protected override void Equip(GameObject GO)
        {
            GO.GetComponent<Toothbrush>().AddToothpaste();
        }

        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            AppManager.instance.sfxManager.PlaySFX("open_toothpaste", 1f);
        }
    }
}
