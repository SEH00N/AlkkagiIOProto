using UnityEngine;

public abstract class Character : Unit
{
    public enum ECharacterState
    {
        Idle,
        Dynamic,
    }

    [Header("Character")]
    [SerializeField] StatContainer<EStatType> statContainer = new StatContainer<EStatType>();

    public override float Weight => base.Weight + GetStat(EStatType.Weight).CurrentValue;
    
    private Vector2 velocityBuffer = Vector2.zero;
    public override Vector2 Velocity => velocityBuffer;

    private Vector2 moveDirection = Vector2.zero;
    private ECharacterState characterState = ECharacterState.Idle;
    public ECharacterState CharacterState => characterState;

    protected override void Start()
    {
        base.Start();
        statContainer.Build();
    }

    protected override void Update()
    {
        base.Update();

        if(characterState == ECharacterState.Dynamic)
        {
            if(unitRigidbody.linearVelocity.magnitude <= 0.1f)
                characterState = ECharacterState.Idle;
        }

        if(characterState == ECharacterState.Idle)
            unitRigidbody.linearVelocity = moveDirection * GetStat(EStatType.MoveSpeed).CurrentValue;
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        velocityBuffer = unitRigidbody.linearVelocity;
    }

    protected override void OnCollide(Entity other, Vector2 contactPoint, Vector2 normal, Vector2 reflected)
    {
        base.OnCollide(other, contactPoint, normal, reflected);
        // Vector2 reflectionDirection = Vector2.Reflect(unitRigidbody.linearVelocity.normalized, normal);
        // if(reflectionDirection.magnitude <= 0.1f)
        //     reflectionDirection = -normal;

        LaunchDynamicMotion(reflected.normalized, reflected.magnitude);
    }

    protected void LaunchDynamicMotion(Vector2 direction, float power)
    {
        characterState = ECharacterState.Dynamic;
        unitRigidbody.linearVelocity = direction.normalized * power;
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    protected Stat GetStat(EStatType statType)
    {
        return statContainer[statType];
    }
}
