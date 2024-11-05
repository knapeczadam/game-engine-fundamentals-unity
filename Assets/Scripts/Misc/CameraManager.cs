using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera _camera = null;
    private Quaternion _cameraRotationStart = Quaternion.identity;
    
    private const float MIN_CAMERA_DISTANCE = 6.0f;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _cameraRotationStart = _camera.transform.rotation;
    }

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
        _camera.transform.rotation = Quaternion.Euler(_cameraRotationStart.eulerAngles.x, _camera.transform.rotation.eulerAngles.y + 0.5f, _cameraRotationStart.eulerAngles.z);
    }
    
    public void RotateCameraRight()
    {
        _camera.transform.rotation = Quaternion.Euler(_cameraRotationStart.eulerAngles.x, _camera.transform.rotation.eulerAngles.y - 0.5f, _cameraRotationStart.eulerAngles.z);
    }
}
