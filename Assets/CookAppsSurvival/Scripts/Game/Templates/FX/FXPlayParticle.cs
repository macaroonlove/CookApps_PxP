using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    [CreateAssetMenu(menuName = "CookAppsSurvival/FX/PlayParticle", fileName = "PlayParticle", order = 0)]
    public class FXPlayParticle : FX
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _offset;

        public override void Play(Unit target, Unit caster = null)
        {
            if (_particle == null) return;
            if (target == null) return;

            target.StartCoroutine(CoPlay(target));
        }

        private IEnumerator CoPlay(Unit target)
        {
            var poolSystem = BattleManager.Instance.GetSubSystem<PoolSystem>();
            
            // ��ƼŬ �����
            var instance = poolSystem.Spawn(_particle.gameObject);

            // ��ġ ����
            instance.transform.position = target.transform.position + _offset;

            // ��ƼŬ ���
            _particle.Play();

            // �����ð�
            yield return new WaitForSeconds(_duration);

            // ��ƼŬ ���߱�
            _particle.Stop();

            // ��ƼŬ �ٽ� Ǯ�� �־�α�
            poolSystem.DeSpawn(instance);

        }
    }
}
