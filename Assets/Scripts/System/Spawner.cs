using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fish;

    [SerializeField] private Color gizmoColor = Color.red;

    [Tooltip("Total wave for spawning")]
    [SerializeField] private int _numberOfwave;
    [Tooltip("Summon a number of fishes for one wave")]
    [SerializeField] private int _fishesPerWave;
    [Tooltip("Delaying to summon for some seconds")]
    [SerializeField] private int _secondBetweenWave;
    [Tooltip("Declare the spawner start in a second you give")]
    [SerializeField] private int _secondStartDelay;
    private int _currentWave;

    private bool _startSummon = true;

    public List<GameObject> _activeFishes = new List<GameObject>();

    private void Update()
    {
        if(_startSummon == false && _activeFishes.Count == 0)
        {
            _startSummon = true;
            StartSpawner();
        }
    }

    public void StartSpawner()
    {
        ResetSpawner();

        StartCoroutine(BeginWaveSpawn());
    }

    private IEnumerator BeginWaveSpawn()
    {
        yield return new WaitForSeconds(_secondStartDelay);
        while(_currentWave < _numberOfwave)
        {
            SpawnWave(_currentWave);
            _currentWave++;
            yield return new WaitForSeconds(_secondBetweenWave);
        }
        _startSummon = false;
    }

    public void SpawnWave(int wave)
    {
        for (int i = 0; i < _fishesPerWave; i++)
        {
            GameObject fishObject = Instantiate(fish, new Vector3(5.0f, 0.0f, 0.0f), Quaternion.identity);
            fishObject.GetComponent<Fish>().Initialize(this.gameObject);
            _activeFishes.Add(fishObject);
            if (_activeFishes.Count > 6)
                break;
        }
    }

    public void ResetSpawner()
    {
        _currentWave = 0;
        _activeFishes.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Gizmos.DrawSphere(gameObject.transform.position, 0.5f);
    }
}
