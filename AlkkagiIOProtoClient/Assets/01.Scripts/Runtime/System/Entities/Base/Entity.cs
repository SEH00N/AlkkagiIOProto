using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    private const float RESTITUTION = 1f; // 충돌 계수

    public virtual float Weight => 1f;
    public virtual Vector2 Velocity => Vector2.zero;

    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
    protected virtual void LateUpdate() { }

    protected virtual void OnCollide(Entity other, Vector2 contactPoint, Vector2 normal, Vector2 reflected) {}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Entity otherEntity) == false)
            return;

        Vector2 contactPoint = other.ClosestPoint(transform.position);                  // 충돌 지점
        Vector2 normal = ((Vector2)other.transform.position - contactPoint).normalized; // 법선
        Vector2 tangent = new Vector2(-normal.y, normal.x);                             // 접선

        // 법선 방향 속도
        float velocityNormal = Vector2.Dot(Velocity, normal);
        float otherVelocityNormal = Vector2.Dot(otherEntity.Velocity, normal);

        // 접선 방향 속도
        float velocityTangent = Vector2.Dot(Velocity, tangent);

        // 충돌 후 법선 방향 속도
        float velocityNormalReflected = ((Weight - RESTITUTION * otherEntity.Weight) * velocityNormal + (1 + RESTITUTION) * otherEntity.Weight * otherVelocityNormal) / (Weight + otherEntity.Weight);

        // 충돌 후 접선 방향 속도
        float velocityTangentReflected = velocityTangent;

        // 충돌 후 속도
        Vector2 velocityReflected = velocityNormalReflected * normal + velocityTangentReflected * tangent;

        OnCollide(otherEntity, contactPoint, normal, velocityReflected);
    }
}
