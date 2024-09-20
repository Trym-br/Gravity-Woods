using UnityEngine;

public class InputActions : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;

    [SerializeField]
    public float Horizontal;

    [SerializeField]
    public bool Jump;
    public bool Sprint;
    public bool Interact;

    private void Update()
    {
        Horizontal = _inputSystem.Player.Move.ReadValue<Vector2>().x;
        Jump = _inputSystem.Player.Jump.IsPressed();
        Sprint = _inputSystem.Player.Sprint.IsPressed();
    }

    private void Awake() { _inputSystem = new InputSystem_Actions(); }

    private void OnEnable() { _inputSystem.Enable(); }

    private void OnDisable() { _inputSystem.Disable(); }
}