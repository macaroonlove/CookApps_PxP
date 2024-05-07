using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class EnemyAttackAbility : AttackAbility
    {
        private EnemyUnit _enemyUnit;
        private PartySystem _partySystem;

        public override Unit unit => _enemyUnit;

        #region ����
        internal override float finalAttackTerm
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

        internal override float finalAttackDistance
        {
            get
            {
                float final = _pureAttackDistance;

                //�Ϻ� ����
                final += 0.3f;

                final = Mathf.Max(final, 0);

                return final;
            }
        }

        internal override int finalATK
        {
            get
            {
                int final = _pureATK;

                return final;
            }
        }
        #endregion

        internal void Initialize(EnemyUnit enemyUnit)
        {
            this._enemyUnit = enemyUnit;

            _pureATK = enemyUnit.pureATK;
            _pureAttackTerm = enemyUnit.pureAttackTerm;
            _pureAttackDistance = enemyUnit.pureAttackRange;

            cooldownTime = finalAttackTerm;
            isAttackAble = true;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();

            base.Initialize();
        }

        internal override void DeInitialize()
        {
            isAttackAble = false;
            base.DeInitialize();
        }

        protected override bool Action()
        {
            if (_enemyUnit.moveAbility.isMove) return false;
            if (_enemyUnit.moveAbility.isPatrol) return false;
            if (_enemyUnit.abnormalStatusAbility.UnableToAttackEffects.Count > 0) return false;

            ExcuteAttack();

            return true;
        }

        #region �⺻ ����
        private void ExcuteAttack()
        {
            // ��ǥ�� �̵��ϴ� Ÿ���� ���� Ÿ������ ����
            _attackTarget = _enemyUnit.moveAbility.target;

            // ���� ���� �ȿ� Ÿ���� ���Դ���
            bool isInRange = IsInRange(_attackTarget);

            if (_attackTarget != null && isInRange)
            {
                // ����
                AttackAnimation(_attackTarget);
            }
        }
        #endregion
    }
}
