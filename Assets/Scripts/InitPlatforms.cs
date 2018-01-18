using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InitPlatforms : MonoBehaviour {
  [SerializeField]
  private int _maxPlatforms = 100;
  private List<GameObject> _platforms;
  private Vector3 _nextPosition;
  private int _counter;

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
  }

  void Update () {
    if(_counter > _maxPlatforms) {
      enabled = false;
    }
    CreatePlatform(GetRandomPlatform());
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
      Instantiate(platform, _nextPosition, Quaternion.identity);
      _counter++;
      _nextPosition.Set(_nextPosition.x + platform.GetComponent<Renderer>().bounds.size.x, _nextPosition.y, _nextPosition.z);
    }
  }
}
