using UnityEngine;
using System.Collections;

public class PassThroughPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private bool _playerOnPlatform;
    private float vertical;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (_playerOnPlatform && Mathf.Abs(vertical) > 0f)
        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
        // Mathf.Abs(vertical) > 0f
        // Input.GetAxisRaw("Vertical") < 0
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1.0f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<playermovements>();
        if (player != null)
        {
            _playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, value:true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, value:true);
    }
}
