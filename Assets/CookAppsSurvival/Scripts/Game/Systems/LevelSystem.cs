using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    /// <summary>
    /// ���� �ý���
    /// �������� ���� �� ������ ������ ���� �ø���
    /// �� �׾��ִ� ������ ������ �ø� �� �����ϴ�.
    /// </summary>
    public class LevelSystem : MonoBehaviour, ISubSystem
    {
        [SerializeField] private bool isClearExpLevel;
        [SerializeField] private LevelUpCurveTemplate template;

        private PartySystem _partySystem;
        private EnemySystem _enemySystem;

        internal UnityAction onGainExp;
        internal UnityAction onLevelUp;

        public void Initialize()
        {
            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();

            _enemySystem.onDieEnemy += OnDieEnemy;

            if (isClearExpLevel)
            {
                StartCoroutine(CoClearExpLevel());
            }
        }

        private IEnumerator CoClearExpLevel()
        {
            yield return null;
            foreach (var member in _partySystem.GetAllMembers())
            {
                member.ClearExpLevel();
            }
        }

        public void Deinitialize()
        {
            _enemySystem.onDieEnemy -= OnDieEnemy;
        }

        private void OnDieEnemy(EnemyUnit enemyUnit)
        {
            var units = _partySystem.GetAllMembers();
            var dealStatistics = enemyUnit.statisticsAbility.GetDamage();

            float totalDamage = dealStatistics.Values.Sum();
            int dropExp = enemyUnit.dropExp;

            foreach (var unit in units)
            {
                if (dealStatistics.ContainsKey(unit.id))
                {
                    // ������ ���� ���� / �� ����ġ��
                    float damageRatio = dealStatistics[unit.id] / totalDamage;
                    int exp = Mathf.RoundToInt(damageRatio * dropExp);

                    int totalExp = unit.GainEXP(exp);
                    float needExp = GetNeedExp(unit);

                    onGainExp?.Invoke();

                    if (totalExp > needExp)
                    {
                        int level = unit.LevelUp(Mathf.CeilToInt(totalExp - needExp));
                        onLevelUp?.Invoke();
                    }
                }
            }
        }

        internal float GetNeedExp(PartyUnit unit)
        {
            return template.needExp.Evaluate(unit.GetLevel());
        }

        internal float GetExpGauge(PartyUnit unit)
        {
            int totalExp = unit.GetExp();
            float needExp = template.needExp.Evaluate(unit.GetLevel());

            return (totalExp / needExp);
        }
    }
}
