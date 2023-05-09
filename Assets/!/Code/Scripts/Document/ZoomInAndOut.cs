using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZoomInAndOut : MonoBehaviour {
    private float _currentScale;
    public float minScale, maxScale;
    public float scalingStep = 0.2f;


    private void Start() {
        _currentScale = transform.localScale.x;
    }

    public void ZoomIn() {
        if (_currentScale >= maxScale) {
            _currentScale = maxScale;
        }
        else {
            _currentScale += scalingStep;
        }
        transform.localScale = new Vector2(_currentScale, _currentScale);
    }

    public void ZoomOut() {
        if (_currentScale <= minScale) {
            _currentScale = minScale;
        }
        else {
            _currentScale -= scalingStep;
        }
        transform.localScale = new Vector2(_currentScale, _currentScale);
    }

}