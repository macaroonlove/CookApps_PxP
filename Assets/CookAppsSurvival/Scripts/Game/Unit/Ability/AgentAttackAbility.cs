using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class AgentAttackAbility : AttackAbility
    {
        private PartyUnit _partyUnit;

        private PartySystem _partySystem;

        /// <summary>
        /// ���� ���� ����
        /// </summary>
        protected override float finalAttackTerm
        {
            get
            {
                float final = _pureAttackTerm;

                // ���� �ӵ� ���� ������ ���� (���� ����)

                //�ּҰ��ݼӵ� : �⺻ ���ݼӵ��� 30% ����
                final = Mathf.Min(final, _pureAttackTerm / 0.3f);

                return final;
            }
        }

        /// <summary>
        /// ���� ���ݰŸ�
        /// </summary>
        protected override float finalAttackDistance
        {
            get
            {
                float final = _pureAttackDistance;

                //�Ϻ� ����
                final += 0.1f;

                final = Mathf.Max(final, 0);

                return final;
            }
        }

        internal void Initialize(PartyUnit partyUnit)
        {
            this._partyUnit = partyUnit;

            _pureATK = partyUnit.pureATK;
            _pureAttackTerm = partyUnit.pureAttackTerm;
            _pureAttackDistance = partyUnit.pureAttackRange;
            _cooldownTime = finalAttackTerm;
            _isAttackAble = true;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
        }

        internal void DeInitialize()
        {
            _isAttackAble = false;
        }

        private bool IsInRange(Unit attackTarget)
        {
            float sqrDistance = (_partyUnit.transform.position - attackTarget.transform.position).sqrMagnitude;

            return sqrDistance <= finalAttackDistance * finalAttackDistance;

            //float distance = Vector3.Distance(_partyUnit.transform.position, attackTarget.transform.position);

            //return distance <= finalAttackDistance;
        }

        protected override bool Action()
        {
            if (_partyUnit.moveAbility.isMove) return false;

            var attackTarget = _partySystem.mainUnit.moveAbility.target;
            _partyUnit.moveAbility.NewAttackTarget(attackTarget);

            var isInRange = IsInRange(attackTarget);

            if (attackTarget != null && isInRange)
            {
                Attack(attackTarget);
            }

            return true;
        }

        private void Attack(Unit attackTarget)
        {
            // ���� ���
            _partyUnit.animationController.Attack();

            // �������� ������ �ֱ�
            attackTarget.healthAbility.Damaged(_pureATK);
        }
    }
}
