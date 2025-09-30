using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PauseManager PauseManager;
    [SerializeField] private float _pushPower = 30f;
    [SerializeField] private float _coolDown = 1f;

    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    [SerializeField] private GameObject _cubePrefab;

    private GameObject _cube;
    private float _timeToCreation = 0f;

    private void Push()
    {
        Rigidbody cubeRB = _cube.GetComponent<Rigidbody>();
        cubeRB.AddForce(Vector3.forward * _pushPower, ForceMode.VelocityChange);
        _cube = null;
    }

    private void CreateCube()
    {
        StartCoroutine(CheckForPauseActive());

        IEnumerator CheckForPauseActive()
        {
            yield return new WaitForSeconds(0.5f);
            if (!PauseManager.PausePanel.activeInHierarchy)
            {
                _cubePrefab.GetComponent<CubeController>().CubeNumber = GetRandomCubeNumber();
                _cube = Instantiate(_cubePrefab);
                _cube.tag = "NewCube";
                Vector3 center = (_pointA.position + _pointB.position) / 2;
                _cube.transform.position = center;
                StopAllCoroutines();
            }
        }        
    }

    private void FixedUpdate()
    {
        if (_timeToCreation < Time.time && GameManager.IsGameOver != true)
        {
            if (Input.touchCount > 0 && _cube == null)
            {
                CreateCube();   
            }

            if (_cube != null)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    float screenWidth = Screen.width;
                    float touchPositionX = Input.GetTouch(0).position.x / screenWidth;
                    _cube.transform.position = Vector3.Lerp(_pointA.position, _pointB.position, touchPositionX);
                }
                else if (Input.touchCount == 0)
                {
                    Push();
                    _timeToCreation = Time.time + _coolDown;
                }
            }
        }
    }

    private int GetRandomCubeNumber()
    {
        int number = 2;
        int iterations = 0;

        for (int i = 2; i <= GameManager.HighestNumberOnCube; i += i)
        {
            iterations++;
        }

        int random = Random.Range(0, iterations);
        for (int i = 0; i < random; i++)
        {
            number += number;
        }
        return number;
    }
}
/* локальная переменная это переменная которая объявлена внутри метода,свойства,параметра метода,цикла,условной конструкции
 * локальная переменная состоит из 2 частей 1 это тип данных 2 название.В локальную переменную можно присвоить значение сразу же а можно
 * зделать это позже.Локальная переменная пишется всегда с маленькой буквы.
 
 */ 