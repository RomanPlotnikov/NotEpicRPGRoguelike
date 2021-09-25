using UnityEngine;

public abstract class PlayerStats : MonoBehaviour
{
    protected float _maxHealth = 5;
    protected float _power = 2;
    protected float _agility = 2;
    protected float _luck = 2;

    protected float _level = 1;
    protected float _experience = 0;
    protected float _skillPoints = 1;
    protected float _currentHealth = 5;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public float Power => _power;
    public float Agility => _agility;
    public float Luck => _luck;
    public float Level => _level;
    public float Experience => _experience;
    public float SkillPoints => _skillPoints;
} 