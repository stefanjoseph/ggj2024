using UnityEngine;

public class Player1Controller : GenericPlayerController
{
    private PlayerInputActions _input;

    private void Start()
    {
        _input = new PlayerInputActions();
        _input.Player_V2.Enable();
        _input.Player_V2.Grab_Drop.performed += GrabOrDropPerformed;
    }

    private void Update()
    {
        var move = _input.Player_V2.Movement.ReadValue<Vector3>();

        GenericUpdate(move);
    }

}
