using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionController : MonoBehaviour {
	public string tagCollider = "Collider";
	public float radius = 0.5f;
	
	private Camera _targetCamera;
	private MeshFilter[] _meshFilters;

	// Use this for initialization
	void Start () {
		_targetCamera = Camera.main;
		_meshFilters = GetComponentsInChildren<MeshFilter>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!Input.GetMouseButtonDown(0))
			return;

		Ray rayFromCamera = _targetCamera.ScreenPointToRay(Input.mousePosition);
		
		MeshFilter mf;
		float dist;
		if (!MeshTester.hitObject(_meshFilters, rayFromCamera, out mf, out dist)) {
			//Debug.Log("Not found");
			return;
		}
		
		Debug.Log(string.Format("Hit on {0}", mf.gameObject.name));
		StartCoroutine(ChangeColorForWhile(mf.gameObject.renderer, 1.0f));
	}
	
	static IEnumerator ChangeColorForWhile(Renderer r, float seconds) {
		var prevColor = r.material.color;
		r.material.color = Color.green;
		yield return new WaitForSeconds(seconds);
		r.material.color = prevColor;
	}
}
