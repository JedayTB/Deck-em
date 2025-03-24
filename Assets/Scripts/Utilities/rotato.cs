using UnityEngine;

public class rotato : MonoBehaviour
{
  [SerializeField] private Vector3 _rotationAxis;
  [SerializeField] private float rotationSpeed;
  void Update()
  {
    transform.Rotate(Time.deltaTime * rotationSpeed * _rotationAxis.normalized);
  }
}
