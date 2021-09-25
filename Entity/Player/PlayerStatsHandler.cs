using UnityEngine;

public class PlayerStatsHandler : PlayerStats
{
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    public void SubtractHealth(float damage)
    {
        if (Random.Range((_agility + _luck), 20) == 20)
        {
            Debug.Log("Miss");
        }
        else
        {
            _currentHealth -= damage;
            Debug.Log(_currentHealth);
        }
    }

    
}
