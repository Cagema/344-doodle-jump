using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] _prefabs;

	[SerializeField] float _distanceBetween;
	float _curHeight= 7.5f;
	int _index = 4;

	public void Spawn()
	{
		var newGO = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], SetPosition(), Quaternion.identity);
		newGO.transform.SetParent(transform, true);
	}

	private Vector3 SetPosition()
	{
		_curHeight += _distanceBetween;
		return new Vector3(-10, _curHeight, 0);
	}
}
