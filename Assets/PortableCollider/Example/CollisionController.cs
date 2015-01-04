using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using nobnak.Geometry;

public class CollisionController : MonoBehaviour {
	public string tagCollider = "Collider";
	public float radius = 0.5f;
	public float seconds = 1f;
	
	private Camera _targetCamera;
	private MeshFilter[] _meshFilters;
	private Color _initColor;

	// Use this for initialization
	void Start () {
		_targetCamera = Camera.main;
		_meshFilters = GetComponentsInChildren<MeshFilter>();
		_initColor = _meshFilters[0].renderer.sharedMaterial.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Input.GetMouseButton(0))
			return;

		Ray rayFromCamera = _targetCamera.ScreenPointToRay(Input.mousePosition);
		
		MeshFilter mf;
		TriangleTester.HitRes hit;
		if (!MeshTester.hitObject(_meshFilters, rayFromCamera, out mf, out hit)) {
			//Debug.Log("Not found");
			return;
		}
		
		Debug.Log(string.Format("Hit on tri {1} in mesh {0}", mf.gameObject.name, hit.i));
		StartCoroutine(ChangeColorForWhile(mf.gameObject.renderer, seconds));
	}
	
	IEnumerator ChangeColorForWhile(Renderer r, float seconds) {
		r.material.color = Color.green;
		yield return new WaitForSeconds(seconds);
		r.material.color = _initColor;
	}
}
