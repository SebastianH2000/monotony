using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyansNamespace
{
    public class Iris : MonoBehaviour
    {
        [SerializeField] private float mouseDistanceToMaxDistance;
        private float maxDistance;

        // Start is called before the first frame update
        void Start()
        {
            // essentially eyeball radius - iris radius in world space
            float eyeballRadius = transform.parent.lossyScale.x / 2f;
            float irisRadius = transform.lossyScale.x / 2f;

            maxDistance = eyeballRadius - irisRadius;
            mouseDistanceToMaxDistance += eyeballRadius;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 moveDir = ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;
            Vector3 clampedMoveDir = Vector3.ClampMagnitude(moveDir, maxDistance);
            Vector3 lerpedMoveDir = clampedMoveDir * Mathf.Clamp((moveDir.magnitude / mouseDistanceToMaxDistance), 0f, 1f);

            transform.position = transform.parent.position + lerpedMoveDir;
        }

        private Vector3 ScreenToWorldPoint(Vector3 mousePos) {
            mousePos.z = -Camera.main.transform.position.z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            return worldPos;
        }
    }
}
