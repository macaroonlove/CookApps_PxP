using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    /// <summary>
    /// ���� ��ġ�� �޾ƿ��ų� ���� ���� �����ϴ��� ���� �� �� �ִ� Ŭ����
    /// </summary>
    public class EnemySystem : MonoBehaviour, ISubSystem
    {
        [SerializeField, ReadOnly] private List<EnemyUnit> _enemies = new List<EnemyUnit>();

        internal event UnityAction<EnemyUnit> onDieEnemy;

        public void Initialize(StageTemplate stage)
        {
            
        }

        public void Deinitialize()
        {
            foreach (var item in _enemies)
            {
                Destroy(item.gameObject);
            }
            _enemies.Clear();
        }

        internal void Add(EnemyUnit instance)
        {
            _enemies.Add(instance);
        }

        internal void Remove(EnemyUnit instance)
        {
            _enemies.Remove(instance);

            onDieEnemy?.Invoke(instance);
        }

        #region ��ƿ��Ƽ �޼���
        /// <summary>
        /// ��� �� ��ȯ
        /// </summary>
        internal List<EnemyUnit> AllEnemies()
        {
            return _enemies;
        }

        /// <summary>
        /// �ڽ��� �������� ���� ����� ���� ��ȯ
        /// </summary>
        /// <returns></returns>
        internal EnemyUnit FindNearestEnemy(Vector3 unitPos)
        {
            EnemyUnit nearestEnemy = null;
            float nearestDistanceSqr = Mathf.Infinity;

            foreach (EnemyUnit enemy in _enemies)
            {
                if (enemy != null && enemy.isActiveAndEnabled)
                {
                    float distanceSqr = (enemy.transform.position - unitPos).sqrMagnitude;
                    if (distanceSqr < nearestDistanceSqr)
                    {
                        nearestEnemy = enemy;
                        nearestDistanceSqr = distanceSqr;
                    }
                }
            }

            return nearestEnemy;
        }

        /// <summary>
        /// ���� �� �� ã��
        /// </summary>
        internal List<EnemyUnit> FindEnemiesInRadius(Vector3 unitPos, float radius, int maxCount = int.MaxValue)
        {
            List<EnemyUnit> enemies = new List<EnemyUnit>();

            foreach (EnemyUnit enemy in _enemies)
            {
                if (enemies.Count >= maxCount) break;

                if (enemy != null && enemy.isActiveAndEnabled)
                {
                    var diff = enemy.transform.position - unitPos;

                    var distance = diff.magnitude;

                    if (distance <= radius)
                    {
                        if (enemies.Contains(enemy) == false)
                        {
                            enemies.Add(enemy);
                        }
                    }
                }
            }

            return enemies;
        }
        #endregion
    }
}
