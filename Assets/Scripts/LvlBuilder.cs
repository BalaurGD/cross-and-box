using UnityEngine;

public class LvlBuilder : MonoBehaviour
{
    public LvlCreator Lvl;
    private FigureLibruary _objectsLibruary;
    private GameObject[] _objectsOnScene;

    private void Awake()
    {
        _objectsLibruary = GetComponent<FigureLibruary>();
        _objectsOnScene = new GameObject[Lvl.PlaceableObjectsArray.Length];
        for (int i = 0; i < Lvl.PlaceableObjectsArray.Length; i++)
        {
            AddObjectOnScene(Lvl.PlaceableObjectsArray[i], i);
        }
        for (int i = 0; i < _objectsOnScene.Length; i++)
        {
            RotateObjectsOnScene(_objectsOnScene[i]);
        }
    }
    private void AddObjectOnScene(PlaceableObject PObject, int num)
    {
        Vector3 ObjectCordinats = new Vector3(PObject.XCordinat, 0, PObject.ZCordinat);
        Object ObjectName = PObject.ObjectName;
        _objectsOnScene[num] = Instantiate(_objectsLibruary.Objects[(int)ObjectName], ObjectCordinats, Quaternion.identity);
    }
    private void RotateObjectsOnScene(GameObject ObjectOnScene)
    {
        ConveyorPart Conveyor = ObjectOnScene.GetComponent<ConveyorPart>();
        if (Conveyor != null)
        {
            Conveyor.CalculateRotation();
        }
        else
            return;
    }
}
