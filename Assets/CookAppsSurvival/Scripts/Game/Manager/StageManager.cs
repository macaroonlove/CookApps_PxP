using FrameWork;
using FrameWork.Sound;
using FrameWork.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CookApps.Game
{
    public class StageManager : Singleton<StageManager>
    {
        [SerializeField] StageLibraryTemplate template;

        [Header("UI")]
        [SerializeField] CanvasGroupController ui_Opening;
        [SerializeField] CanvasGroupController ui_GameClear;
        [SerializeField] CanvasGroupController ui_Victory;
        [SerializeField] CanvasGroupController ui_Default;

        [Header("����")]
        [SerializeField] AudioClip clip_GameStart;

        private BossSystem _bossSystem;
        private PartySystem _partySystem;

        private string activeScene;

        protected override void Start()
        {
            base.Start();

            activeScene = SceneManager.GetActiveScene().name;

            if (activeScene == template.stage[0].sceneName)
            {
                ui_Opening.Show();
                SoundManager.PlayBGM(clip_GameStart);
                return;
            }

            InitializeStage();
        }

        internal void InitializeStage()
        {
            foreach (var stage in template.stage)
            {
                if (stage.sceneName == activeScene)
                {
                    SoundManager.PlayBGM(stage.BGM);
                    BattleManager.Instance.InitializeStage(stage);
                }
            }

            _bossSystem = BattleManager.Instance.GetSubSystem<BossSystem>();
            _partySystem = BattleManager.Instance.GetSubSystem<PartySystem>();


            _bossSystem.onVictory += OnVictory;
            _partySystem.onDefault += OnDefeat;
        }

        private void OnVictory()
        {
            for (int i = 0; i < template.stage.Count; i++)
            {
                var stage = template.stage[i];
                if (stage.sceneName == activeScene)
                {
                    if (i + 1 == template.stage.Count)
                    {
                        // ���� Ŭ���� UI �����ֱ�
                        ui_GameClear.Show();
                    }
                    else
                    {
                        // �¸� UI �����ֱ�
                        ui_Victory.Show();
                        
                    }
                }
            }

            UnityEventClear();
        }

        private void OnDefeat()
        {
            ui_Default.Show();

            UnityEventClear();
        }

        private void UnityEventClear()
        {
            _bossSystem.onVictory -= OnVictory;
            _partySystem.onDefault -= OnDefeat;
        }

        public void Victory()
        {
            for (int i = 0; i < template.stage.Count; i++)
            {
                var stage = template.stage[i];
                if (stage.sceneName == activeScene)
                {
                    // ���� ������ �̵�
                    SceneManager.LoadScene($"{template.stage[i + 1].sceneName}");
                }
            }    
        }

        public void DefeatOrGameClear()
        {
            // ù ������ �̵�
            SceneManager.LoadScene($"{template.stage[0].sceneName}");
        }

        
    }
}
