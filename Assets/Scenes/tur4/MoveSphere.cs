using UnityEngine;

namespace Scene1{
    public class MoveSphere : MonoBehaviour
    {
        float x;
        float y;
        public float Speed;
        private void Update()
        {
            x = Input.GetAxis("Horizontal");

            y = Input.GetAxis("Vertical");

            var dir = x * Vector3.right + y * Vector3.forward;

            transform.position += Speed * dir * Time.deltaTime;

        }
    }
}


