using UnityEngine;

// Interface for damageable entities
public interface IDamageable
{
    void TakeDamage(float amount);
}

// Abstract base class for all marine creatures
public abstract class MarineCreature : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float rotationSpeed = 90f;

    protected float currentHealth;
    protected bool isAlive = true;

    protected virtual void Awake()
    {
        InitializeCreature();
    }

    protected abstract void InitializeCreature();
    protected abstract void Move();

    protected virtual void UpdateAnimation()
    {
        // Override if needed
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            isAlive = false;
            // Add death logic here
        }
    }
}

// A non-hostile NPC with optional movement patterns
public class MarineNPC : MarineCreature
{
    public enum MovementPattern { Stationary, Patrol, Wander }

    [SerializeField] private MovementPattern movementType;
    [SerializeField] private Vector3[] patrolPoints;
    [SerializeField] private float wanderRadius = 5f;

    protected override void InitializeCreature()
    {
        maxHealth = float.PositiveInfinity; // Immortal NPC
        currentHealth = maxHealth;
    }

    protected override void Move()
    {
        // Implement NPC movement logic here
    }
}

// An enemy with attack logic and vulnerability
public class MarineEnemy : MarineCreature
{
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float attackDamage = 20f;

    private bool isNeutralized = false;

    public override void TakeDamage(float amount)
    {
        if (isNeutralized) return;
        base.TakeDamage(amount);

        if (!isAlive)
        {
            isNeutralized = true;
            // Add logic for neutralized behavior
        }
    }

    protected override void InitializeCreature()
    {
        currentHealth = maxHealth;
    }

    protected override void Move()
    {
        // Implement enemy movement/AI logic
    }
}

// A special enemy class with unique behavior
public class BossEnemy : MarineEnemy
{
    private bool isEnraged = false;

    protected override void InitializeCreature()
    {
        base.InitializeCreature();
        // Add boss-specific setup here
    }

    // You can override Move or TakeDamage for boss-specific logic
}

// Interface for swim-capable characters
public interface ISwimmable
{
    void Swim();
}

// A swimming NPC with extra functionality
public class SwimmingNPC : MarineNPC, ISwimmable
{
    public void Swim()
    {
        // Implement swimming behavior
    }
}