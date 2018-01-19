using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
  private GameObject _cameraTarget;
  private Vector3 _offset;
  private Vector3 _velocity = Vector3.zero;
  [SerializeField]
  private float smoothTime = 0.1f;

  public GameObject CameraTarget {
    get {
      return _cameraTarget;
    }
    set {
      _cameraTarget = value;
    }
  }

  void Update() {
    if(_offset == Vector3.zero) {
      _offset = transform.position;
    }
    if(CameraTarget != null) {
      transform.position = Vector3.SmoothDamp(transform.position, CameraTarget.transform.position + _offset, ref _velocity, smoothTime);
    }
  }
}
