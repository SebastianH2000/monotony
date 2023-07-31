using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bean : MonoBehaviour
{
    public enum State {
        BARCODE,
        DRAG
    }

    public State currentState { get; private set; } = State.BARCODE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentState(State state) {
        currentState = state;
    }
}
