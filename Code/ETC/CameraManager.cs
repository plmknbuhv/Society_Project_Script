using Unity.Cinemachine;
using UnityEngine;
using Work.Common.Core;

namespace Work.SHS.Code.ETC
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        [SerializeField] private CinemachineImpulseSource impulseSource;

        public void ImpulseCamera(float power)
        {
            impulseSource.GenerateImpulse(power);
        }
    }
}