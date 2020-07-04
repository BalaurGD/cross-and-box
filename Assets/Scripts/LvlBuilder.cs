using UnityEngine;

//собирает уровень )
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
            AddObjectOnScene(Lvl.PlaceableObjectsArray[i], i);  //расставляет обьекты на сцене по координатам
        }


        for (int i = 0; i < _objectsOnScene.Length; i++)
        {
            SetLayerMasksOnObjectsOnScene(_objectsOnScene[i]);
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

        //если на обьекте лежит скрипт конвейера, то он передает ему инфу о направлении
        ConveyorPart Conveyor = _objectsOnScene[num].GetComponent<ConveyorPart>();
        if (Conveyor != null)
        {
            Conveyor.DirectionOfConveyor = PObject.ObjectDirection;
        }
        else
            return;
    }
    //всем кускам конвейеров делает правильный слой
    private void SetLayerMasksOnObjectsOnScene(GameObject ObjectOnScene)
    {
        ConveyorPart Conveyor = ObjectOnScene.GetComponent<ConveyorPart>();
        if (Conveyor != null)
        {
            Conveyor.SetLayerMask();
        }
        else
            return;
    }
    //подставляет необходимую часть конвейера и вращает ее нужным образом
    private void RotateObjectsOnScene(GameObject ObjectOnScene)
    {
        ConveyorPart Conveyor = ObjectOnScene.GetComponent<ConveyorPart>();
        if (Conveyor != null)
        {
            Conveyor.SetCorrectConveyorPart();
        }
        else
            return;
    }
}
