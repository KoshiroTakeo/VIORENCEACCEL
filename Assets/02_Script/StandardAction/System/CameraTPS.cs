using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CameraTPS : MonoBehaviour
{
    public GameObject player;
    InputAction mouseValue;
    [SerializeField] Vector2 vector2 = Vector2.zero;

    [Range(-0.999f, -0.5f)]
    public float maxYAngle = -0.5f;
    [Range(0.5f, 0.999f)]
    public float minYAngle = 0.5f;

    private void Start()
    {
        mouseValue = player.GetComponent<PlayerInput>().currentActionMap["Mouse"];
    }

    private void FixedUpdate()
    {
        
        vector2 = mouseValue.ReadValue<Vector2>();
    }


   
}