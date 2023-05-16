using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    public float horizontalInput;
    public float verticalInput;

    public Animator animator;


    void Start()
    {

    }


    void Update()
    {
        MovePlayer();

        SetDireccion();
    }


    /// <summary>
    /// Lógica que mueve al jugador
    /// </summary>
    private void MovePlayer()
    {
        transform.Translate(Vector2.up * Time.deltaTime * verticalInput * speed);


        transform.Translate(Vector2.right * Time.deltaTime * horizontalInput * speed);



        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// Según los se estén presionando una combinación de teclas u otra
    ///     se asigna una animación (cambia dirección hacia donde se mira) 
    /// </summary>
    private void SetDireccion()
    {

        if (horizontalInput > 0)
        {
            // Mirar Derecha
            animator.SetInteger("Right", 1);
        }
        else if (horizontalInput < 0)
        {
            // Mirar Izquierda 
            animator.SetInteger("Right", -1);
        }
        else
        {
            // Mirar Frente
            animator.SetInteger("Right", 0);
        }

        if (verticalInput > 0)
        {
            // Mirar Arriba (Espaldas)
            animator.SetInteger("Top", 1);
        }
        else if (verticalInput < 0)
        {
            // Mirar Abajo(Frente)
            animator.SetInteger("Top", -1);
        }
        else
        {
            // Mirar Frente
            animator.SetInteger("Top", 0);
        }
    }
}
