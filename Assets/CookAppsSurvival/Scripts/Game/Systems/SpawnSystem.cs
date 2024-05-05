using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    /// <summary>
    /// ���� �����ϴ� �ý���
    /// </summary>
    public class SpawnSystem : MonoBehaviour, ISubSystem
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private EnemySpawnTemplate template;

        private Stack<EnemyUnit> _enemyUnits;

        private PartySystem _partySystem;
        private EnemySystem _enemySystem;
        private WaitForSeconds waitForSeconds;

        private int _simultaneousMaxCnt = 1;
        private float _spawnRadius = 1;
        private int _size = 10;

        internal event UnityAction<PartyUnit> onCompleteSpawn;

        public void Initialize()
        {
            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();

            waitForSeconds = new WaitForSeconds(template.spawnTime);
            _simultaneousMaxCnt = template.simultaneousMaxCnt;
            _spawnRadius = template.spawnRadius;
            _size = template.poolSize;

            InitPool();
            StartCoroutine(AutomaticSpawn());
        }

        public void Deinitialize()
        {
            foreach (var unit in _enemyUnits)
            {
                Destroy(unit.gameObject);
            }
            _enemyUnits.Clear();
        }

        private void InitPool()
        {
            _enemyUnits = new Stack<EnemyUnit>();
            
            for (int i = 0; i < _size; i++)
            {
                EnemyUnit enemyUnit = Instantiate(_prefab, Vector3.zero, Quaternion.identity, transform).GetComponent<EnemyUnit>();
                enemyUnit.gameObject.SetActive(false);
                _enemyUnits.Push(enemyUnit);
            }
        }

        private IEnumerator AutomaticSpawn()
        {
            while (true)
            {
                int spawnCnt = Random.Range(1, _simultaneousMaxCnt);

                for (int i = 0; i < spawnCnt; i++)
                {
                    SpawnEnemy(_partySystem.mainUnit.transform.position);
                }

                yield return waitForSeconds;
            }
        }

        /// <summary>
        /// �÷��̾� ��ġ�� �������� ���� �׷� ���� ��ġ �޾ƿ���
        /// </summary>
        private Vector3 GetRandomPos(Vector3 partyPos)
        {
            Vector3 randomOffset = Random.onUnitSphere * _spawnRadius;
            randomOffset.y = 1f;

            Vector3 spawnPosition = partyPos + randomOffset;

            return spawnPosition;
        }

        private void SpawnEnemy(Vector3 partyPos)
        {
            EnemyUnit enemyUnit;
            if (_enemyUnits.Count == 0)
            {
                enemyUnit = Instantiate(_prefab, Vector3.zero, Quaternion.identity, transform).GetComponent<EnemyUnit>();
            }
            else
            {
                enemyUnit = _enemyUnits.Pop();
                enemyUnit.gameObject.SetActive(true);
            }
            var spawnPos = GetRandomPos(partyPos);
            enemyUnit.transform.position = spawnPos;
            // TODO: ���ø����� �����ϵ��� ����
            enemyUnit.Initialize();

            _enemySystem.Add(enemyUnit);

            onCompleteSpawn?.Invoke(_partySystem.mainUnit);
        }

        public void DespawnEnemy(EnemyUnit enemyUnit)
        {
            enemyUnit.gameObject.SetActive(false);
            _enemyUnits.Push(enemyUnit);
            _enemySystem.Remove(enemyUnit);
        }
    }
}
