using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
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
            Debug.Log("Cat picked up");
            
            _catPickedUp = true;
            
            // Reset the cat's position and rotation
            _cat.transform.position = _socket.transform.position;
            _cat.transform.rotation = Quaternion.identity;
            
            _cat.transform.SetParent(_socket.transform);
            
            _cat.GetComponent<WanderingBehaviour>().enabled = false;
        }
        else if (_catPickedUp)
        {
            Debug.Log("Release the cat");
            
            _catPickedUp = false;
            _cat.transform.SetParent(null);

            _cat.GetComponent<WanderingBehaviour>().enabled = true;
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
        else if (other.CompareTag(Tags.TREE))
        {
            _catDetected = other.gameObject.GetComponent<MyTree>().HasCat();
            if (_catDetected)
            {
                _cat = other.gameObject.GetComponentInChildren<Cat>().gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.CAT))
        {
            _catDetected = false;
            _cat = null;
        }
        else if (other.CompareTag(Tags.TREE))
        {
            _catDetected = false;
            if (!_catPickedUp)
            {
                _cat = null;
            }
        }
    }
}