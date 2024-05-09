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
            float nearestDistance = Mathf.Infinity;

            foreach (EnemyUnit enemy in _enemies)
            {
                if (enemy != null && enemy.isActiveAndEnabled)
                {
                    float distance = Vector3.Distance(enemy.transform.position, unitPos);
                    if (distance < nearestDistance)
                    {
                        nearestEnemy = enemy;
                        nearestDistance = distance;
                    }
                }
            }

            return nearestEnemy;
        }

        /// <summary>
        /// �ڽ��� �������� ���� ���� ���� ����� ���� ��ȯ
        /// </summary>
        internal EnemyUnit FindNearestEnemyInRange(Vector3 unitPos, float radius)
        {
            EnemyUnit nearestEnemy = null;
            float nearestDistance = Mathf.Infinity;

            foreach (EnemyUnit enemy in _enemies)
            {
                if (enemy != null && enemy.isActiveAndEnabled)
                {
                    float distance = Vector3.Distance(enemy.transform.position, unitPos);
                    if (distance < nearestDistance && distance <= radius)
                    {
                        nearestEnemy = enemy;
                        nearestDistance = distance;
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
                    if (Vector3.Distance(enemy.transform.position, unitPos) <= radius)
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
