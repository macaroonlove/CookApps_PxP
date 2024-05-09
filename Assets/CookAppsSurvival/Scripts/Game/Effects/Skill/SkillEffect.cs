using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// ��ų�� ȿ������ �����ϴ� Ŭ����
    /// </summary>
    public abstract class SkillEffect : Effect
    {
        public abstract List<Unit> GetTarget(PartyUnit unit);

        public abstract void Excute(PartyUnit unit, Unit enemy);
    }
}