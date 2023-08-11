using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimAssist : Tank
{
    private Animator anim;
    public float aimRange = 30.0f;
    [SerializeField] private TankAI[] tanksInRange;
    public Image hpBar;

    public Tank closestTarget;

    public float coolDown = 0.0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        tanksInRange = FindObjectsOfType<TankAI>();
        StartAiming();
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateCooldown();
    }

    private void UpdateHealthBar()
    {
        hpBar.fillAmount = GetHealthPercentage();
    }

    private void UpdateCooldown()
    {
        if (coolDown <= 0.5f)
        {
            coolDown += Time.deltaTime;
        }
        else
        {
            HandleFireInput();
        }
    }

    private void HandleFireInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
            coolDown -= 0.5f;
        }
    }

    public void StartAiming()
    {
        InvokeRepeating("GetClosestTank", 0.2f, 0.2f);
    }

    public void StopAiming()
    {
        CancelInvoke("GetClosestTank");
    }

    private void GetClosestTank()
    {
        if (tanksInRange.Length == 0) return;

        Tank closestTank = tanksInRange[0];
        float closestDistance = 1000.0f;

        foreach (TankAI tankAI in tanksInRange)
        {
            if (tankAI != null)
            {
                float tankDistance = Vector3.Distance(transform.position, tankAI.transform.position);

                if (tankDistance <= closestDistance)
                {
                    closestDistance = tankDistance;
                    closestTank = tankAI;
                }
            }
        }
        
        closestTarget = closestTank;
        anim.SetFloat("closestTargetDistance", closestDistance);
    }

    private void OnDestroy()
    {
        ResetHealthBar();
    }

    private void ResetHealthBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = 0;
        }
    }
}
