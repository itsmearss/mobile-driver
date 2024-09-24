using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float increaseSpeed = 0.1f;
    [SerializeField] private float turnSpeed = 200f;

    private int steerValue;

    void Update()
    {
        speed = speed + increaseSpeed * Time.deltaTime;

        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            SceneManager.LoadScene("Scene_MainMenu");
        }
    }

    public void Steer(int value)
    {
        steerValue = value;
    }


}
