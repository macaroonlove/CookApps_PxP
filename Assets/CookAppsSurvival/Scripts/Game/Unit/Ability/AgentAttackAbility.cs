using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    public class AgentAttackAbility : AttackAbility
    {
        private float skillCooldownTime = 1f;
        private float finalSkillCooldownTime = 1f;

        private PartyUnit _partyUnit;
        private PartySystem _partySystem;
        private EnemySystem _enemySystem;

        internal float skillCooldownAmount => skillCooldownTime / finalSkillCooldownTime;

        public override Unit unit => _partyUnit;

        #region ����
        internal override float finalAttackTerm
        {
            get
            {
                float final = _pureAttackTerm;

                float increase = 1;
                // ���� ���� �������� ���� ���ݼӵ� ����
                foreach (var effect in _partyUnit.buffAbility.AttackSpeedIncreaseDataEffects)
                {
                    increase += effect.value;
                }

                final /= increase;

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

                // ������ ��� ��Ÿ� ����
                if (_attackTarget is EnemyUnit enemy)
                {
                    if (enemy.template.enemyType == EEnemyType.Boss)
                    {
                        final += enemy.template.attackRange;
                    }
                }

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
                float final = _pureATK;

                float increase = 1;

                // ������ ���� ���ݷ� �߰� (���Ƿ� +10��)
                int level = _partyUnit.GetLevel();
                final += (level - 1) * 10;

                // ���� ���� �������� ���� ���ݷ� ����
                foreach(var effect in _partyUnit.buffAbility.ATKIncreaseDataEffects)
                {
                    increase += effect.value;
                }

                final *= increase;

                return (int)final;
            }
        }
        #endregion

        internal void Initialize(PartyUnit partyUnit)
        {
            this._partyUnit = partyUnit;

            _pureATK = partyUnit.pureATK;
            _pureAttackTerm = partyUnit.pureAttackTerm;
            _pureAttackDistance = partyUnit.pureAttackRange;

            cooldownTime = finalAttackTerm;
            isAttackAble = true;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();

            base.Initialize();
        }

        internal override void DeInitialize()
        {
            isAttackAble = false;
            base.DeInitialize();
        }

        protected override void Update()
        {
            if (skillCooldownTime > 0)
            {
                skillCooldownTime -= Time.deltaTime;
            }

            base.Update();
        }

        protected override bool Action()
        {            
            if (_partyUnit.moveAbility.isMove) return false;
            if (_partyUnit.abnormalStatusAbility.UnableToAttackEffects.Count > 0) return false;

            if (skillCooldownTime > 0)
            {
                // �⺻ ����
                ExcuteAttack();
            }
            else
            {
                // ��ų ����
                ExcuteSkill();
            }
            
            return true;
        }

        #region �⺻ ����
        private void ExcuteAttack()
        {
            // ���� ������ ��ǥ�� �̵��ϴ� Ÿ���� ���� Ÿ������ ����
            _attackTarget = _partySystem.mainUnit.moveAbility.target;
            
            if (_attackTarget == null) return;

            _partyUnit.moveAbility.NewAttackTarget(_attackTarget);

            // ���� ���� �ȿ� Ÿ���� ���Դ���
            bool isInRange = IsInRange(_attackTarget);

            // ������ Ÿ���� �ְ�, ���� �ȿ� �ִٸ�
            if (_attackTarget != null && isInRange)
            {
                // ����
                AttackAnimation(_attackTarget);
            }
        }
        #endregion

        #region ��ų ����
        private void ExcuteSkill()
        {
            // ��ų ���
            _partyUnit.animationController.Skill();

            foreach (var effect in _partyUnit.skillTemplate.effects)
            {
                effect.Excute(_partyUnit);
            }
            skillCooldownTime = _partyUnit.skillTemplate.cooldownTime;
            finalSkillCooldownTime = skillCooldownTime;
        }
        #endregion


        #region ��ƿ��Ƽ �޼���
        internal List<EnemyUnit> FindAttackTargetEnemies(EEnemyTarget targetCondition, float radius, int maxCount)
        {
            List<EnemyUnit> enemies = new List<EnemyUnit>();

            switch (targetCondition)
            {
                case EEnemyTarget.OneEnemyInRange:
                    enemies.Add(_enemySystem.FindNearestEnemy(_partyUnit.transform.position));
                    break;
                case EEnemyTarget.NumEnemyInRange:
                    enemies.AddRange(_enemySystem.FindEnemiesInRadius(_partyUnit.transform.position, radius, maxCount));
                    break;
                case EEnemyTarget.AllEnemyInRange:
                    enemies.AddRange(_enemySystem.FindEnemiesInRadius(_partyUnit.transform.position, radius));
                    break;
                case EEnemyTarget.AllEnemy:
                    enemies.AddRange(_enemySystem.AllEnemies());
                    break;
            }

            return enemies;
        }

        internal List<PartyUnit> FindHealTargetMembers(EAgentTarget targetCondition, float radius)
        {
            List<PartyUnit> members = new List<PartyUnit>();

            switch (targetCondition)
            {
                case EAgentTarget.Myself:
                    members.Add(_partyUnit);
                    break;
                case EAgentTarget.OneAgentInRange:
                    members.AddRange(_partySystem.FindMembersInRadius(_partyUnit.transform.position, radius, 1));
                    break;
                case EAgentTarget.AllAgentInRange:
                    members.AddRange(_partySystem.FindMembersInRadius(_partyUnit.transform.position, radius));
                    break;
                case EAgentTarget.AllAgentInRangeExceptMe:
                    foreach (var member in _partySystem.FindMembersInRadius(_partyUnit.transform.position, radius))
                    {
                        if (member == _partyUnit) continue;
                        members.Add(member);
                    }
                    break;
            }

            return members;
        }
        #endregion
    }
}