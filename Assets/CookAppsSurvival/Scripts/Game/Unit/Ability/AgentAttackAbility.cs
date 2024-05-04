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

                // ���� �ӵ� ���� ������ ����

                //�ּҰ��ݼӵ� : �⺻ ���ݼӵ��� 30% ����
                final = Mathf.Min(final, _pureAttackTerm / 0.3f);

                return final;
            }
        }

        internal void Initialize(PartyUnit partyUnit)
        {
            this._partyUnit = partyUnit;

            _pureAttackTerm = partyUnit.template.attackTerm;
            _pureAttackDistance = partyUnit.template.attackRange;
            _cooldownTime = finalAttackTerm;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
        }

        protected override bool Action()
        {
            if (_partyUnit.moveAbility.isMove) return false;

            var attackTarget = _partySystem.mainUnit.moveAbility.target;
            _partyUnit.moveAbility.NewAttackTarget(attackTarget);

            Attack(attackTarget);

            return true;
        }

        private void Attack(Unit attackTarget)
        {
            //Debug.Log("����");
            // ���� ���
            _partyUnit.animationController.Attack();
        }
    }
}
