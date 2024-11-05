using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator m_animator;
    
    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    
    public void OpenDoor()
    {
        m_animator.Play("Open");
    }
    
    public void CloseDoor()
    {
        m_animator.Play("Close");
    }
}
