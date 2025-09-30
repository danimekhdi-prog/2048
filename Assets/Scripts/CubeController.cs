using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour
{
    public int CubeNumber;
    public float JumpPower;
    public TMP_Text[] CubeInfo;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        UpdateInformation();
    }

    private void UpdateInformation()
    {
        for (int i = 0; i < CubeInfo.Length; i++)
        {
            CubeInfo[i].text = $"{CubeNumber}";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CubeController>())
        {
            if (collision.gameObject.GetComponent<CubeController>().CubeNumber == CubeNumber)
            {
                Vector3 thisCubePosition = transform.position;

                Vector3 collisionCubePosition = collision.gameObject.transform.position;

                Vector3 center = (thisCubePosition + collisionCubePosition) / 2;

                Destroy(collision.gameObject);

                transform.position = center;

                _rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.VelocityChange);

                CubeNumber += CubeNumber;

                GameManager.UpdateScore(CubeNumber);

                if (CubeNumber > GameManager.HighestNumberOnCube)
                {
                    GameManager.HighestNumberOnCube = CubeNumber;
                }

                UpdateInformation();
            }
        }
    }
}
