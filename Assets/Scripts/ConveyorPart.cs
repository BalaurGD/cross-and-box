using UnityEngine;

public class ConveyorPart : MonoBehaviour
{
	public GameObject StartingPart;
	public GameObject MiddlePart;
	public GameObject EndingPart;
	public Direction DirectionOfConveyor;
	public LayerMask SideGoingConveyor;//у меня какие то траблы с работой слоев поэтому это нужно что бы в префабе указать правильный проверочный слой
	public LayerMask ForwardGoingConveyor;

	private Transform _objectTransform;
	[SerializeField] private bool _isConveyorForward = false, _isConveyorBack = false;

	//меняет слой конвейеру для корректной работу SetCorrectConveyorPart
	public void SetLayerMask()
	{
		if (DirectionOfConveyor == Direction.Forward || DirectionOfConveyor == Direction.Backward)
			gameObject.layer = (int)Layers.ForwardGoingConveyor;
		else if (DirectionOfConveyor == Direction.Right || DirectionOfConveyor == Direction.Left)
			gameObject.layer = (int)Layers.SideGoingConveyor;
	}

	//подставляет нужную часть конвеера в зависимости от ситуации
	public void SetCorrectConveyorPart()
	{
		_objectTransform = GetComponent<Transform>();

		Vector3 FirstDirectionForChecking = new Vector3();
		Vector3 SecondDirectionForChecking = new Vector3();
		LayerMask CheckingLayer = new LayerMask();
		switch (DirectionOfConveyor)
		{
			case Direction.Forward:
				FirstDirectionForChecking = Vector3.forward;
				SecondDirectionForChecking = Vector3.back;
				CheckingLayer = ForwardGoingConveyor;
				break;
			case Direction.Backward:
				FirstDirectionForChecking = Vector3.back;
				SecondDirectionForChecking = Vector3.forward;
				CheckingLayer = ForwardGoingConveyor;
				break;
			case Direction.Right:
				FirstDirectionForChecking = Vector3.right;
				SecondDirectionForChecking = Vector3.left;
				CheckingLayer = SideGoingConveyor;
				break;
			case Direction.Left:
				FirstDirectionForChecking = Vector3.left;
				SecondDirectionForChecking = Vector3.right;
				CheckingLayer = SideGoingConveyor;
				break;
		}
		if (Physics.Raycast(_objectTransform.position, transform.TransformDirection(FirstDirectionForChecking), 0.6f, CheckingLayer))
		{
			_isConveyorForward = true;
		}
		if (Physics.Raycast(_objectTransform.position, transform.TransformDirection(SecondDirectionForChecking), 0.6f, CheckingLayer))
		{
			_isConveyorBack = true;
		}
		if (_isConveyorBack && !_isConveyorForward)
			GameObject.Instantiate(EndingPart, _objectTransform);
		else if (_isConveyorBack && _isConveyorForward)
			GameObject.Instantiate(MiddlePart, _objectTransform);
		else if (!_isConveyorBack && _isConveyorForward)
			GameObject.Instantiate(StartingPart, _objectTransform);
		RotateConveyor();
	}

	//поворачивает конвейер в нужную сторону
	private void RotateConveyor()
	{
		float RotationMultiplayer = 0;
		switch (DirectionOfConveyor)
		{
			case Direction.Right:
				RotationMultiplayer = 90;
				break;
			case Direction.Left:
				RotationMultiplayer = -90;
				break;
			case Direction.Forward:
				RotationMultiplayer = 0;
				break;
			case Direction.Backward:
				RotationMultiplayer = 180;
				break;
		}
		Vector3 RotationVector = new Vector3(0, RotationMultiplayer, 0);
		_objectTransform.Rotate(RotationVector);
	}
}
