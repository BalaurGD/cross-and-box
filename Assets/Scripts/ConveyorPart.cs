using UnityEngine;

public class ConveyorPart : MonoBehaviour
{
    public GameObject StartingPart;
    public GameObject MiddlePart;
    public GameObject EndingPart;
    private Transform _objectTransform;
    private bool _isConveyorForward = false, _isConveyorBack = false;

    public LayerMask _layer;

    public void CalculateRotation()
    {
        _objectTransform = GetComponent<Transform>();
        if (Physics.Raycast(_objectTransform.position, transform.TransformDirection(Vector3.forward), 0.6f, _layer))
        {
            _isConveyorForward = true;
        }
        if (Physics.Raycast(_objectTransform.position, transform.TransformDirection(Vector3.back), 0.6f, _layer))
        {
            _isConveyorBack = true;
        }


        if (_isConveyorBack && !_isConveyorForward)
            GameObject.Instantiate(EndingPart, _objectTransform);
        else if(_isConveyorBack && _isConveyorForward)
            GameObject.Instantiate(MiddlePart, _objectTransform);
        else if(!_isConveyorBack && _isConveyorForward)
            GameObject.Instantiate(StartingPart, _objectTransform);
    }
}
