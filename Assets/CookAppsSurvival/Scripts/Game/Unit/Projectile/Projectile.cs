using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// ����ü Ŭ����
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Unit _caster;
        private Unit _target;
        private AttackAbility _attackAbility;

        // �ʱ�ȭ ����
        private bool isInit;

        internal void Initialize(AttackAbility attackAbility, Unit target)
        {
            _caster = attackAbility.unit;
            _target = target;
            _attackAbility = attackAbility;

            isInit = true;
        }

        private void Update()
        {
            if (isInit == false) return;

            // ���󰡴� ���� Ÿ���� �׾��ٸ�
            if (!_target.healthAbility.IsAlive)
            {
                DeSpawn();
                return;
            }

            Move();
        }

        private void Move()
        {
            var projectilePos = this.transform.position;
            var targetPos = _target.projectileHitPoint.position;
            var distance = Vector3.Distance(projectilePos, targetPos);
            var moveDistance = Time.deltaTime * speed;

            // ���󰡴� ��
            if (distance > moveDistance)
            {
                var dir = (targetPos - projectilePos).normalized;
                var deltaPos = dir * moveDistance;
                this.transform.Translate(deltaPos);
            }
            // �浹
            else
            {
                OnCollision();
            }
        }

        private void OnCollision()
        {
            _attackAbility.AttackImpact(_target);
            DeSpawn();
        }

        private void DeSpawn()
        {
            _attackAbility.DeSpawnProjectile(gameObject);
            isInit = false;
        }
    }
}
