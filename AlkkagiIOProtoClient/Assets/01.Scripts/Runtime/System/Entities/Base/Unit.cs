using UnityEngine;

public abstract class Unit : Entity
{
    protected Rigidbody2D unitRigidbody = null;

    protected override void Awake()
    {
        base.Awake();
        unitRigidbody = GetComponent<Rigidbody2D>();
    }
}
