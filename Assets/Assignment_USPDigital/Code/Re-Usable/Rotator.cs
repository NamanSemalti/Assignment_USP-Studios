using UnityEngine;

namespace USPDigital
{
    /// <summary>
    /// Continuously rotates the GameObject in the specified direction and speed.
    /// </summary>
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 v3_direction; // Rotation axis and direction
        [SerializeField] private float f_speed;       // Rotation speed

        // Rotate the object every frame
        private void Update()
        {
            transform.Rotate(v3_direction * f_speed * Time.deltaTime);
        }
    }
}
