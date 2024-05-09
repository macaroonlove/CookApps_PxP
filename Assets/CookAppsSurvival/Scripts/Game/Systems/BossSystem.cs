using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CookApps.Game
{
    /// <summary>
    /// ������ �����ϴ� �ý���
    /// </summary>
    public class BossSystem : MonoBehaviour, ISubSystem
    {
        private PartySystem _partySystem;
        private EnemySystem _enemySystem;
        private SpawnSystem _spawnSystem;

        private EnemyTemplate _template;
        private EnemyUnit _instance;
        private int _bossCnt;
        private int _currentKillCnt;

        internal UnityAction onVictory;

        public void Initialize(StageTemplate stage)
        {
            _template = stage.bossEnemy;
            _bossCnt = stage.killUntilBoss;
            _currentKillCnt = 0;

            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();
            _enemySystem = BattleManager.Instance.GetSubSystem<EnemySystem>();
            _spawnSystem = BattleManager.Instance.GetSubSystem<SpawnSystem>();

            _enemySystem.onDieEnemy += BossAppearanceRitual;
        }

        public void Deinitialize()
        {
            _enemySystem.onDieEnemy -= BossAppearanceRitual;
        }

        private void BossAppearanceRitual(EnemyUnit arg0)
        {
            // óġ�� ���� �� ����
            _currentKillCnt++;

            // ��ǥ óġ �� ���� �Ϲ� ���͸� ��Ҵٸ�
            if (_bossCnt <= _currentKillCnt)
            {
                _enemySystem.onDieEnemy -= BossAppearanceRitual;

                // ���� ����
                _instance = _spawnSystem.SpawnEnemy(_template, _partySystem.mainUnit.transform.position);
                _partySystem.mainUnit.moveAbility.FocusBossTarget(_instance);

                // ��� �Ϲ� ���� ��Ȱ��ȭ
                _spawnSystem.DisableAllEnemy();

                _instance.healthAbility.onDeath += Victory;
            }
        }

        /// <summary>
        /// ���� �¸�
        /// </summary>
        private void Victory()
        {
            var members = _partySystem.GetAllMembers();

            foreach(var member in members)
            {
                member.animationController.Victory();
                member.DeInitialize();
            }

            onVictory?.Invoke();

            _instance.healthAbility.onDeath -= Victory;
        }
    }
}