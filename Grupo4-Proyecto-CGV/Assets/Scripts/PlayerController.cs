using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables públicas para ajustar parámetros
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;
    public float gravity = 22f;        // Gravedad aplicada al personaje
    public float fallVelocity;         // Velocidad de caída
    public float jumpForce = 8;        // Fuerza de salto

    public CharacterController player; // Componente CharacterController que controla al personaje

    public float playerSpeed;          // Velocidad de movimiento del jugador
    private Vector3 movePlayer;       // Vector de movimiento

    public Camera mainCamera;          // Cámara principal
    private Vector3 camForward;       // Dirección hacia adelante de la cámara
    private Vector3 camRight;         // Dirección hacia la derecha de la cámara

    // Método llamado al inicio del juego
    void Start()
    {
        // Inicializa el componente CharacterController
        player = GetComponent<CharacterController>();
    }

    // Método llamado en cada fotograma del juego
    void Update()
    {
        // Obtener las entradas del jugador para el movimiento
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        // Crear un vector de entrada del jugador y limitar su magnitud a 1
        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        // Calcular la dirección de la cámara en relación al personaje
        camDirection();

        // Calcular el vector de movimiento del jugador
        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer = movePlayer * playerSpeed;

        // Hacer que el personaje mire en la dirección del movimiento
        player.transform.LookAt(player.transform.position + movePlayer);

        // Aplicar la gravedad
        SetGravity();

        // Gestionar las habilidades del jugador, como el salto
        PlayerSkills();

        // Mover al jugador en base al vector de movimiento
        player.Move(movePlayer * Time.deltaTime);

        // Imprimir la magnitud de la velocidad del jugador en la consola
        Debug.Log(player.velocity.magnitude);
    }

    // Calcular la dirección de la cámara
    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        // Anular los componentes de altura (y) y normalizar los vectores
        camForward.y = 0;
        camRight.y = 0;
        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    // Función para las habilidades del jugador
    public void PlayerSkills()
    {
        // Verificar si el jugador está en el suelo y presiona el botón de salto
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }
    }

    // Función para gestionar la gravedad
    void SetGravity()
    {
        if (player.isGrounded)
        {
            // El jugador está en el suelo, restablecer la velocidad de caída
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
        else
        {
            // El jugador no está en el suelo, aplicar la gravedad
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }
    }
}