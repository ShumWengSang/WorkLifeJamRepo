using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/Stats")]
public class PlayerStatsSO : ScriptableObject
{
    [System.Serializable]
    public class PlayerStats
    {
        [SerializeField]
        public bool CanInput;
        public int Happiness;
        public int Stress;
        public int Money;
    }

    public PlayerStats stats;

    private void OnEnable()
    {
        GlobalPlayer.stats = stats;
    }
}

public static class GlobalPlayer
{
    public static PlayerStatsSO.PlayerStats stats;
}


