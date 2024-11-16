using System;
using Cinemachine;
using UnityEngine;

namespace GEF
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraManager : MonoBehaviour
    {
        #region Properties
        private CinemachineVirtualCamera _camera = null;
        private const float MIN_CAMERA_DISTANCE = 6.0f;
        #endregion

        #region Lifecycle
        private void Awake()
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
        }
        #endregion

        #region Public Methods
        public void EnableShake(float amplitude, float frequency)
        {
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        }

        public void DisableShake()
        {
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.0f;
            _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.0f;
        }

        public void SetCameraDistance(float distance)
        {
            _camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = distance;
        }

        public void IncreaseCameraDistance()
        {
            _camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance += 0.5f;
        }

        public void DecreaseCameraDistance()
        {
            if (_camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance > MIN_CAMERA_DISTANCE)
                _camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance -= 0.5f;
        }

        public void RotateCameraLeft()
        {
            _camera.transform.rotation = Quaternion.Euler(_camera.transform.eulerAngles.x,
                _camera.transform.rotation.eulerAngles.y + 0.5f, _camera.transform.eulerAngles.z);
        }

        public void RotateCameraRight()
        {
            _camera.transform.rotation = Quaternion.Euler(_camera.transform.eulerAngles.x,
                _camera.transform.rotation.eulerAngles.y - 0.5f, _camera.transform.eulerAngles.z);
        }

        public void TiltCameraUp()
        {
            if (_camera.transform.eulerAngles.x + 0.5f > 90.0f) return;
            _camera.transform.rotation = Quaternion.Euler(_camera.transform.eulerAngles.x + 0.5f,
                _camera.transform.eulerAngles.y, _camera.transform.eulerAngles.z);
        }

        public void TiltCameraDown()
        {
            if (_camera.transform.eulerAngles.x - 0.5f < 0.0f) return;
            _camera.transform.rotation = Quaternion.Euler(_camera.transform.eulerAngles.x - 0.5f,
                _camera.transform.eulerAngles.y, _camera.transform.eulerAngles.z);
        }
        #endregion
    }
}
