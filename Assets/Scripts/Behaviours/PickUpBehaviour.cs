using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
    private bool _catPickedUp = false;
    private bool _catDetected = false;
    private GameObject _cat = null;
    
    [SerializeField]
    private GameObject _socket = null;
    
    public void PickUp()
    {
        if (!_catPickedUp && _catDetected)
        {
            _catPickedUp = true;
            Debug.Log("Cat picked up");
            _cat.transform.SetParent(_socket.transform);
        }
        else if (_catPickedUp)
        {
            Debug.Log("Release the cat");
            _catPickedUp = false;
            _cat.transform.SetParent(null);
        }
        else
        {
            Debug.Log("No cat detected");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.CAT))
        {
            _catDetected = true;
            _cat = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.CAT))
        {
            _catDetected = false;
            _cat = null;
        }
    }
}