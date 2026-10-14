using UnityEngine;

public class SceneObjectSaver : MonoBehaviour
{
	private static SceneObjectSaver instance;

    [SerializeField] private GameObject[] ethernalObjects;

	private void Awake()
	{
		if (instance != null)
			instance = this;
		else
			Destroy(this.gameObject);

		foreach (var obj in ethernalObjects) 
			DontDestroyOnLoad(obj);
		DontDestroyOnLoad(this);
	}
}
