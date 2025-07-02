using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, ISetup<CharacterModel>
{
    private float direction = 0;
    private Rigidbody2D rb;
    [field: SerializeField] public CharacterModel Model { get; set; } = new();

    public Vector2 Velocity => rb?.linearVelocity ?? Vector2.zero;
    private void Awake()
        => rb = GetComponent<Rigidbody2D>();

    
    public void Setup(CharacterModel model)
        => Model = model;

    private void FixedUpdate()
    {
        var scaledDirection = Vector2.right * (direction * Model.Acceleration);
        if (Mathf.Abs(rb.linearVelocity.x) < Model.Speed)
            rb.AddForce(scaledDirection, ForceMode2D.Force);
    }

    public void SetDirection(float direction)
        => this.direction = direction;

    public IEnumerator Jump()
    {
        yield return new WaitForFixedUpdate();
        rb.AddForce(Vector3.up * Model.JumpForce, ForceMode2D.Impulse);
    }
}