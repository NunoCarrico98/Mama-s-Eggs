using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public double[] MinSpawnIntervalZombieValues
        => minSpawnIntervalZombieValues;
    public double[] MaxSpawnIntervalZombieValues
        => maxSpawnIntervalZombieValues;
    public double[] MinSpawnIntervalTrapValues
        => minSpawnIntervalTrapValues;
    public double[] MaxSpawnIntervalTrapValues
        => maxSpawnIntervalTrapValues;
    public double[] ZombieSpeedValues
        => zombieSpeedValues;

    public float PlayTime { get; private set; }

    [SerializeField] private Transform yolk;
    [SerializeField] private Transform eggWhite;
    [SerializeField] private Transform pan;
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawners;
    [SerializeField] private Transform zombieDestination;
    [SerializeField] private float spawnRadius;
    /* Procedural generation values */
    [SerializeField] private double[] minSpawnIntervalZombieValues = new double[4];
    [SerializeField] private double[] maxSpawnIntervalZombieValues = new double[4];
    [SerializeField] private double[] minSpawnIntervalTrapValues = new double[4];
    [SerializeField] private double[] maxSpawnIntervalTrapValues = new double[4];
    [SerializeField] private double[] zombieSpeedValues = new double[4];

    private void Start()
    {
        SpawnPan();
        SpawnEggWhite();
        SpawnYolk();
        SpawnPlayer();
        SpawnSpawners();
        SpawnZombieDest();
    }

    private void Update()
    {
        PlayTime += Time.deltaTime;
    }

    public Vector2 GetSpawnPos(Vector2 centerPos)
    {
        float ang = Random.value * 360;
        float radius = Random.Range(0, spawnRadius);
        Vector2 pos;
        pos.x = (centerPos.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad));
        pos.y = (centerPos.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad));
        return pos;
    }

    private void SpawnYolk()
    {
        Vector2 centerPos = transform.position;

        Vector2 pos = GetSpawnPos(centerPos);

        yolk.position = pos;
        yolk.rotation = eggWhite.rotation;
    }

    private void SpawnEggWhite() => SetRandomRotation(eggWhite);

    private void SpawnPan() => SetRandomRotation(pan);

    private void SpawnPlayer() => player.position = yolk.position;

    private void SpawnSpawners() => spawners.position = yolk.position;

    private void SpawnZombieDest() => zombieDestination.position = yolk.position;

    private void SetRandomRotation(Transform obj)
    {
        var euler = obj.eulerAngles;
        euler.z = Random.Range(0.0f, 360.0f);
        obj.eulerAngles = euler;
    }
}
