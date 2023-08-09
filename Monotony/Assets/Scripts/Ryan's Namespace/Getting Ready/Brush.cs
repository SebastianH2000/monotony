using UnityEngine;

namespace RyansNamespace {
    public class Brush : Drag
    {
        private enum Type {
            Toothbrush,
            Hairbrush
        }

        private enum State {
            Null,
            MovingPositiveDirection,
            MovingNegativeDirection
        }

        [Header("Brush")]
        [SerializeField] private Type type;
        [SerializeField] private float threshold;
        [SerializeField] private int timesToBrush;

        [Header("Boundaries")]
        [SerializeField] private float min;
        [SerializeField] private float max;

        private State currentState = State.Null;
        private float distanceDragged = 0f;
        private int timesBrushed = 0;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            xMin = type == Type.Toothbrush ? min : RB.position.x;
            xMax = type == Type.Toothbrush ? max : RB.position.x;
            yMin = type == Type.Hairbrush ? min : RB.position.y;
            yMax = type == Type.Hairbrush ? max : RB.position.y;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            switch (type)
            {
                case Type.Toothbrush:
                    UpdateBrushing(clampedPos.x, RB.position.x);
                    break;
                case Type.Hairbrush:
                    UpdateBrushing(clampedPos.y, RB.position.y);
                    break;
            }
        }

        public void UpdateBrushing(float clampedPosAxis, float rbPositionAxis)
        {
            if (clampedPosAxis > rbPositionAxis)
            {
                switch (currentState)
                {
                    case State.MovingNegativeDirection:
                        CheckBrushingCompletion();
                        currentState = State.MovingPositiveDirection;
                        break;
                    case State.MovingPositiveDirection:
                        distanceDragged += Mathf.Abs(clampedPosAxis - rbPositionAxis);
                        break;
                    case State.Null:
                        StartBrushingIfToothbrush();
                        currentState = State.MovingPositiveDirection;
                        break;
                }
            }
            else if (clampedPosAxis < rbPositionAxis)
            {
                switch (currentState)
                {
                    case State.MovingPositiveDirection:
                        CheckBrushingCompletion();
                        currentState = State.MovingNegativeDirection;
                        break;
                    case State.MovingNegativeDirection:
                        distanceDragged += Mathf.Abs(clampedPosAxis - rbPositionAxis);
                        break;
                    case State.Null:
                        StartBrushingIfToothbrush();
                        currentState = State.MovingNegativeDirection;
                        break;
                }
            }
        }

        private void StartBrushingIfToothbrush() {
            if (type == Type.Toothbrush)
                Player.instance.StartBrushingTeeth();
        }

        private void CheckBrushingCompletion() {
            if (distanceDragged > threshold)
            {
                timesBrushed++;

                if (timesBrushed >= timesToBrush)
                {
                    switch (type) {
                        case Type.Toothbrush:
                            Player.instance.StopBrushingTeeth();
                            Debug.Log("You brushed your teeth!");
                            break;
                        case Type.Hairbrush:
                            Player.instance.CompleteHairTask();
                            Debug.Log("You brushed your hair!");
                            break;
                    }
                }
            }

            distanceDragged = 0f;
        }

        public void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(min, yMin), new Vector3(max, yMax));
        }
    }
}
