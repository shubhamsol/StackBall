using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreateLevel : MonoBehaviour
{
    [SerializeField] bool _generate;
    [SerializeField] bool _clear;

    [SerializeField] int _no;
    [SerializeField] int _levelDifficulty;

    [SerializeField] float yOffset;
    [SerializeField] float yOffsetRot;

    [SerializeField] Transform _parent;
    [SerializeField] GameObject _obstacalPrefab;

    [SerializeField] Material _safeMaterial;
    [SerializeField] ColorGradient _colorGradient;
    Quaternion _rotation;
    Vector3 _position;

    float _smoothness;

    private void Start()
    {
        Clear();
    }

    private void Update()
    {
        if(_generate)
        {
            _generate = false;

            Generate();
        }
        if(_clear)
        {
            _clear = false;
            Clear();
        }
    }

    private void Clear()
    {
        _rotation = Quaternion.identity;
        _position = _parent.position;
        _smoothness = 0;
    }

    void Generate()
    {
        GradientColors gradientColors = _colorGradient.GetRandomGradient();
        for (int i = 0; i < _no; i++)
        {
            _smoothness += 1f / _no;

            GameObject insObj = Instantiate(_obstacalPrefab, _position, _rotation, _parent);

            int nonSafe = 0;
            foreach (Transform t in insObj.transform)
            {
                Material mat = new Material(_safeMaterial);
                mat.name = t.name;
                mat.color = Color.Lerp(gradientColors.colorTop, gradientColors.colorBottom, _smoothness);
                t.GetComponent<MeshRenderer>().material = mat;
                if(Random.Range(0,100) < _levelDifficulty)
                {
                    if(nonSafe<5)
                    {
                        nonSafe++;
                        t.GetComponent<Part>().isSafe = false;

                    }
                }
            }

            _position.y += yOffset;

            Vector3 tempAngle = _rotation.eulerAngles;
            tempAngle.y += yOffsetRot;
            _rotation.eulerAngles = tempAngle;
        }
    }
}
