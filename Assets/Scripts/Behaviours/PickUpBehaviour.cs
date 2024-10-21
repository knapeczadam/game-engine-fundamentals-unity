using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[DisallowMultipleComponent]
public class PickUpBehaviour : MonoBehaviour
{
    private bool _catDetected = false;
    private bool _catPickedUp = false;
    public bool CatPickedUp => _catPickedUp;
    
    private GameObject _aiCat     = null;
    private GameObject _staticCat = null;
    private GameObject _rootCat   = null;
    
    [SerializeField] private GameObject _catSocket = null;
    
    public delegate void PickUpAction(bool catPickedUp);
    public event PickUpAction OnPickUp;
    
    public void PickUp()
    {
        if (!_catPickedUp && _catDetected)
        {
            Debug.Log("Cat picked up");
            
            _catPickedUp = true;
            OnPickUp?.Invoke(_catPickedUp);

            // Hide the AI cat and show the static cat
            _aiCat.SetActive(false);
            _staticCat.SetActive(true);
            
            _rootCat.transform.SetParent(_catSocket.transform, false);
        }
        else if (_catPickedUp)
        {
            Debug.Log("Release the cat");
            
            _catPickedUp = false;
            OnPickUp?.Invoke(_catPickedUp);

            // Hide the static cat and show the AI cat
            _staticCat.SetActive(false);
            _aiCat.SetActive(true);
            
            _rootCat.transform.SetParent(null, false);
            
            // Reset the cat's position and rotation
            _aiCat.transform.position = transform.position;
            _aiCat.transform.rotation = Quaternion.identity;
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
            
            _aiCat = other.gameObject;
            _staticCat = other.transform.parent.GetComponentInChildren<StaticCat>(true).gameObject;
            _rootCat = other.transform.root.gameObject;
        }
        else if (other.CompareTag(Tags.TREE))
        {
            _catDetected = other.gameObject.GetComponent<MyTree>().HasCat();
            if (_catDetected)
            {
                _staticCat = other.gameObject.GetComponentInChildren<StaticCat>().gameObject;
                _aiCat = _staticCat.transform.parent.GetComponentInChildren<AICat>(true).gameObject;
                _rootCat = _staticCat.transform.parent.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.CAT))
        {
            _catDetected = false;
            _aiCat = null;
            _staticCat = null;
        }
        else if (other.CompareTag(Tags.TREE))
        {
            _catDetected = false;
            if (!_catPickedUp)
            {
                _staticCat = null;
                _aiCat = null;
                _rootCat = null;
            }
        }
    }
}