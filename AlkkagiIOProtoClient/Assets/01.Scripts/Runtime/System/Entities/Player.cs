using UnityEngine;

public class Player : Character
{
    private const float DYNAMIC_MOTION_POWER_MULTIPLIER = 10f;

    private bool isCharging = false;
    private float chargingTime = 0f;

    protected override void Update()
    {
        base.Update();
        
        if(Input.GetKeyDown(KeyCode.Space) && isCharging == false)
        {
            if(CharacterState == ECharacterState.Idle)
            {
                isCharging = true;
                SetMoveDirection(Vector2.zero);
            }
        }

        if(isCharging)
        {
            chargingTime += Time.deltaTime;
            chargingTime = Mathf.Clamp(chargingTime, 0f, GetStat(EStatType.Power).CurrentValue);

            if(Input.GetKeyUp(KeyCode.Space))
            {
                Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = (mousePositionInWorld - (Vector2)transform.position).normalized;
                LaunchDynamicMotion(direction, chargingTime * DYNAMIC_MOTION_POWER_MULTIPLIER);

                chargingTime = 0f;
                isCharging = false;
            }
        }
        else
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            SetMoveDirection(new Vector2(horizontal, vertical));
        }
    }
}
