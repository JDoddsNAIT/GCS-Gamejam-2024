using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private GManager GameManager;
    private string _name = "";
    private int _playerID = 0;
    [SerializeField] private PlayerMovement _movementSystem;
    private ParticleSystem _deathParticles;
    [SerializeField] private GameObject _controller;
    private bool _isDead = false;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Debug.Log("New Player Joined");
        GameManager = GameObject.Find("GameManager").transform.GetComponent<GManager>();
        if (GameManager == null) { Debug.LogError("Game Manager is NULL!");  }

        if (_movementSystem == null) { Debug.LogError("Movement Component is NULL!"); }

        _deathParticles = transform.GetComponentInChildren<ParticleSystem>();
        if (_deathParticles == null) { Debug.LogError("Particle System in NULL!");}

        GameManager.PlayerJoined(this);
    }

    private void Update()
    {
        if (!_isDead && _controller.transform.position.y <= -5) { KillPlayer();  }
    }

    public void ResetPlayer(Vector3 resetPosition)
    {
        // Reset position and velocity
        transform.position = resetPosition;
        _movementSystem.ResetMovement();
        _controller.SetActive(true);
    }

    public void KillPlayer()
    {
        _isDead = true;
        _deathParticles.transform.position = _controller.transform.position;
        _deathParticles.Play();
        _controller.SetActive(false);
    }

    public void SetName(string newName)
    {
        _name = newName;
    }

    public void SetID(int newID)
    {
        _playerID = newID;
        _movementSystem.SetID(_playerID);
    }

    public string GetPlayerName()
    {
        return _name;
    }

    public bool IsDead() { return _isDead; }
    public float GetControllerPosX() { return _controller.transform.position.x; }
}
