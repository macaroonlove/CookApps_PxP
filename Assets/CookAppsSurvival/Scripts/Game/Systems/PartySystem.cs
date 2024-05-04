using FrameWork.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CookApps.Game
{
    /// <summary>
    /// �Ʊ� ���� ��Ƽ�� �����ϴ� �ý���
    /// </summary>
    public class PartySystem : MonoBehaviour, ISubSystem
    {
        private Transform _pos;

        internal Transform pos => _pos;

        public void Initialize()
        {
            _pos = transform.GetChild(0).GetChild(0);
        }

        public void Deinitialize()
        {
            
        }
    }
}
