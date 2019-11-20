using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public TMP_Text ScoreText;
    [SerializeField]
    private float _moveMultiplier = 6.0f, _jumpMultiplier = 8.0f;
    private bool _isGrounded;
    private int _scoreCount;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _scoreCount = 0;
        SetScoreCountText();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(Vector3.right * _moveMultiplier, ForceMode.Force);
        
        // isGrounded checking is buggy, needs refactoring
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _rigidbody.AddForce(Vector3.up * _jumpMultiplier, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            _isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            _isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            _scoreCount += 1;
            SetScoreCountText();
        }
        //else if (other.gameObject.CompareTag("Obstacle"))
        //{
        //    other.gameObject.SetActive(false);
        //}
    }

    private void SetScoreCountText()
    {
        ScoreText.text = $"Score: {_scoreCount.ToString()}";
    }
}
