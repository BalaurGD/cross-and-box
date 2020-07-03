using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New LVL")]
public class LvlCreator : ScriptableObject
{
	[ReorderableList] public PlaceableObject[] PlaceableObjectsArray;
}
