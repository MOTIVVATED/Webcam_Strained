using UnityEngine;

    [RequireComponent(typeof(PlayerInput))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;

    private PlayerInput input;

    public Vector2 GetPlayerPosition()
    {
        Vector3 position = transform.position;

        return position;
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 position = transform.position;
        position.x += input.Horizontal * speed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, minX, maxX);

        transform.position = position;
    }
}
