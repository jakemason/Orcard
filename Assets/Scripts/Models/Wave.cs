﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public List<Enemy> EnemiesInWave;
    public float TimeBetweenSpawns = 0.5f;
}