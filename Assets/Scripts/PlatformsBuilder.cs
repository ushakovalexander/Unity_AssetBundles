using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlatformsBuilder : MonoBehaviour {
  [SerializeField]
  private CameraController _camera;
  [SerializeField]
  private float _waitToCreateSec;
  private List<GameObject> _platforms;
  private Vector3 _nextPosition;
  private WaitForSeconds _waitToCreate;
  private WaitForSeconds _waitToDestroy;

  void Start () {
    var prototypes = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "prototyping"));
    if (prototypes == null) {
      Debug.Log("Failed to load AssetBundle!");
      return;
    }

    _platforms = new List<GameObject>();
    _platforms.Add(prototypes.LoadAsset("PlatformPrototype02x01x02") as GameObject);
    _platforms.Add(prototypes.LoadAsset("PlatformPrototype04x01x04") as GameObject);
    _platforms.Add(prototypes.LoadAsset("PlatformPrototype08x01x08") as GameObject);

    _waitToCreate = new WaitForSeconds(_waitToCreateSec);
    _waitToDestroy = new WaitForSeconds(_waitToCreateSec * 40);
    StartCoroutine(SpawnPlatforms());
  }

  private IEnumerator SpawnPlatforms() {
    while(true) {
      CreatePlatform(GetRandomPlatform());
      yield return _waitToCreate;
    }
  }

  private GameObject GetRandomPlatform() {
    if(_platforms != null && _platforms.Count > 0) {
      int index = UnityEngine.Random.Range(0, _platforms.Count);
      return _platforms[index];
    }
    return null;
  }

  private void CreatePlatform(GameObject platform) {
    if(platform != null) {
      GameObject newPlatform = Instantiate(platform, _nextPosition, Quaternion.identity);
      StartCoroutine(RemovePlatform(newPlatform));
      _nextPosition.Set(_nextPosition.x + 1, _nextPosition.y + platform.GetComponent<Renderer>().bounds.size.y, _nextPosition.z);
      if(_camera != null) {
        _camera.CameraTarget = newPlatform;
      }
    }
  }

  private IEnumerator RemovePlatform(GameObject platform) {
    if(platform == null) {
      yield return null;
    }
    yield return _waitToDestroy;
    Destroy(platform);
  }
}
